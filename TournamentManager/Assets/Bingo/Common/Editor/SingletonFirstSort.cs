using UnityEngine;
using UnityEditor;

public class SingletonFirstSort : BaseHierarchySort
{
    public override int Compare(GameObject lhs, GameObject rhs)
    {
        if (lhs.name[0] == '~') return -1;

        return base.Compare(lhs, rhs);
    }
}