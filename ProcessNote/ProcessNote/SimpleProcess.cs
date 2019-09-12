using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ProcessNote
{
    class SimpleProcess
    {

        public int PID { get { return process.Id; } }
        public string Name { get { return process.ProcessName; } }
        public string StartTime { get; }
        public string ElapsedTime { get
            {
                try
                {
                    return process.UserProcessorTime.ToString();
                }
                catch (Exception e)
                {
                    return "N/A";
                }
            }
        }
        public ProcessThread[] ThreadsList { get { return process.Threads.Cast<ProcessThread>().ToArray(); } }

        private DateTime lastTime;
        private TimeSpan lastTotalProcessorTime;
        private Process process;

        public SimpleProcess(Process process)
        {
            try
            {
                StartTime = process.StartTime.ToString("HH:mm:ss");
            }
            catch(Exception e)
            {
                StartTime = "N/A";
            }
            
            lastTime = DateTime.Now;
            try
            {
                lastTotalProcessorTime = process.TotalProcessorTime;
            }
            catch(Exception e)
            {
                lastTotalProcessorTime = TimeSpan.Zero;
            }
            this.process = process;
        }

        public string getCPU_Usage()
        {
            TimeSpan curTotalProcessorTime;
            try
            {
                curTotalProcessorTime = process.TotalProcessorTime;
            }
            catch(Exception e)
            {
                return "N/A";
            }
            DateTime curTime = DateTime.Now;
            string instanceName = getInstanceName();
            if (instanceName.Equals("N/A"))
            {
                return "N/A";
            }
            double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds)
                / curTime.Subtract(lastTime).TotalMilliseconds
                / Convert.ToDouble(Environment.ProcessorCount);

            lastTime = curTime;
            lastTotalProcessorTime = curTotalProcessorTime;
            return (CPUUsage).ToString("P");
        }

        public string getRamUsage()
        {
            string instanceName = getInstanceName();
            if (instanceName.Equals("N/A"))
            {
                return "N/A";
            }
            var ramUsage = new PerformanceCounter("Process", "Working Set - Private", instanceName, true);
            return (ramUsage.NextValue() / 1048576).ToString("0.00") + " MB";
        }

        private string getInstanceName()
        {
            foreach (var instanceName in new PerformanceCounterCategory("Process").GetInstanceNames())
            {
                if (instanceName.StartsWith(Name))
                {
                    using (var processPerformance = new PerformanceCounter("Process", "ID Process", instanceName, true))
                    {
                        try
                        {
                            if (PID == (int)processPerformance.RawValue)
                            {
                                return instanceName;
                            }
                        }
                        catch(InvalidOperationException e)
                        {
                            return "N/A";
                        }
                            
                    }
                }
            }
            return "N/A";
        }
    }
}
