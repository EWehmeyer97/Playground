using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouyant : MonoBehaviour
{
    [Header("Bouyance Collision")]
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private Vector2 size = Vector2.one;
    [SerializeField][Range(1, 5)] private int subdivision = 1;

    private Rigidbody rb;
    private Vector3[] floatPoints = new Vector3[0];

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        SetPoints();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < floatPoints.Length; i++)
        {
            Vector3 force = Physics.gravity * rb.mass / floatPoints.Length;

            Vector3 pos = transform.TransformPoint(floatPoints[i]);
            float cover = OceanSimulation.Instance.GetWaterHeight(pos) - pos.y;

            if (cover > 0)
            {
                force *= -1f; //push up instead of down
            }

            force *= Mathf.Min(1f, Mathf.Abs(cover));
            rb.AddForceAtPosition(force, pos);
        }
    }

    private void SetPoints()
    {
        floatPoints = new Vector3[subdivision * subdivision];

        for (int i = 0; i < floatPoints.Length; i++)
        {
            int x = i % subdivision;
            int y = i / subdivision;

            floatPoints[i] = center + new Vector3((1f / (subdivision * 2f) + ((float)x / subdivision) - .5f) * size.x, 0, (1f / (subdivision * 2f) + ((float)y / subdivision) - .5f) * size.y);
        }
    }

    // private void OnValidate()
    // {
    //     SetPoints();        
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(center, Vector3.forward * size.y + Vector3.right * size.x);

        // for (int i = 0; i < floatPoints.Length; i++)
        // {
        //     Gizmos.DrawWireSphere(floatPoints[i], 0.1f);
        // }
    }
}
