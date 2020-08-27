using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote
{
    public class ProcessInf
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public long CPU { get; set; }
        public long Memory { get; set; }
        public string Started { get; set; }
        public long Thread { get; set; }

        public static List<ProcessInf> Stats { get; set; }
        public static Dictionary<int, long> History = new Dictionary<int, long>();
        public static Dictionary<int, string> Notes = new Dictionary<int, string>();

       
        public static async Task PopulateStats()
        {
            List<ProcessInf> result = new List<ProcessInf>();
            Process[] remoteAll = await Task.Run(() => Process.GetProcesses());
            foreach (var item in remoteAll)
            {
          
                PerformanceCounter myAppCpu = new PerformanceCounter(
                "Process", "% Processor Time", item.ProcessName, true);
                PerformanceCounter myAppRam = new PerformanceCounter("Process", "ID Process", item.ProcessName, true);

                long cpu = (long)myAppCpu.NextValue();


                string startTime = "00";
                try
                {
                    startTime = Convert.ToString(item.StartTime);
                }
                catch (Exception e)
                {

                    startTime = "6/15/2020 8:45:61 PM";
                }
                result.Add(new ProcessInf()
                {
                    ID = item.Id,
                    Name = item.ProcessName,
                    Note = verifyNote(item.Id),
                    CPU = cpu,
                    Memory = Convert.ToInt64(item.WorkingSet64),
                    Started = startTime,
                    Thread = Convert.ToInt64(item.Threads.Count)
                });
            }
            Stats = result;
            populateHistory(result);
        }

        private static string verifyNote(int id)
        {
            string note = "...";
            if (Notes.ContainsKey(id))
            {
                note = Notes[id];
            }
            return note;
        }

       
        private static bool populateHistory(List<ProcessInf> result)
        {
            foreach (var item in result)
            {
                if (History.ContainsKey(item.ID))
                {
                    History[item.ID] = item.CPU;
                }
                else
                {
                    History.Add(item.ID, item.CPU);
                }
            }
            Console.WriteLine("history populated");
            return true;
        }

        private static long findPreviousCPUValue(int id)
        {
            long tempResult = 0;
            if (History.ContainsKey(id))
            {
                tempResult = History[id];
            }
            return tempResult;
        }
    }
}
