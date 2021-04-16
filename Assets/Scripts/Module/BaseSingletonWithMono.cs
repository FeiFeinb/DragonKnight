using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Module
{
    public class BaseSingletonWithMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                }
                if (instance == null)
                {
                    var tempObj = new GameObject(typeof(T).ToString());
                    instance = tempObj.AddComponent<T>();
                }
                return instance;
            }
        }
    }
}

