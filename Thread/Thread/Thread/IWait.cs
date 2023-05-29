using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
    internal interface IWait
    {
        /// <summary>
        /// 每帧检测是否等待结束
        /// </summary>
        /// <returns></returns>
        bool Tick();
    }
}
