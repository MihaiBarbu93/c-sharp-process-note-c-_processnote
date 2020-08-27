using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote
{
    class Sorter
    {

        public static List<ProcessInf> SortProcesses(List<ProcessInf> stats, string sortMethod)
        {
            if (sortMethod.Equals("IDAscending"))
            {
                stats.Sort((x, y) => x.ID.CompareTo(y.ID));
            }
            else if (sortMethod.Equals("IDDescending"))
            {
                stats.Sort((x, y) => y.ID.CompareTo(x.ID));
            }
            else if (sortMethod.Equals("NameAscending"))
            {
                stats.Sort((x, y) => x.Name.CompareTo(y.Name));
            }
            else if (sortMethod.Equals("NameDescending"))
            {
                stats.Sort((x, y) => y.Name.CompareTo(x.Name));
            }
            else if (sortMethod.Equals("NoteAscending"))
            {
                stats.Sort((x, y) => x.Note.CompareTo(y.Note));
            }
            else if (sortMethod.Equals("NoteDescending"))
            {
                stats.Sort((x, y) => y.Note.CompareTo(x.Note));
            }
            else if (sortMethod.Equals("CPUAscending"))
            {
                stats.Sort((x, y) => x.CPU.CompareTo(y.CPU));
            }
            else if (sortMethod.Equals("CPUDescending"))
            {
                stats.Sort((x, y) => y.CPU.CompareTo(x.CPU));
            }
            else if (sortMethod.Equals("MemoryAscending"))
            {
                stats.Sort((x, y) => x.Memory.CompareTo(y.Memory));
            }
            else if (sortMethod.Equals("MemoryDescending"))
            {
                stats.Sort((x, y) => y.Memory.CompareTo(x.Memory));
            }
            else if (sortMethod.Equals("StartedAscending"))
            {
                stats.Sort((x, y) => x.Started.CompareTo(y.Started));
            }
            else if (sortMethod.Equals("StartedDescending"))
            {
                stats.Sort((x, y) => y.Started.CompareTo(x.Started));
            }
            else if (sortMethod.Equals("ThreadAscending"))
            {
                stats.Sort((x, y) => x.Thread.CompareTo(y.Thread));
            }
            else if (sortMethod.Equals("ThreadDescending"))
            {
                stats.Sort((x, y) => y.Thread.CompareTo(x.Thread));
            }
            else
            {
                stats.Sort((x, y) => y.CPU.CompareTo(x.CPU));
            }

            return stats;
        }
    }
}
