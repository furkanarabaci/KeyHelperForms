using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class ProcessThread
    {
        bool state = false; //This will be changed by Start() and Stop()
        Thread procThread;
        Action functionToInvoke;
        public ProcessThread(Action function)
        {
            functionToInvoke = function;
            RenewThread();
            Start(); //You might want to remove this. It starts itself cuz we want this thread to always be working.
        }
        private void RenewThread()
        {
            procThread = new Thread(new ThreadStart(MainThread))
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal //No need to overtire CPU
                
            };
        }
        private void MainThread()
        {
            while (true)
            {
                functionToInvoke();
                Thread.Sleep(Variables.processThreadSleep);
            }
        }
        public bool IsThreadWorking()
        {
            return procThread.IsAlive;
        }
        public void Start()
        {
            if (!state)
            {
                RenewThread();
                procThread.Start();
            }
            state = true;
        }
        public void Stop()
        {
            if (state)
            {
                procThread.Abort();
            }
            state = false;
        }
    }
}
