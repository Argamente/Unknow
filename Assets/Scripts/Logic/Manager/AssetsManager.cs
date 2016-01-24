using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Argamente.Utils;

namespace Argamente.Fight.Managers
{
    public class AssetsManager : MonoBehaviour
    {

        private static AssetsManager _instance = null;

        public static AssetsManager GetInstance ()
        {
            if (_instance == null)
            {
                InitAssetsManager ();
            }

            return _instance;
        }


        private static void InitAssetsManager ()
        {
            GameObject obj = new GameObject ();
            obj.name = "AssetsManager";
            DontDestroyOnLoad (obj);
            _instance = obj.AddComponent<AssetsManager> ();
        }


        Dictionary<string,Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>> ();




        public GameObject InstantiateGameObject (string path, string name = "")
        {
            GameObject obj = null;

            if (pool.ContainsKey (path))
            {
                if (pool [path].Count > 0)
                {
                    obj = pool [path].Dequeue ();
                    obj.SetActive (true);
                }
                else
                {
                    obj = Instantiate (Resources.Load (path)) as GameObject; 
                }
            }
            else
            {
                obj = Instantiate (Resources.Load (path)) as GameObject;
            }


            if (obj != null)
            {
                if (!string.IsNullOrEmpty (name))
                {
                    obj.name = name;
                }
            }
            else
            {
                Log.Error (this, "GameObject path is Null:", path);
            }

            return obj;
        }


        public void DestroyGameObject (string name, GameObject obj)
        {
            if (obj != null)
            {
                if (!pool.ContainsKey (name))
                {
                    pool.Add (name, new Queue<GameObject> ());
                }

                pool [name].Enqueue (obj);
                obj.SetActive (false);
            }
        }

    }
}

