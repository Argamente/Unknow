using System;


namespace Argamente.Actions 
{
    public interface IAction
    {
        string id
        {
            get;
            set;
        }


        void StartAction();
        void StopAction();
    }
}
