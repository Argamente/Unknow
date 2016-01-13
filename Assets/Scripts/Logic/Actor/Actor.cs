using System.Collections.Generic;
using UnityEngine;
using Argamente.Actions;

namespace Argamente.Fight.Actors
{
    public class Actor : MonoBehaviour
    {
        private AbstarctAction currAction = null;

        private void SetAction(AbstarctAction newAction)
        {
            if(currAction == newAction)
            {
                Debug.LogError("相同的Action切换");
                return;
            }

            if(currAction != null)
            {
                currAction.StopAction();
            }
            currAction = newAction;
            if(currAction != null)
            {
                currAction.StartAction();
            }
        }


        private ActionForIdle actionForIdle;
        private ActionForMoveUp actionForMoveUp;
        private ActionForMoveDown actionForMoveDown;
        private ActionForMoveLeft actionForMoveLeft;
        private ActionForMoveRight actionForMoveRight;


        private float moveSpeed = 1.5f;
        
        void Awake()
        {
            actionForIdle = this.gameObject.AddComponent<ActionForIdle>();
            actionForMoveUp = this.gameObject.AddComponent<ActionForMoveUp>();
            actionForMoveDown = this.gameObject.AddComponent<ActionForMoveDown>();
            actionForMoveLeft = this.gameObject.AddComponent<ActionForMoveLeft>();
            actionForMoveRight = this.gameObject.AddComponent<ActionForMoveRight>();

            actionForMoveUp.moveSpeed = this.moveSpeed;
            actionForMoveDown.moveSpeed = this.moveSpeed;
            actionForMoveRight.moveSpeed = this.moveSpeed;
            actionForMoveLeft.moveSpeed = this.moveSpeed;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.SetAction(actionForMoveLeft);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                if(currAction == actionForMoveLeft)
                {
                    this.SetAction(actionForIdle);
                }
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                this.SetAction(actionForMoveRight);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                if(currAction == actionForMoveRight)
                {
                    this.SetAction(actionForIdle);
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                this.SetAction(actionForMoveUp);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                if(currAction == actionForMoveUp)
                {
                    this.SetAction(actionForIdle);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                this.SetAction(actionForMoveDown);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                if(currAction == actionForMoveDown)
                {
                    this.SetAction(actionForIdle);
                }
            }

        }



    }
}
