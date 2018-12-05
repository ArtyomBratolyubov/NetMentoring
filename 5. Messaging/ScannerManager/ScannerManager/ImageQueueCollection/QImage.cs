using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager.ImageQueueCollection
{
	public class QImage : IDisposable
	{
		private Bitmap image;
		private bool disposed = false;

		public QImage(FileInfo fileInfo, int number)
		{
			this.FileInfo = fileInfo;

			this.Number = number;
		}

		public bool IsSeparator { get; set; }

		public FileInfo FileInfo { get; private set; }

		public int Number { get; private set; }

		public Bitmap Image
		{
			get
			{
				if (this.image == null)
				{
					try
					{
						this.image = new Bitmap(this.FileInfo.FullName);
					}
					catch (Exception)
					{
						this.CreateMockImage();
					}
				}
				return this.image;
			}
		}

		private void CreateMockImage()
		{
			Font font = new Font("Arial", 12f);
			//first, create a dummy bitmap just to get a graphics object
			Bitmap img = new Bitmap(1, 1);
			Graphics drawing = Graphics.FromImage(img);

			//free up the dummy image and old graphics object
			img.Dispose();
			drawing.Dispose();

			//create a new image of the right size
			img = new Bitmap(500, 500);

			drawing = Graphics.FromImage(img);

			//paint the background
			drawing.Clear(Color.White);

			//create a brush for the text
			Brush textBrush = new SolidBrush(Color.Black);

			drawing.DrawString(this.FileInfo.Name, font, textBrush, 0, 0);

			drawing.Save();

			textBrush.Dispose();
			drawing.Dispose();

			this.image = img;
		}

		public void Dispose()
		{
			if (this.image != null)
			{
				this.image.Dispose();
			}
		}
	}
}
