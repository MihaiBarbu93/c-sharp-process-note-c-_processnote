using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessNote
{
    class ThreadModel
    {

        private List<ProcessThread> processThreadsList;

        public ThreadModel()
        {
            processThreadsList = new List<ProcessThread>();
        }

        public void GetAllProcessThreads(int id)
        {
            ProcessThreadCollection threads = Process.GetProcessById(id).Threads;
            foreach (ProcessThread thread in threads)
            {
                processThreadsList.Add(thread);
            }
        }

        public List<ProcessThread> GetThreadsList()
        {
            return processThreadsList;
        }

    }
}
