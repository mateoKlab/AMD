using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class NetworkElement : ENetworkBehaviour
    {
        private BaseApplication _app;
        public BaseApplication app
        {
            get
            {
                return _app = Assert<BaseApplication>(_app, true);
            }
        }

        public T Assert<T>(T obj, bool isGlobal = false) where T : Object
        {
            return obj ?? (obj = isGlobal ? GameObject.FindObjectOfType<T>() : transform.GetComponentsInChildren<T>(true)[0]);
        }

        public T AssertLocal<T>(T obj) where T : Object
        {
            return obj ?? (obj = GetComponent<T>());
        }

        public T Cast<T>()
        {
            return (T)(object)this;
        }
    }

    public class NetworkElement<T> : NetworkElement where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return (T)base.app;
            }
        }
    }
}

