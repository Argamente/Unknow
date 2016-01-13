using System.Collections.Generic;
using UnityEngine;
using Argamente.Actions;
namespace Argamente.Fight.Actors
{
    public class ActionForMoveDown : AbstarctAction
    {
        public float moveSpeed = 1.0f;

        private Transform m_Transform;
        private SpriteAnimation anim;

        private bool isActive = false;

        void Awake()
        {
            m_Transform = this.gameObject.GetComponent<Transform>();
            anim = this.gameObject.GetComponent<SpriteAnimation>();
        }


        public override void StartAction()
        {
            base.StartAction();
            isActive = true;
            anim.Play("Down");
        }


        public override void StopAction()
        {
            base.StopAction();
            isActive = false;
        }



        void Update()
        {
            if (!isActive)
            {
                return;
            }

            Vector3 currPos = m_Transform.position;
            currPos.y -= moveSpeed * Time.deltaTime;
            m_Transform.position = currPos;
        }
    }
}

