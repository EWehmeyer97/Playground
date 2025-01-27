using UnityEngine;

public class DiskGravityOrbit : GravityOrbit
{
    [SerializeField] private float innerRadius = 1f;

    public override Gravity GetGravity(Vector3 characterPos)
    {
        characterPos = transform.InverseTransformPoint(characterPos);
        Vector3 gravDirect;
        Vector2 charPos = new Vector2(characterPos.x, characterPos.z);
        Vector2 gravPos = new Vector2(transform.localPosition.x, transform.localPosition.z);
        if (Vector2.Distance(charPos, gravPos) < innerRadius)
            gravDirect = -transform.up * (characterPos.y > transform.localPosition.y ? 1f : -1f);
        else
        {
            Vector2 addTo = (charPos - gravPos).normalized * innerRadius;
            Vector3 gravityOrigin = transform.localPosition + new Vector3(addTo.x, 0f, addTo.y);
            gravDirect = transform.TransformDirection((gravityOrigin - characterPos).normalized);
        }
            
        
        return new Gravity(gravDirect * (isRepulsor ? -1f : 1f), Strength);
    }
}