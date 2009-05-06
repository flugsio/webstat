using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace webstat
{
	public partial class WebView : UserControl
	{
		private IService service;

        public IService Service { get { return service; } }

		public WebView()
		{
			InitializeComponent();
		}

		public WebView(IService service)
		{
			InitializeComponent();

			this.service = service;

			label1.Text = service.Name;
			service.StatusChanged += new EventHandler(service_StatusChanged);
		}

		void service_StatusChanged(object sender, EventArgs e)
		{
			//this.statusBox.Checked = service.Status == Status.OK ? true : false;

			label2.Text = service.Percent.ToString() + "%";

			switch (service.Status)
			{
				case Status.None:
					pictureBox1.Image = null;
					break;
				case Status.OK:
					pictureBox1.Image = ((Form1)this.ParentForm).imageList1.Images[0];
					break;
				case Status.Error:
					pictureBox1.Image = ((Form1)this.ParentForm).imageList1.Images[1];
					break;
				case Status.Info:
					pictureBox1.Image = ((Form1)this.ParentForm).imageList1.Images[2];
					break;
				case Status.Warning1:
					pictureBox1.Image = ((Form1)this.ParentForm).imageList1.Images[3];
					break;
				case Status.Warning2:
					pictureBox1.Image = ((Form1)this.ParentForm).imageList1.Images[4];
					break;
				default:
					break;
			}

		}
	}
}
