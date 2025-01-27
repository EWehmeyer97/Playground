using UnityEngine;

public class SphereGravityOrbit : GravityOrbit
{
    public override Gravity GetGravity(Vector3 characterPos)
    {
        return new Gravity ((transform.position - characterPos).normalized * (isRepulsor ? -1f : 1f), Strength);
    }
}
