using System;
using UnityEngine;
using System.Collections.Generic;

namespace Bingo
{
    [Serializable]
    public class MessengerData : ScriptableObject
    {
        [NonSerialized]
        public static readonly string ASSET_PATH = string.Concat(InternalConstants.DATA_ASSET_PATH, "EventTags.asset");

        [HideInInspector]
        public List<MessengerEventTag> eventTypes = new List<MessengerEventTag>();

        void OnEnable()
        {
            hideFlags = HideFlags.HideInInspector;
        }
    }

    [Serializable]
    public class MessengerEventTag : ICloneable
    {
        public string hash;
        public string name;

        public MessengerEventTag()
        {
            hash = Guid.NewGuid().ToString();
        }

        public object Clone()
        {
            MessengerEventTag m = new MessengerEventTag();
            m.name = name;
            m.hash = hash;
            return m;
        }
    }
}
