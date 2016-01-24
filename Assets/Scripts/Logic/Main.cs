using System;
using UnityEngine;
using Argamente.Fight.Data;
using UnityEngine.UI;

namespace Argamente.Fight
{
    public class Main : MonoBehaviour
    {
        public Text unitIndexText;

        void Start ()
        {
            GameObject explorer = GameObject.FindGameObjectWithTag ("Player");
            WorldManager.GetInstance ().Init (explorer.transform);
            WorldManager.GetInstance ().StartWorld ();
        }


        void Update ()
        {
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                Application.Quit ();
            }

            unitIndexText.text = "当前区块: " + WorldManager.GetInstance().currUnitIndexX + "  " + WorldManager.GetInstance().currUnitIndexY;
        }
    }
}

