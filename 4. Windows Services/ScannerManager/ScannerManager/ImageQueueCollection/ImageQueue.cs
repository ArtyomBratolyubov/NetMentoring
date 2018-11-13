using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using ZXing;

namespace ScannerManager.ImageQueueCollection
{
	public class ImageQueue
	{
		private static string END_DOCUMENT = "end document";
		private List<Queue<QImage>> queues;
		private Queue<QImage> currentQueue;
		private BarcodeReader barcodeReader;

		private int currentNumber = -1;

		public ImageQueue()
		{
			this.queues = new List<Queue<QImage>>();
			this.currentQueue = new Queue<QImage>();
			this.barcodeReader = new BarcodeReader();
		}

		public int CurrentNumber
		{
			get
			{
				return this.currentNumber;
			}
		}

		public void AddImage(QImage img)
		{
			if (img.Number <= this.currentNumber)
			{
				return;
			}

			string res = this.CheckQRCode(img.Image);

			if (res == END_DOCUMENT)
			{
				if (this.currentQueue.Count != 0)
				{
					img.IsSeparator = true;

					// No need to check if image number is greater by 1 or not, anyway it leads to creating a new queue.
					this.currentQueue.Enqueue(img);

					this.queues.Add(this.currentQueue);
					this.currentQueue = new Queue<QImage>();
				}
			}
			else
			{
				if (this.currentQueue.Count == 0)
				{
					// If current queue is empty that just add new image.
					this.currentQueue.Enqueue(img);
				}
				else
				{
					if (img.Number - this.currentNumber == 1)
					{
						// If image number is next to previous then just add it to current queue.
						this.currentQueue.Enqueue(img);
					}
					else
					{
						// Otherwise create new queue.
						this.queues.Add(this.currentQueue);
						this.currentQueue = new Queue<QImage>();
						this.currentQueue.Enqueue(img);
					}
				}
			}

			this.currentNumber = img.Number;
		}

		public List<Queue<QImage>> Flush()
		{
			var res = this.queues;
			this.queues = new List<Queue<QImage>>();

			// If we have no other images we can reset current number so new queues can have any starting number.
			if(this.currentQueue.Count == 0)
			{
				this.currentNumber = -1;
			}

			return res;
		}

		public Queue<QImage> FlushCurrent()
		{
			this.currentNumber = -1;
			var res = this.currentQueue;
			this.currentQueue = new Queue<QImage>();

			return res;
		}

		private string CheckQRCode(Bitmap img)
		{
			var barcodeResult = this.barcodeReader.Decode(img);

			return barcodeResult?.Text;
		}
	}
}
