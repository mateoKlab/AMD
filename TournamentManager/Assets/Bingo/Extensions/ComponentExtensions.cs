using UnityEngine;

namespace Bingo
{
    public static class ComponentExtensions
    {
        public static T GetIComponent<T>(this Component comp) where T : class
        {
            return comp.GetComponent(typeof(T)) as T;
        }

        public static T[] GetIComponents<T>(this Component comp) where T : class
        {
            return comp.GetComponents(typeof(T)) as T[];
        }

        public static T GetIComponentInParent<T>(this Component comp) where T : class
        {
            return comp.GetComponentInParent(typeof(T)) as T;
        }

        public static T[] GetIComponentsInParent<T>(this Component comp, bool includeInactive = false) where T : class
        {
            return comp.GetComponentsInParent(typeof(T), includeInactive) as T[];
        }

        public static T GetIComponentInChildren<T>(this Component comp) where T : class
        {
            return comp.GetComponentInChildren(typeof(T)) as T;
        }

        public static T[] GetIComponentsInChildren<T>(this Component comp, bool includeInactive = false) where T : class
        {
            return comp.GetComponentsInChildren(typeof(T), includeInactive) as T[];
        }
    }

}
