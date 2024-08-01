using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouyant : MonoBehaviour
{
    [SerializeField] private float floatation = 1f;
    [SerializeField] private Rigidbody rb;

    [Space]

    [SerializeField] private Vector4[] location = { new Vector4(0f, 0f, 0f, 1f)};
    private float[] massPoint;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float floatMass = 0;
        for (int i = 0; i < location.Length; i++)
            floatMass += location[i].w;

        massPoint = new float[location.Length];
        for(int i = 0; i < massPoint.Length; i++)
            massPoint[i] = rb.mass * location[i].w / floatMass;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < location.Length; i++)
        {
            Vector3 force = -Vector3.up * 9.81f * location[i].w;
            Vector3 pos = transform.position + transform.TransformDirection(location[i]);
            float cover = OceanSimulation.Instance.GetWaterHeight(pos) - pos.y;
            if (cover > 0f)
            {
                force += Vector3.up * floatation * (1 + cover) * massPoint[i];
            }
                rb.AddForceAtPosition(force, pos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < location.Length; i++)
        {
            Gizmos.color = Color.yellow * location[i].w;
            Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(location[i]), 0.1f);
        }
    }
}
