using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class KeyThreadArray
    {
        List<KeyThread> keyThreads; //Starts from 1, ends with 0
        List<bool> keyThreadStatuses; //Again starts from 1
        public KeyThreadArray()
        {
            keyThreads = new List<KeyThread>();
            List<int> keyList = Variables.KeyList();
            for(int i = 0; i < 10; i++)
            {
                keyThreads.Add(new KeyThread(keyList[i]));
            }
            
        }
        public void ChangeChecks(List<bool> checks)
        {
            keyThreadStatuses = checks;
        }
        public void StartAll()
        {
            for(int i = 0; i < keyThreads.Count; i++)
            {
                if(keyThreadStatuses[i]) //Only start checked keys
                {
                    keyThreads[i].Start();
                }
            }
        }
        public void StopAll()
        {
            for (int i = 0; i < keyThreads.Count; i++)
            {
                if (keyThreadStatuses[i]) //Only start checked keys
                {
                    keyThreads[i].Stop();
                }
            }
        }
    }
}
