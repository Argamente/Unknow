using System.Collections.Generic;
using UnityEngine;
using Argamente.Actions;

namespace Argamente.Fight.Actors
{
    public class ActionForIdle : AbstarctAction
    {
        private SpriteAnimation anim;

        private bool isActive = false;

        void Awake()
        {
            anim = this.gameObject.GetComponent<SpriteAnimation>();
        }

        public override void StartAction()
        {
            base.StartAction();
            anim.Stop();
        }
    }
}
