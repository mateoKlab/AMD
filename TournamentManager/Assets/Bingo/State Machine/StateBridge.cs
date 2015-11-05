using UnityEngine;

namespace Bingo
{
    public class StateBridge : StateMachineBehaviour
    {
        [HideInInspector]
        public StateMachineController stateMachineController;
        public string stateName;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateMachineController.EnterState(stateName);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateMachineController.UpdateState(stateName);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            stateMachineController.ExitState(stateName);
        }
    }

}
