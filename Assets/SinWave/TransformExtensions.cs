using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }