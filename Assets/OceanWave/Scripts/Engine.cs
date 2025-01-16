using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Engine : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float force = 15f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Vector3 location = Vector3.zero;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = GetEnginePosition();
        float cover = OceanSimulation.Instance.GetWaterHeight(pos);
        
        rb.AddForce(transform.forward * force * (cover - pos.y > 0f ? 1f : 0.5f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetEnginePosition(), 0.1f);
    }

    public Vector3 GetEnginePosition()
    {
        return transform.TransformPoint(location);
    }
}
