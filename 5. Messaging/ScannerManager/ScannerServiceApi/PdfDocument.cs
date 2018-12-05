using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerServiceApi
{
	[Serializable]
	public class DocumentPdf
	{
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public int ChunkNumber { get; set; }

		public int ChunksCount { get; set; }

		public string Id { get; set; }
	}
}
