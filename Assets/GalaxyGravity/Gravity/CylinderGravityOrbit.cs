using UnityEngine;

public class CylinderGravityOrbit : GravityOrbit
{
    public override Gravity GetGravity(Vector3 characterPos)
    {
        Vector3 newPos = characterPos - transform.position;
        Vector3 closestPoint = transform.position + Vector3.Dot(newPos, transform.up) * transform.up;
        return new Gravity((closestPoint - characterPos).normalized * (isRepulsor ? -1f : 1f), Strength);
    }
}
