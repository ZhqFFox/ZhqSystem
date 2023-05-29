using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = Test01();
            var t2 = Test02();
            CoroutineManager.Instance.StartCoroutine(t1);
            CoroutineManager.Instance.StartCoroutine(t2);

            while (true)// 模拟update
            {
              
                Thread.Sleep(Time.deltaMilliseconds);
                CoroutineManager.Instance.UpdateCoroutine();
            }
        }
        static IEnumerator Test01()
        {
            Console.WriteLine("第一段测试");
            yield return new WaitForSeconds(2);
            Console.WriteLine("经过了2秒");
            yield return new WaitForSeconds(3);
            Console.WriteLine("经过了3秒");
        }

        static IEnumerator Test02()
        {
            Console.WriteLine("第二段测试");
            yield return new WaitForFrames(500);
            Console.WriteLine("经过了500帧");
            yield return null;
            Console.WriteLine( "测试");
        }
    }
}
