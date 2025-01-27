using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
        Gravity gravity = new Gravity();
        for(int i = orbits.Length - 1; i >= 0; i--)
        {
            if (orbits[i] != null)
            {
                gravity = orbits[i].GetGravity(transform.position);
                break;
            }
        }

        Quaternion targetRot = Quaternion.FromToRotation(transform.up, -gravity.direction) * transform.rotation;
        rb.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
        rb.AddForce(gravity.direction * gravity.strength * rb.mass);
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