using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    public class StateMachineController<T> : StateMachineController where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }

    public class StateMachineController : Controller
    {
        public bool useAnimator = true;
        [HideInInspector]
        public Animator animator;

        public string currentStateName { get; private set; }
        public string previousStateName { get; private set; }

        private string _currentStateMachineName;
        public string currentStateMachineName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentStateMachineName))
                {
                    _currentStateMachineName = "BaseLayer";
                }
                return _currentStateMachineName;
            }
            private set
            {
                _currentStateMachineName = value;
            }
        }

        private string _backResolverName;
        public string backResolverName
        {
            get
            {
                if (string.IsNullOrEmpty(_backResolverName))
                {
                    _backResolverName = "Back";
                }
                return _backResolverName;
            }
            private set
            {
                _backResolverName = value;
            }
        }

        private StateView currentState;
        private StateMachineView stateMachineView;

        private StateMachineGroupManager stateMachineGroupManager;

        protected List<string> stateStack = new List<string>();
        protected Dictionary<string, StateView> callbackMap = new Dictionary<string, StateView>();

        protected void Initialize(StateMachineView view)
        {
            stateMachineView = view;
            view.controller = this;

            if (useAnimator)
            {
                animator = GetComponent<Animator>();
                useAnimator = animator != null;
            }

            if (useAnimator)
            {
                StateBridge[] stateMachines = animator.GetBehaviours<StateBridge>();
                for (int i = 0; i < stateMachines.Length; i++)
                {
                    stateMachines[i].stateMachineController = this;
                }
            }
            else
            {
                stateMachineGroupManager = GetComponent<StateMachineGroupManager>();
            }
        }

        protected void RegisterStates(StateView[] states)
        {
            for (int i = 0; i < states.Length; i++)
            {
                RegisterState(states[i]);
            }
        }

        protected void RegisterState(StateView state)
        {
            state.controller = this;

            StateMachineGroup smg = state.GetComponent<StateMachineGroup>();
            if (smg != null)
            {
                state.stateGroupName = string.IsNullOrEmpty(smg.groupName) ? "BaseLayer" : smg.groupName;
                state.hasGroup = true;
            }

            callbackMap.Add(state.stateName, state);
        }

        protected void RegisterAllStatesInChildren(string initialStateName = null)
        {
            StateView[] sv = stateMachineView != null? stateMachineView.GetComponentsInChildren<StateView>(true) :
                GetComponentsInChildren<StateView>(true);

            for (int i = 0; i < sv.Length; i++)
            {
                RegisterState(sv[i]);
            }

            if (!useAnimator && !string.IsNullOrEmpty(initialStateName))
            {
                if (callbackMap.ContainsKey(initialStateName))
                {
                    EnterState(initialStateName);
                }
            }
        }

        public void SetInitialState(string initialStateName = null)
        {
            if (!useAnimator && !string.IsNullOrEmpty(initialStateName))
            {
                if (callbackMap.ContainsKey(initialStateName))
                {
                    EnterState(initialStateName);
                }
            }
        }

        public void GoBack()
        {
            if (stateStack.Count > 1)
            {
                SetTrigger("Back");
            }
        }

        protected void SetBackResolverState(string stateName)
        {
            backResolverName = stateName;
        }

        public void SetTrigger(string triggerName)
        {
            if (useAnimator)
            {
                animator.SetTrigger(triggerName);
            }
            else
            {
                ExitState(currentStateName);
                EnterState(triggerName);
            }
        }

        private void ChangeStateMachine(string stateMachineName)
        {
            if (!stateMachineName.Equals(currentStateMachineName))
            {
                StateMachineGroupInfo smg = stateMachineGroupManager.FindGroup(currentStateMachineName);
                if (smg != null)
                {
                    ExitStateMachine(currentStateMachineName, smg.clearStateStackOnExit);
                }

                smg = stateMachineGroupManager.FindGroup(stateMachineName);
                if (smg != null)
                {
                    EnterStateMachine(stateMachineName, smg.clearStateStackOnEnter);
                }

            }
        }

        public virtual void EnterStateMachine(string stateMachineName, bool clearStack = false)
        {
            currentStateMachineName = stateMachineName;

            if (clearStack)
            {
                stateStack.Clear();
            }
        }

        public virtual void ExitStateMachine(string stateMachineName, bool clearStack = false)
        {
            currentStateMachineName = "BaseLayer";

            if (clearStack)
            {
                stateStack.Clear();
            }
        }

        public virtual void EnterState(string stateName)
        {
            if (!stateName.Equals(backResolverName))
            {
                ////Log.D("StateMachineController", "Entering state:", stateName);
                StateView sv;
                bool bypassStack = false;
                if (callbackMap.TryGetValue(stateName, out sv))
                {
                    if (!useAnimator && sv.hasGroup)
                    {
                        ChangeStateMachine(sv.stateGroupName);
                    }
                    bypassStack = sv.bypassStack;
                    sv.OnEnterState();
                }

                previousStateName = stateStack.Count > 0 ? stateStack[stateStack.Count - 1] : string.Empty;
                currentStateName = stateName;
                currentState = sv;

                if (!bypassStack)
                {
                    stateStack.Add(stateName);
                }

                //PrintStack();
            }
            else
            {
                ResolveBack();
            }
        }

        public virtual void UpdateState(string stateName)
        {
            if (!stateName.Equals(backResolverName))
            {
                if (currentState != null)
                {
                    currentState.OnUpdateState();
                }
            }
        }

        public virtual void ExitState(string stateName)
        {
            if (!stateName.Equals(backResolverName))
            {
                ////Log.D("StateMachineController", "Exiting state:", stateName);
                StateView sv;
                if (callbackMap.TryGetValue(stateName, out sv))
                {
                    sv.OnExitState();
                }
            }
        }

        void ResolveBack()
        {
            if (stateStack.Count > 1)
            {
                stateStack.RemoveAt(stateStack.Count - 1);
                string triggerName = stateStack[stateStack.Count - 1];
                stateStack.RemoveAt(stateStack.Count - 1);
                ////Log.D("MenuState", "Top of stack:", triggerName);
                SetTrigger(triggerName);
            }
        }

        void Update()
        {
            if (!useAnimator)
            {
                UpdateState(currentStateName);
            }
        }

        void PrintStack()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = stateStack.Count - 1; i >= 0; i--)
            {
                sb.AppendLine(stateStack[i]);
            }
            Debug.Log(sb);
        }
    }
}
