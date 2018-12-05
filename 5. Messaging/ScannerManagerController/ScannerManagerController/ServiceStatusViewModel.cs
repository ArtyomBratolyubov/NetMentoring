using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManagerController
{
	public class ServiceStatusViewModel : INotifyPropertyChanged
	{
		public ServiceStatusViewModel(string name, string status, DateTime time, double timeOut)
		{
			this.Name = name;
			this.status = status;
			this.time = time;
			this.timeOut = timeOut;
		}

		public string Name { get; set; }

		private DateTime time;
		public DateTime Time {
			get { return this.time; }
			set
			{
				this.time = value;
				this.OnPropertyChanged(nameof(Time));
			}
		}

		private string status;
		public string Status
		{
			get { return this.status; }
			set
			{
				this.status = value;
				this.OnPropertyChanged(nameof(Status));
			}
		}

		private double timeOut;
		public double TimeOut {
			get { return this.timeOut; }
			set
			{
				this.timeOut = value;
				this.OnPropertyChanged(nameof(TimeOut));
			}
		}

		private void OnPropertyChanged(string name)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
