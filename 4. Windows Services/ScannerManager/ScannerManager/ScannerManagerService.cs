using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using ScannerManager.ImageQueueCollection;
using Topshelf;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Threading.Tasks;

namespace ScannerManager
{
	public class ScannerManagerService : ServiceControl
	{
		private static string DONE_FOLDER;
		private static string UNRECOGNIZED_FOLDER;

		private static string CONFIG_PATH;
		public static void SetConfigPath(string path) => CONFIG_PATH = path;

		private Timer timer;
		private Timer timeOut;
		private ImageQueue imageQueue;
		private Config config;

		private DirectoryInfo directoryInfo;

		private object sync = new object();

		public bool Start(HostControl hostControl)
		{
			if (string.IsNullOrEmpty(CONFIG_PATH))
			{
				// Some default value.
				CONFIG_PATH = "C:\\config.json";
			}

			try
			{
				this.LoadJson();
			}
			catch (Exception ex)
			{
				return false;
			}

			DONE_FOLDER = Path.Combine(config.ReadFolder, "DONE");
			UNRECOGNIZED_FOLDER = Path.Combine(config.ReadFolder, "UNRECOGNIZED");

			this.CreateFolders();

			this.directoryInfo = new DirectoryInfo(config.ReadFolder);

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

			return true;
		}

		private void SetupCheckTimer()
		{
			this.timer = new Timer();
			this.timer.Interval = 3000; // 3 seconds
			this.timer.Elapsed += new ElapsedEventHandler(this.CheckFolder);
			this.timer.Start();
		}

		private void SetupTimeOut()
		{
			this.timeOut = new Timer();
			this.timeOut.Interval = this.config.TimeOut * 1000;
			this.timeOut.Elapsed += new ElapsedEventHandler(this.HandleTimeOut);
		}

		private void LoadJson()
		{
			using (StreamReader r = new StreamReader(CONFIG_PATH))
			{
				string json = r.ReadToEnd();
				this.config = JsonConvert.DeserializeObject<Config>(json);
			}

			if (!this.config.Validate())
			{
				throw new Exception();
			}
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

			document.Save(Path.Combine(config.DropFolder, DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + ".pdf"));

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
	}
}
