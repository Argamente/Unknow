using System;
using UnityEngine;
using Argamente.Fight.Data;

namespace Argamente.Fight
{
    public class Main : MonoBehaviour
    {
        void Start ()
        {
            GameObject explorer = GameObject.FindGameObjectWithTag ("Player");
            WorldManager.GetInstance ().Init (explorer.transform);
            WorldManager.GetInstance ().StartWorld ();
        }
        
    }
}

