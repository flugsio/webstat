using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace webstat
{
	public partial class Form1 : Form
	{
		List<IService> services = new List<IService>();

        private IService selectedWebViewService;

		public Form1()
		{
			InitializeComponent();

			services.Add(new PingService("Localhost", "127.0.0.1"));
			//services.Add(new PingService("Server", "192.168.0.102"));
            services.Add(new PingService("Google", "216.239.59.104"));
			services.Add(new PingService("Blixtvik29", "87.96.213.29"));
			services.Add(new PingService("Blixtvik30", "87.96.213.30"));

			services.Add(new PingService("Dimea FIRE", "212.181.94.34"));
			services.Add(new PingService("Dimea DNS", "212.181.94.36"));
			services.Add(new PingService("Dimea WEB", "212.181.94.40"));
		}

		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StartServices();
		}

		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StopServices();
		}

		private void serviceTick_Tick(object sender, EventArgs e)
		{
			TickServices();
		}

		private void StartServices()
		{
			startToolStripMenuItem.Enabled = false;

			panel1.Controls.Clear();

			foreach (IService service in services)
			{
                WebView webView = new WebView(service);
				panel1.Controls.Add(webView);
				service.Start();
				service.LogEvent += new LogEvent(service_LogEvent);
                webView.Click += new EventHandler(webView_Click);
			}

			serviceTick.Start();

			stopToolStripMenuItem.Enabled = true;
		}

        void webView_Click(object sender, EventArgs e)
        {
            SwitchSelectedWebView(sender);
        }

        private void SwitchSelectedWebView(object sender)
        {
            selectedWebViewService = ((WebView)sender).Service;
            UpdateSelectedWebView();
        }

        private void UpdateSelectedWebView()
        {
            if (selectedWebViewService != null)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(selectedWebViewService.Name);
                foreach (IServiceTickEvent tickEvent in selectedWebViewService.TickEvents)
                {
                    listBox1.Items.Insert(1, tickEvent.Timestamp.ToString() + " " + tickEvent.Status.ToString());
                }
            }
        }

		void service_LogEvent(string log_message, System.Diagnostics.EventLogEntryType type)
		{
			//eventLogger.WriteEntry(log_message, type);
		}

		private void StopServices()
		{
			stopToolStripMenuItem.Enabled = false;
			serviceTick.Stop();

			foreach (IService service in services)
			{
				service.Stop();
			}

			panel1.Controls.Clear();

			startToolStripMenuItem.Enabled = true;
		}

		private void TickServices()
		{
			foreach (IService service in services)
			{
				service.Tick();
			}
            UpdateSelectedWebView();
		}


	}
}