using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bingo
{
    public class StateMachineGroupManager : MonoBehaviour
    {
        public StateMachineGroupInfo[] members;
        [HideInInspector]
        public StateMachineGroupInfo current;
        private Dictionary<string, StateMachineGroupInfo> map;

        void Awake()
        {
            if (members.Length > 0)
            {
                map = new Dictionary<string, StateMachineGroupInfo>();
                for (int i = 0; i < members.Length; i++)
                {
                    map.Add(members[i].name, members[i]);
                }
            }
        }

        public StateMachineGroupInfo FindGroup(string name)
        {
            StateMachineGroupInfo g;
            if (map != null)
            {
                map.TryGetValue(name, out g);
                return g;
            }

            return null;
        }
    }

    [System.Serializable]
    public class StateMachineGroupInfo
    {
        public string name;
        public bool clearStateStackOnEnter;
        public bool clearStateStackOnExit;

    }
}
