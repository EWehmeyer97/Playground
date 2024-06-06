using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGravityOrbit : GravityOrbit
{
    public override Vector3 GetGravity(Vector3 characterPos)
    {
        return (characterPos - transform.position).normalized * (isRepulsor ? -1f : 1f);
    }
}
