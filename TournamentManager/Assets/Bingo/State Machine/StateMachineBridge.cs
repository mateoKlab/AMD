using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class StateMachineBridge : StateBridge
    {
        public bool clearStateStackOnEnter;
        public bool clearStateStackOnExit;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            stateMachineController.EnterStateMachine(stateName, clearStateStackOnEnter);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            stateMachineController.ExitStateMachine(stateName, clearStateStackOnExit);
        }
    }

}
