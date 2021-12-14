using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public static class ExtensionMethods
    {
        public static DT FindAnyChild<DT>(this Transform trans, string name) where DT : Component
        {
            for (var n = 0; n < trans.childCount; n++)
            {
                if (trans.GetChild(n).childCount > 0)
                {
                    var child = trans.GetChild(n).FindAnyChild<Transform>(name);
                    if (child != null)
                        return child.GetComponent<DT>();
                }
                if (trans.GetChild(n).name == name)
                {
                    return trans.GetChild(n).GetComponent<DT>();
                }
            }
            return default;
        }
    }
}