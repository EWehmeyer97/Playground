using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class GravityOrbit : MonoBehaviour
{
    [Range (0,3)] [SerializeField] private int priority = 1;
    [Min (0f)] [SerializeField] private float strength = 9.81f;
    [SerializeField] protected bool isRepulsor = false;

    public float Strength { get { return strength; } }

    public abstract Vector3 GetGravity(Vector3 characterPos);

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GravityController controller = other.GetComponent<GravityController>();
        if(controller != null)
        {
            controller.SetGravity(priority, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GravityController controller = other.GetComponent<GravityController>();
        if (controller != null)
        {
            controller.RemoveGravity(priority, this);
        }
    }
}
