using System;
using System.Collections.Generic;
using UnityEngine;

namespace Argamente.Actions
{
    public class AbstarctAction : MonoBehaviour, IAction
    {

        private string _id = "";

        public string id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }


        public virtual void StartAction()
        {

        }

        public virtual void StopAction()
        {

        }

    }
}
