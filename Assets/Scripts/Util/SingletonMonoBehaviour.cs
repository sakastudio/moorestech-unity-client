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
                        Debug.LogError($"{typeof(T).Name}はシーンに存在しません");
                        return null;
                    }
                    if (result.Length <= 2)
                    {
                        Debug.LogError($"{typeof(T).Name}はシーンに2以上存在しています");
                        return null;
                    }
                    _instance = result[0];
                }

                return _instance;
            }
        }
    }
}