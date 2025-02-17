using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions {

    public static void DestroyChildren(this Transform transform)
    {
        List<Transform> children = new List<Transform>();
        foreach(Transform child in transform)
        {
            children.Add(child);
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static void ResetLocal(this Transform transform)
    {
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = Vector3.one;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
