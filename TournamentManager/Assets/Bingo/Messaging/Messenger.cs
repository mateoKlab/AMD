using System;
using System.Collections.Generic;

namespace Bingo
{
    public class Messenger : Singleton<Messenger>
    {
        public delegate void MessageDelegate(params object[] args);
        private Dictionary<object, List<MessageDelegate>> _callbackList = new Dictionary<object, List<MessageDelegate>>();

        public static void AddListener<T>(T EventTagsName, MessageDelegate callback)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            List<MessageDelegate> l;
            if (!Instance._callbackList.TryGetValue(EventTagsName, out l))
            {
                l = new List<MessageDelegate>();
                Instance._callbackList.Add(EventTagsName, l);
            }

            l.Add(callback);
        }

        public static void RemoveListener<T>(T EventTagsName, MessageDelegate callback)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            List<MessageDelegate> l;
            if (Instance._callbackList.TryGetValue(EventTagsName, out l))
            {
                l.Remove(callback);
            }
        }

        public static void RemoveAllListeners<T>(T EventTagsName)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            Instance._callbackList.Remove(EventTagsName);
        }

        public static void Send<T>(T EventTagsName, params object[] args)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            List<MessageDelegate> l;
            if (Instance._callbackList.TryGetValue(EventTagsName, out l))
            {
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i] != null)
                    {
                        l[i](args);
                    }
                }
            }
        }
    }
}
