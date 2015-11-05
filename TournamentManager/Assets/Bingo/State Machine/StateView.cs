using UnityEngine;

namespace Bingo
{
    public class StateView<T> : StateView where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }

    public class StateView : View
    {
        public string stateName;
        [HideInInspector]
        public string stateGroupName;
        [HideInInspector]
        public bool hasGroup;
        public bool bypassStack;

        [HideInInspector]
        public StateMachineController controller;

        public virtual void OnEnterState() { gameObject.SetActive(true); }
        public virtual void OnUpdateState() { }
        public virtual void OnExitState() { gameObject.SetActive(false); }

        public void SwitchState()
        {
            controller.SetTrigger(stateName);
        }

        public void GoBack()
        {
            controller.GoBack();
        }
    }
}
