using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
    internal class Time
    {
        //每帧时间(秒)
        public static float deltaTime
        {
            get
            {
                return (float)deltaMilliseconds / 1000;
            }
        }
        //每帧时间（毫秒）
        public static int deltaMilliseconds
        {
            get
            {
                return 20;
            }
        }
    }
}
