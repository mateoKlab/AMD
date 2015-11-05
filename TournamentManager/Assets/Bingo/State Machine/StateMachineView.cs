using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class StateMachineView : View
    {
        [HideInInspector]
        public StateMachineController controller;

        public void GoBack()
        {
            controller.GoBack();
        }
    }
}
