using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class, new()
{
    private static T t = default(T);//对泛型设定默认值

    /// <summary>
    /// 获取泛型单例
    /// </summary>
    public static T Instance
    {
        get
        {
            if (t == null)
            {
                t = new T();
            }
            return t;
        }
    }
}

