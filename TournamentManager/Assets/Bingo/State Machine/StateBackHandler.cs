using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bingo
{
    [RequireComponent(typeof(Button))]
    public class StateBackHandler : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            StateMachineView stateMachineView = GetComponent<StateMachineView>();
            if (stateMachineView == null)
            {
                stateMachineView = GetComponentInParent<StateMachineView>();
            }

            if (stateMachineView != null)
            {
                GetComponent<Button>().onClick.AddListener(() => stateMachineView.controller.GoBack());
            }
        }
    }
}
