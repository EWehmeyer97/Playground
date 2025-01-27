using UnityEngine;

public class PlanarGravityOrbit : GravityOrbit
{
    public override Gravity GetGravity(Vector3 characterPos)
    {
        return new Gravity(-transform.up * (isRepulsor ? -1f : 1f), Strength);
    }
}
