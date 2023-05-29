using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class WaitForFrames : IWait
    {
        private int _frames = 0;
        public WaitForFrames(int frames)
        {
            _frames = frames;
        }
        public bool Tick()
        {
            _frames -= 1;
            return _frames <= 0;
        }
    }
}
