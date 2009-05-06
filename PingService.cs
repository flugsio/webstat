using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Text;

namespace webstat
{
	public class PingService : IService
	{
		private int total_count = 0;
		private int total_success = 0;

		public bool Started = false;

		public PingService()
		{

		}

		public PingService(string name, string ip)
		{
			Name = name;
			IP = ip;
		}

		public int Percent
		{
			get
			{
				if (total_count > 0)
					return total_success * 100 / total_count;
				else
					return -1;
			}
		}

		private string _Name = "PingService";

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string IP = "127.0.0.1";

		private Status _Status;

		public Status Status
		{
			get { return _Status; }
			set
			{
				if (Started)
				{
					total_count++;
					if (value == Status.OK)
						total_success++;
				}

				_Status = value;
				OnStatusChanged(null);
			}
		}

		public event EventHandler StatusChanged;

		protected virtual void OnStatusChanged(EventArgs e)
		{
			if (StatusChanged != null)
				StatusChanged(this, e);
		}

		public event LogEvent LogEvent;

		protected virtual void OnLogEvent(string log_message,System.Diagnostics.EventLogEntryType type)
		{
			if (LogEvent != null)
				LogEvent(log_message, type);
		}

		public void Start()
		{
			if (Started)
				Stop();

			Status = Status.None;
			Started = true;
			DoPing();
		}

		public void Stop()
		{
			Started = false;
			Status = Status.None;
		}

		public void Tick()
		{
			DoPing();
		}

		private void DoPing()
		{
			Ping pinger = new Ping();
			PingOptions options = new PingOptions();

			options.DontFragment = true;

			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);
			int timeout = 120;

			PingReply reply = pinger.Send(IP, timeout, buffer, options);

			if (reply.Status == IPStatus.Success)
				Status = Status.OK;
			else
			{
				Status = Status.Error;

				OnLogEvent(string.Format("{0} did not answer ping with IP: {1}, Result: {2}", Name, IP, reply.Status), System.Diagnostics.EventLogEntryType.Error);
			}
		}
	}
}
