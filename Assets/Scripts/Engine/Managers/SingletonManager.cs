using UnityEngine;
using System;

namespace Argamente.Managers
{
    public class SingletonManager
    {
        private static GameObject _instance = null;

        private static GameObject GetInstance ()
        {
            if (_instance == null)
            {
                _instance = new GameObject ("SigletonManager");
                GameObject.DontDestroyOnLoad (_instance);
            }
            return _instance;
        }



        public static T AddComponent<T> () where T : Component
        {
            return GetInstance ().AddComponent<T> ();
        }

        public static T GetComponent<T> () where T : Component
        {
            return (T)(GetInstance ().GetComponent<T> ());
        }

        public static void RemoveComponent<T> () where T : Component
        {
            GameObject.Destroy (GetInstance ().GetComponent<T> ());
        }



    }




}

