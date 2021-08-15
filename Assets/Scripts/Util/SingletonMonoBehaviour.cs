using System;
using System.Linq;
using UnityEngine;

namespace Util
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var result = FindObjectsOfType<T>();
                    if (result.Length == 0)
                    {
                        throw new Exception($"{typeof(T).Name}はシーンに存在しません");
                    }
                    if (result.Length != 1)
                    {
                        result.ToList().ForEach(r => Debug.LogError(r.name));
                        throw new Exception($"{typeof(T).Name}はシーンに2以上存在しています");
                    }
                    _instance = result[0];
                }

                return _instance;
            }
        }
    }
}