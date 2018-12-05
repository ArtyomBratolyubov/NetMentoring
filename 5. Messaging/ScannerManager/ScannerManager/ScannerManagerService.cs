using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using ScannerManager.ImageQueueCollection;
using Topshelf;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Threading.Tasks;
using System.Configuration;
using System.Messaging;
using ScannerServiceApi;

namespace ScannerManager
{
	public class ScannerManagerService : ServiceControl
	{
		private const int CHUNK_SIZE = 3000000; //3 MB

		private string SCANNER_NAME = ConfigurationManager.AppSettings["ScannerName"];
		private const string DOCUMENT_QUEUE = "ScannerDocumentQueue";
		private const string STATUS_QUEUE = "ScannerStatusQueue";
		private const string SETTINGS_QUEUE = "ScannerSettingsQueue";

		private MessageQueue documentQueue;
		private MessageQueue statusQueue;
		private MessageQueue settingsQueue;

		private static string DONE_FOLDER;
		private static string UNRECOGNIZED_FOLDER;

		private Timer timer;
		private Timer timeOut;
		private ImageQueue imageQueue;

		private DirectoryInfo directoryInfo;

		private object sync = new object();

		public bool Start(HostControl hostControl)
		{
			DONE_FOLDER = Path.Combine(ConfigurationManager.AppSettings["ReadFolder"], "DONE");
			UNRECOGNIZED_FOLDER = Path.Combine(ConfigurationManager.AppSettings["ReadFolder"], "UNRECOGNIZED");

			this.CreateFolders();

			this.directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["ReadFolder"]);

			this.imageQueue = new ImageQueue();

			this.SetupCheckTimer();
			this.SetupTimeOut();

			// Initial check.
			Task.Run(() => { this.CheckFolder(null, null); });

			return true;
		}

		public bool Stop(HostControl hostControl)
		{
			lock (sync)
			{
				this.timer.Stop();
				this.timeOut.Stop();
			}

			this.SendStatus("Stopped");
			return true;
		}

		private void SetupCheckTimer()
		{
			this.timer = new Timer();
			this.timer.Interval = 3000; // 3 seconds
			this.timer.Elapsed += new ElapsedEventHandler(this.CheckFolder);
			this.timer.Elapsed += new ElapsedEventHandler(this.ReadSettings);
			this.timer.Start();
		}

		private void SetupTimeOut()
		{
			this.timeOut = new Timer();
			this.timeOut.Interval = int.Parse(ConfigurationManager.AppSettings["TimeOut"]) * 1000;
			this.timeOut.Elapsed += new ElapsedEventHandler(this.HandleTimeOut);
		}

		private void CreateFolders()
		{
			Directory.CreateDirectory(DONE_FOLDER);
			Directory.CreateDirectory(UNRECOGNIZED_FOLDER);
		}

		private void CheckFolder(object sender, ElapsedEventArgs e)
		{
			lock (sync)
			{
				this.SendStatus("Checking read folder");

				var files = this.directoryInfo.GetFiles();
				List<QImage> matched = new List<QImage>();

				// Itarate all found files in folder.
				foreach (var file in files)
				{
					// Test string could be moved to config file.
					var match = Regex.IsMatch(file.Name, @"^scan_+\d+.jpg", RegexOptions.IgnoreCase);

					if (match)
					{
						int number = Int32.Parse(Regex.Match(file.Name, @"\d+").Value);

						matched.Add(new QImage(file, number));
					}
					else
					{
						this.MoveFile(file, UNRECOGNIZED_FOLDER);
					}
				}

				int maxNum = -1;
				if (matched.Count != 0)
				{
					maxNum = matched.Max(x => x.Number);
				}

				// Check if the highest number of all readed images is the same or less than current, 
				if (maxNum <= this.imageQueue.CurrentNumber)
				{
					// start timeout for current queue;
					if (!this.timeOut.Enabled)
					{
						this.timeOut.Start();
					}

					// No need to do anything else.
					return;
				}
				else
				{
					// otherwise stop timeout as we found new image.
					if (this.timeOut.Enabled)
					{
						this.timeOut.Stop();
					}
				}

				foreach (var image in matched.OrderBy(x => x.Number))
				{
					this.imageQueue.AddImage(image);
				}

				var queues = this.imageQueue.Flush();

				// Create PDF documents for each queue.
				foreach (var queue in queues)
				{
					this.CreatePDF(queue);
				}
			}
		}

		private void HandleTimeOut(object sender, ElapsedEventArgs e)
		{
			lock (sync)
			{
				var queue = this.imageQueue.FlushCurrent();

				this.CreatePDF(queue);
			}
		}

		private void CreatePDF(Queue<QImage> images)
		{
			if (images.Count == 0)
			{
				return;
			}

			this.SendStatus("Creating pdf documents");

			PdfDocument document = new PdfDocument();

			foreach (var img in images)
			{
				if (!img.IsSeparator)
				{
					PdfPage page = document.AddPage();

					XGraphics gfx = XGraphics.FromPdfPage(page);
					XImage image = XImage.FromGdiPlusImage(img.Image);

					double resize = img.Image.Width / 500d;
					gfx.DrawImage(image, 50, 50, 500, img.Image.Height / resize);
				}
			}

			string docName = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + ".pdf";
			if (!this.SendDocument(document, docName))
			{
				document.Save(Path.Combine(ConfigurationManager.AppSettings["DropFolder"], docName));
			}

			// We can dispose images in previous loop but it is safier to do it once document is saved.
			foreach (var img in images)
			{
				this.DoneImage(img);
			}
		}

		private void DoneImage(QImage img)
		{
			img.Dispose();

			this.MoveFile(img.FileInfo, DONE_FOLDER);
		}

		private void MoveFile(FileInfo file, string dest)
		{
			int fileCount = -1;
			string fileName;
			do
			{
				fileCount++;
				fileName = Path.GetFileNameWithoutExtension(file.Name)
					+ (fileCount > 0 ? "(" + fileCount.ToString() + ")" : "")
					+ file.Extension;
			}
			while (File.Exists(Path.Combine(dest, fileName)));

			File.Move(file.FullName, Path.Combine(dest, fileName));
		}

		private void SendStatus(string status)
		{
			if (MessageQueue.Exists(@".\Private$\" + STATUS_QUEUE))
			{
				if (this.statusQueue == null)
				{
					this.statusQueue = new MessageQueue(@".\Private$\" + STATUS_QUEUE);
					this.statusQueue.Formatter = new XmlMessageFormatter();
					this.statusQueue.MessageReadPropertyFilter.ArrivedTime = true;
				}

				this.statusQueue.Send(new ServiceStatus
				{
					Name = SCANNER_NAME,
					Status = status,
					TimeOut = this.timeOut.Interval / 1000
				});
			}
		}

		private bool SendDocument(PdfDocument document, string name)
		{
			if (MessageQueue.Exists(@".\Private$\" + DOCUMENT_QUEUE))
			{
				if (this.documentQueue == null)
				{
					this.documentQueue = new MessageQueue(@".\Private$\" + DOCUMENT_QUEUE);
					this.documentQueue.Formatter = new XmlMessageFormatter();
					this.documentQueue.MessageReadPropertyFilter.ArrivedTime = true;
				}

				MemoryStream stream = new MemoryStream();
				document.Save(stream, false);
				byte[] bytes = stream.ToArray();

				int len = bytes.Length;
				int chunksCount = (int)Math.Ceiling((float)len / CHUNK_SIZE);

				string id = Guid.NewGuid().ToString("N");
				for (int i = 0; i < chunksCount; i++)
				{
					this.documentQueue.Send(new DocumentPdf
					{
						Name = name,
						Data = this.GetChunk(bytes, i),
						ChunkNumber = i,
						Id = id,
						ChunksCount = chunksCount
					});
				}
				return true;
			}
			return false;
		}

		private byte[] GetChunk(byte[] arr, int num)
		{
			if (arr.Length < CHUNK_SIZE)
			{
				return arr;
			}

			int skip = CHUNK_SIZE * num;
			int left = arr.Length - skip;

			var result = arr.Skip(skip).Take(CHUNK_SIZE).ToArray();

			return result;
		}

		private void ReadSettings(object sender, ElapsedEventArgs e)
		{
			if (MessageQueue.Exists(@".\Private$\" + SETTINGS_QUEUE))
			{
				if (this.settingsQueue == null)
				{
					this.settingsQueue = new MessageQueue(@".\Private$\" + SETTINGS_QUEUE);
					this.settingsQueue.Formatter = new XmlMessageFormatter(new[] { typeof(Settings) });
				}

				var enumerator = this.settingsQueue.GetMessageEnumerator2();
				while (enumerator.MoveNext())
				{
					var message = enumerator.Current;
					var settings = message.Body as Settings;

					if(settings.Name == this.SCANNER_NAME)
					{
						this.timeOut.Interval = settings.TimeOut * 1000;
						enumerator.RemoveCurrent();
					}
				}
			}
		}
	}
}
