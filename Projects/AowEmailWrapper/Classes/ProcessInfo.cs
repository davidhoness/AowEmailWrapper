/***********************************************************************************************
 * File Name	: ProcessInfo.cs
 * Description	: This class demonstrates the use of WMI.
 *				  It provides a static method to query the list of running processes.
 *				  And it provides two events to delegate when an application has been started.
 * 
 * Author		: Asghar Panahy
 * Date			: 26-oct-2005
 ***********************************************************************************************/
using System;
using System.Text;
using System.Data;
using System.Management;
using System.Diagnostics;

namespace AowEmailWrapper.Classes
{
	/// <summary>
	/// ProcessInfo class.
	/// </summary>
	public class ProcessInfo
	{	
		// events to subscribe
		public EventHandler Started = null; 
		public EventHandler Terminated = null;

        private const string QueryStringInstanceTemplate = "TargetInstance.Name = '{0}'";
        private const string QueryStringInstanceOR = " OR ";
        private const string QueryStringTemplateMulti = "SELECT * FROM __InstanceOperationEvent WITHIN {0} WHERE TargetInstance ISA 'Win32_Process' AND ({1})";
        private const string QueryStringTemplate = "SELECT * FROM __InstanceOperationEvent WITHIN {0} WHERE TargetInstance ISA 'Win32_Process' AND TargetInstance.Name = '{1}'";
        private const string QueryScope = @"\\.\root\CIMV2";
        private int count;

		// WMI event watcher
		private ManagementEventWatcher watcher;
		
		// The constructor uses the application name like notepad.exe
		// And it starts the watcher
		public ProcessInfo(string appName, int pollInterval)
		{			
			// create the watcher and start to listen
            count = 0;
            watcher = new ManagementEventWatcher(QueryScope, string.Format(QueryStringTemplate, pollInterval.ToString(), appName));
			watcher.EventArrived += new EventArrivedEventHandler(this.OnEventArrived);			
			watcher.Start();
		}

        public ProcessInfo(string[] appNames, int pollInterval)
        {
            // create the watcher and start to listen
            count = 0;
            watcher = new ManagementEventWatcher(QueryScope, string.Format(QueryStringTemplateMulti, pollInterval.ToString(), BuildTargetInstanceClause(appNames)));
            watcher.EventArrived += new EventArrivedEventHandler(this.OnEventArrived);
            watcher.Start();
        }

		public void Dispose()
		{
			watcher.Stop();
			watcher.Dispose();
		}

        private string BuildTargetInstanceClause(string[] appNames)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < appNames.Length; i++)
            {
                sb.Append(string.Format(QueryStringInstanceTemplate, appNames[i]));
                if (i != appNames.Length - 1)
                {
                    sb.Append(QueryStringInstanceOR);
                }
            }
            return sb.ToString();            
        }
       
        /*
		public static DataTable RunningProcesses()
		{
            //One way of constructing a query
            //string className = "Win32_Process";
            //string condition = "";
            //string[] selectedProperties = new string[] {"Name", "ProcessId", "Caption", "ExecutablePath"};
            //SelectQuery query = new SelectQuery(className, condition, selectedProperties);
			
			// The second way of constructing a query
			string queryString = 
				"SELECT Name, ProcessId, Caption, ExecutablePath" + 
				"  FROM Win32_Process";
								
			SelectQuery query = new SelectQuery(queryString);
			ManagementScope scope = new System.Management.ManagementScope(@"\\.\root\CIMV2");
			
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
			ManagementObjectCollection processes = searcher.Get();
			
			DataTable result = new DataTable();
			result.Columns.Add("Name", Type.GetType("System.String"));
			result.Columns.Add("ProcessId", Type.GetType("System.Int32"));
			result.Columns.Add("Caption", Type.GetType("System.String"));
			result.Columns.Add("Path", Type.GetType("System.String"));
			
			foreach(ManagementObject mo in processes)
			{
				DataRow row = result.NewRow();
				row["Name"] = mo["Name"].ToString();
				row["ProcessId"] = Convert.ToInt32(mo["ProcessId"]);
				if (mo["Caption"]!= null)
					row["Caption"] = mo["Caption"].ToString();
				if (mo["ExecutablePath"]!= null)
					row["Path"] = mo["ExecutablePath"].ToString();
				result.Rows.Add( row );
			}
			return result;
		}
        */

		private void OnEventArrived(object sender, System.Management.EventArrivedEventArgs e)
		{
			try 
			{
				string eventName = e.NewEvent.ClassPath.ClassName;

				if (eventName.CompareTo("__InstanceCreationEvent")==0)
				{
					// Started
                    if (Started != null && count == 0)
                    {
                        Started(this, e);
                    }

                    count++;
				}
				else if (eventName.CompareTo("__InstanceDeletionEvent")==0)
				{
					// Terminated
                    count = (count > 0) ? count - 1 : 0;

                    if (Terminated != null && count == 0)
                    {
                        Terminated(this, e);
                    }
				}
			}
			catch (Exception ex)
			{
                Trace.TraceError(ex.Message);
                Trace.TraceInformation(ex.StackTrace);
                Trace.Flush();
			}
		}
		
	}
}
