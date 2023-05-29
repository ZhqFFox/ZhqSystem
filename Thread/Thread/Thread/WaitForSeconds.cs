using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class WaitForSeconds : IWait
    {
        float _seconds = 0f;
        public WaitForSeconds(float seconds) 
        { 
            _seconds = seconds;
        }
        public bool Tick()
        {
           _seconds-=Time.deltaTime;

            return _seconds <= 0;
        }
    }
}
