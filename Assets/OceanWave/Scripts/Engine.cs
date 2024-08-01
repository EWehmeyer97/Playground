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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position + transform.TransformDirection(location);
        float cover = OceanSimulation.Instance.GetWaterHeight(pos);
        
        rb.AddForce(transform.forward * force * (cover - pos.y > 0f ? 1f : 0.5f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(location), 0.1f);
    }
}
