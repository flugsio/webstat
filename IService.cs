using System;
using System.Collections.Generic;
using System.Text;

namespace webstat
{
	public delegate void LogEvent(string log_message, System.Diagnostics.EventLogEntryType type);

	public enum Status { None, OK, Error, Info, Warning1, Warning2 }

	public interface IService
	{
		string Name { get; set; }

		int Percent { get; }

		Status Status {	get; set; }
		
		event EventHandler StatusChanged;

		event LogEvent LogEvent;

		void Start();
		void Stop();
		void Tick();
	}
}
