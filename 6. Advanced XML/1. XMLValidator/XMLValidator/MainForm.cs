using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace XMLValidator
{
	public partial class MainForm : Form
	{

		public MainForm()
		{
			InitializeComponent();
		}

		private void BrowseXmlBtn_Click(object sender, EventArgs e)
		{
			this.openFileDialog.Filter = "XML Files|*.xml";
			this.openFileDialog.Title = "Select a XML document";
			this.openFileDialog.FileName = "";
			if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.XmlTB.Text = this.openFileDialog.FileName;
			}
		}

		private void BrowseXsdBtn_Click(object sender, EventArgs e)
		{
			this.openFileDialog.Filter = "XSD Files|*.xsd";
			this.openFileDialog.Title = "Select a XSD scheme";
			this.openFileDialog.FileName = "";
			if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.XsdTB.Text = this.openFileDialog.FileName;
			}
		}

		private void BrowseXsltBtn_Click(object sender, EventArgs e)
		{
			this.openFileDialog.Filter = "XSLT Files|*.xslt";
			this.openFileDialog.Title = "Select a XSLT template";
			this.openFileDialog.FileName = "";
			if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.XsltTB.Text = this.openFileDialog.FileName;
			}
		}

		private void ValidateBtn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.XmlTB.Text))
			{
				this.OutputTB.Text = "Choose XML file";
				return;
			}

			if (string.IsNullOrEmpty(this.XsdTB.Text))
			{
				this.OutputTB.Text = "Choose XSD scheme";
				return;
			}


			XmlSchemaSet schema = new XmlSchemaSet();
			schema.Add(null, this.XsdTB.Text);

			XmlReader rd = XmlReader.Create(this.XmlTB.Text);
			XDocument doc = XDocument.Load(rd);

			this.OutputTB.Text = "Valid";
			doc.Validate(schema, ValidationEventHandler, true);
		}

		private void ValidationEventHandler(object sender, ValidationEventArgs ve)
		{
			if (ve.Severity == XmlSeverityType.Error || ve.Severity == XmlSeverityType.Warning)
			{
				var el = sender as XElement;

				// Search for parent book.
				var book = el.Ancestors(el.Name.Namespace + "book").FirstOrDefault();

				this.OutputTB.Text = (book != null ? "Book " + book.Attribute("id") + "\r\n" : "") + ve.Exception.Message;
			}
		}

		private void ConvertBtn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.XmlTB.Text))
			{
				this.OutputTB.Text = "Choose XML file";
				return;
			}

			if (string.IsNullOrEmpty(this.XsltTB.Text))
			{
				this.OutputTB.Text = "Choose XSLT template";
				return;
			}

			if (string.IsNullOrEmpty(this.FileNameTB.Text))
			{
				this.OutputTB.Text = "Choose destination file name";
				return;
			}

			XslCompiledTransform xslt = new XslCompiledTransform();
			xslt.Load(this.XsltTB.Text);

			string path = Path.GetDirectoryName(this.XmlTB.Text);
			var newFile = Path.Combine(path, this.FileNameTB.Text);
			xslt.Transform(this.XmlTB.Text, newFile);

			this.OutputTB.Text = "Done\r\n" + newFile;
		}
	}
}
