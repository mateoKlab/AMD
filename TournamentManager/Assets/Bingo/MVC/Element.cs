using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using Object = UnityEngine.Object;

namespace Bingo
{
    public class Element : EMonoBehaviour
    {
        public virtual void Awake()
        {
            InjectProperties(this);
        }

        private BaseApplication _app;
        public BaseApplication app
        {
            get
            {
                return _app = Inject(_app, true);
            }
        }

        public static T InjectStatic<T>(T obj) where T : Object
        {
            if (obj != null)
            {
                return obj;
            }

            T o = FindObjectOfType<T>();

            if (o == null)
            {
                Debug.LogError("Inject error: no object of type " + obj.GetType() + " found!");
            }

            return o;
        }

        private static Object _InjectTo(Type type)
        {
            Object o = FindObjectOfType(type);

            if (o == null)
            {
                Debug.LogError("Inject error: no object of type " + type + " found!");
            }

            return o;
        }

        private Object _Inject(Type type, bool isGlobal = false)
        {
            if (isGlobal)
            {
                Object o = FindObjectOfType(type);

                if (o == null)
                {
                    Debug.LogError("Inject error: no object of type " + type + " found!");
                }

                return o;
            }
            else
            {
                Object[] os = transform.GetComponentsInChildren(type, true);
                if (os.Length == 0)
                {
                    Debug.LogError("Inject error: no object of type " + type + " found!");
                    return null;
                }

                return os[0];
            }
        }

        public T Inject<T>(T obj, bool isGlobal = false) where T : Object
        {
            if (obj != null)
            {
                return obj;
            }

            if (isGlobal)
            {
                T o = FindObjectOfType<T>();

                if (o == null)
                {
                    Debug.LogError("Inject error: no object of type " + obj.GetType() + " found!");
                }

                return o;
            }
            else
            {
                T[] os = transform.GetComponentsInChildren<T>(true);
                if (os.Length == 0)
                {
                    Debug.LogError("Inject error: no object of type " + obj.GetType() + " found!");
                    return null;
                }

                return os[0];
            }
        }

        public T InjectLocal<T>(T obj) where T : Object
        {
            return obj ?? (obj = GetComponent<T>());
        }

        public T Cast<T>()
        {
            return (T)(object)this;
        }

        public void InjectProperties(object context)
        {
            Type type = context.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.Instance |
                                                            BindingFlags.DeclaredOnly);
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                object[] attributes = property.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is InjectAttribute)
                    {
                        InjectAttribute ia = attribute as InjectAttribute;
                        Type t = ia.type ?? property.PropertyType;
                        property.SetValue(context, _Inject(t, ia.isGlobal), null);
                    }
                }
            }
        }

        public static void InjectPropertiesTo(object context)
        {
            Type type = context.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.Instance |
                                                            BindingFlags.DeclaredOnly);
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                object[] attributes = property.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is InjectAttribute)
                    {
                        InjectAttribute ia = attribute as InjectAttribute;
                        Type t = ia.type ?? property.PropertyType;
                        property.SetValue(context, _InjectTo(t), null);
                    }
                }
            }
        }
    }

    public class Element<T> : Element where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return (T)base.app;
            }
        }
    }

    public class InjectAttribute : Attribute
    {
        public Type type;
        public bool isGlobal;

        public InjectAttribute() : this(null, false) { }
        public InjectAttribute(Type type) : this(type, false) { }
        public InjectAttribute(bool isGlobal) : this(null, isGlobal) { }

        public InjectAttribute(Type type, bool isGlobal)
        {
            this.type = type;
            this.isGlobal = isGlobal;
        }
    }
}
