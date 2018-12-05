using System;
using System.ComponentModel;
using System.Messaging;
using System.Timers;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ScannerManagerController
{
	public partial class MainForm : Form
	{
		private const string DOCUMENT_QUEUE = "ScannerDocumentQueue";
		private const string STATUS_QUEUE = "ScannerStatusQueue";
		private const string SETTINGS_QUEUE = "ScannerSettingsQueue";
		private const int TIME_OUT = 30; //sec
		private MessageQueue documentQueue;
		private MessageQueue statusQueue;
		private MessageQueue settingsQueue;
		private BindingList<ServiceStatusViewModel> bindingList;
		private Dictionary<string, List<System.Messaging.Message>> rawDocuments;

		public MainForm()
		{
			Directory.CreateDirectory("documents");

			this.InitializeComponent();

			this.bindingList = new BindingList<ServiceStatusViewModel>();

			this.rawDocuments = new Dictionary<string, List<System.Messaging.Message>>();

			this.StatusGridView.DataSource = this.bindingList;
			this.readScannersStatusTimer.Start();
			this.readScannersStatusTimer.Tick += this.ReadStatus;

			this.readPdfDocumentsTimer.Start();
			this.readPdfDocumentsTimer.Tick += this.ReadDocuments;
		}

		private void ReadDocuments(object sender, EventArgs e)
		{
			if (MessageQueue.Exists(@".\Private$\" + DOCUMENT_QUEUE))
			{
				if (this.documentQueue == null)
				{
					this.documentQueue = new MessageQueue(@".\Private$\" + DOCUMENT_QUEUE);
					this.documentQueue.Formatter = new XmlMessageFormatter(new[] { typeof(ScannerServiceApi.DocumentPdf) });
					this.documentQueue.MessageReadPropertyFilter.ArrivedTime = true;
				}

				var enumerator = this.documentQueue.GetMessageEnumerator2();

				while (enumerator.MoveNext())
				{
					var message = enumerator.Current;
					var document = message.Body as ScannerServiceApi.DocumentPdf;

					this.rawDocuments.TryGetValue(document.Id, out var doc);
					if (doc != null)
					{
						doc.Add(message);
					}
					else
					{
						this.rawDocuments.Add(document.Id, new List<System.Messaging.Message> { message });
					}
				}

				this.SaveDocuments();

				// After saving documents rawDocuments collection will only have incomplete documents.
				// We need to check if we wait for them for too long.
				this.CleanQueue();
			}
			else
			{
				MessageQueue.Create(@".\Private$\" + DOCUMENT_QUEUE);
			}
		}

		private void SaveDocuments()
		{
			var toDelete = new List<string>();
			foreach (var doc in rawDocuments)
			{
				var messages = doc.Value;
				var chunks = messages.Select(x => x.Body as ScannerServiceApi.DocumentPdf).ToList();

				// Check if all chunks are received.
				if (chunks.Count != chunks[0].ChunksCount)
				{
					continue;
				}

				// Chunks supposed to be in order as messages come in order, but just to be sure.
				chunks.OrderBy(x => x.ChunkNumber).Select(x => x.Data);

				List<byte> bytes = new List<byte>();
				foreach (var chunk in chunks)
				{
					bytes.AddRange(chunk.Data);
				}

				// Save document
				File.WriteAllBytes(Path.Combine("documents", chunks[0].Name), bytes.ToArray());

				// Remove messages from queue
				messages.ForEach(x => this.documentQueue.ReceiveByLookupId(x.LookupId));

				toDelete.Add(doc.Key);
			}
			toDelete.ForEach(x => this.rawDocuments.Remove(x));
		}

		private void CleanQueue()
		{
			var toDelete = new List<string>();
			foreach (var doc in rawDocuments)
			{
				var messages = doc.Value;

				if (messages.Last().ArrivedTime < DateTime.Now.AddSeconds(-TIME_OUT))
				{
					// Remove messages from queue
					messages.ForEach(x => this.documentQueue.ReceiveByLookupId(x.LookupId));
				}

				toDelete.Add(doc.Key);
			}
			toDelete.ForEach(x => this.rawDocuments.Remove(x));
		}

		private void ReadStatus(object sender, EventArgs e)
		{
			if (MessageQueue.Exists(@".\Private$\" + STATUS_QUEUE))
			{
				if (this.statusQueue == null)
				{
					this.statusQueue = new MessageQueue(@".\Private$\" + STATUS_QUEUE);
					this.statusQueue.Formatter = new XmlMessageFormatter(new[] { typeof(ScannerServiceApi.ServiceStatus) });
					this.statusQueue.MessageReadPropertyFilter.ArrivedTime = true;
				}

				var enumerator = this.statusQueue.GetMessageEnumerator2();
				while (enumerator.MoveNext())
				{
					var message = enumerator.RemoveCurrent();
					var serviceStatus = message.Body as ScannerServiceApi.ServiceStatus;

					var existing = this.bindingList.FirstOrDefault(x => x.Name == serviceStatus.Name);
					if (existing != null)
					{
						existing.Status = serviceStatus.Status;
						existing.Time = message.ArrivedTime;
						existing.TimeOut = serviceStatus.TimeOut;
					}
					else
					{
						this.bindingList.Add(new ServiceStatusViewModel(
							serviceStatus.Name, serviceStatus.Status, message.ArrivedTime, serviceStatus.TimeOut));
					}
				}
			}
			else
			{
				MessageQueue.Create(@".\Private$\" + STATUS_QUEUE);
			}
		}

		private void SendBtn_Click(object sender, EventArgs e)
		{
			if (MessageQueue.Exists(@".\Private$\" + SETTINGS_QUEUE))
			{
				if (this.settingsQueue == null)
				{
					this.settingsQueue = new MessageQueue(@".\Private$\" + SETTINGS_QUEUE);
					this.settingsQueue.Formatter = new XmlMessageFormatter(new[] { typeof(ScannerServiceApi.Settings) });
				}

				foreach (var scanner in this.bindingList)
				{
					this.settingsQueue.Send(new ScannerServiceApi.Settings
					{
						Name = scanner.Name,
						TimeOut = (double)this.TimeOutUD.Value
					});
				}
			}
			else
			{
				MessageQueue.Create(@".\Private$\" + SETTINGS_QUEUE);
			}
		}
	}
}

