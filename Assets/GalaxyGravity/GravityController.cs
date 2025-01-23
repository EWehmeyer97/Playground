using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public GravityOrbit[] orbits = new GravityOrbit[4];
    public float rotationSpeed = 20;

    private Rigidbody rb;

    void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 gravityUp = -Vector3.up;
        float gravityStrength = 9.81f;
        for(int i = orbits.Length - 1; i >= 0; i--)
        {
            if (orbits[i] != null)
            {
                gravityUp = orbits[i].GetGravity(transform.position);
                gravityStrength = orbits[i].Strength;
                break;
            }
        }

        Quaternion targetRot = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
        rb.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        rb.AddForce(-gravityUp * gravityStrength * rb.mass);
    }

    public void SetGravity(int i, GravityOrbit orbit)
    {
        orbits[i] = orbit;
    }

    public void RemoveGravity(int i, GravityOrbit orbit)
    {
        if (orbits[i] == orbit)
            orbits[i] = null;
    }
}
