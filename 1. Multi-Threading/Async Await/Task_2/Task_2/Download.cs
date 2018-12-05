using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
	public class Download : IDisposable, INotifyPropertyChanged
	{
		public static string StateLoading = "Loading..";
		public static string StateSuccess = "Success";
		public static string StatePending = "Pending";
		public static string StateError = "Error";
		public static string StateCanceled = "Canceled";
		public static string StateForbidden = "Forbidden";

		private string status;

		public string URL { get; private set; }

		public string Status
		{
			get { return this.status; }
			private set
			{
				this.status = value;
				this.OnPropertyChanged("Status");
			}
		}

		private WebClient WebClient;

		public event PropertyChangedEventHandler PropertyChanged;

		public Download(string url)
		{
			this.WebClient = new WebClient();

			this.URL = url;
			this.Status = StatePending;
		}

		public async Task<string> LoadWebSiteAsync()
		{
			string downloadString;

			try
			{
				this.Status = StateLoading;
				downloadString = await this.WebClient.DownloadStringTaskAsync(this.URL);
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.RequestCanceled)
				{
					this.Status = StateCanceled;
				}
				else if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					this.Status = StateForbidden;
				}
				else
				{
					this.Status = StateError;
				}

				return null;
			}
			this.Status = StateSuccess;
			return downloadString;
		}

		public void CancelLoading()
		{
			this.WebClient.CancelAsync();
		}

		private void OnPropertyChanged(string name)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public void Dispose()
		{
			this.WebClient.Dispose();
		}
	}
}
