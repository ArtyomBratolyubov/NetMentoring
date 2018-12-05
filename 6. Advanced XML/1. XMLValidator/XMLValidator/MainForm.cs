using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

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

		private void ValidateBtn_Click(object sender, EventArgs e)
		{
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
	}
}
