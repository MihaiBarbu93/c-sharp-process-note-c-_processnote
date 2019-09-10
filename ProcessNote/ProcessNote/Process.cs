using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote
{
    class Process
    {
        public int PID { get; set; }
        public string Name { get; set; }
       
        public static void getProcesses(List<Process> _processes)
        {
            System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process theprocess in processlist)
            {
                _processes.Add(new Process { PID = theprocess.Id, Name = theprocess.ProcessName });
            }

        }
}
}
