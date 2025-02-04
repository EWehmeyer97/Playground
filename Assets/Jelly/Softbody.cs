using UnityEngine;

public class Softbody : MonoBehaviour
{
    [SerializeField] private JellySetup setupMode;
    [SerializeField] private Transform root;

    [Header("Soft Body Properties")]
    [SerializeField][Min(0f)] private float mass = 1f; 
    [SerializeField][Min(0f)] private float spring = 10f;
    [SerializeField][Min(0f)] private float damper = 0.2f;
    [SerializeField][Min(0f)] private float tolerance = 0.025f;

    [Header("Non Manual Properties")]
    [SerializeField] private bool isRootBodyPart = false;
    [SerializeField] private PhysicsMaterial physics;

    [Header("Distance Based Properties")]
    [SerializeField][Min(0f)] private float connectionDistance = .5f;

    private Rigidbody[] bodies;
    private SpringJoint[] joints;
    
    void Start()
    {
        //Add Rigidbodies if not in manual mode
        if (setupMode != JellySetup.Manual)
        {
            foreach (var bone in root.GetComponentsInChildren<Transform>())
            {
                if (bone == root && !isRootBodyPart)
                    continue;

                bone.gameObject.AddComponent<Rigidbody>();
                if (bone.GetComponent<Collider>() != null)
                    bone.GetComponent<Collider>().material = physics;
            }
        }
        bodies = root.GetComponentsInChildren<Rigidbody>();

        //Set Mass of object
        float individualMass = mass / bodies.Length;
        foreach (var rb in bodies) 
            rb.mass = individualMass;

        //Add SpringJoints if not in manual mode + Parent Rigidbody
        if (setupMode != JellySetup.Manual)
        {
            SetupParent();

            foreach (var body in bodies)
            {
                foreach (var otherBody in bodies)
                {
                    if (body != otherBody && Vector3.Distance(body.position, otherBody.position) <= connectionDistance)
                    {
                        var sj = body.gameObject.AddComponent<SpringJoint>();
                        sj.connectedBody = otherBody;
                    }
                }
            }
        }
        joints = root.GetComponentsInChildren<SpringJoint>();
        
        //Set Spring Joint properites
        foreach (var sj in joints)
        {
            sj.spring = spring;
            sj.damper = damper;
            sj.tolerance = tolerance;
        }
    }

    private void SetupParent()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = .001f;

        gameObject.AddComponent<FixedJoint>().connectedBody = bodies[0];
    }
}


[System.Serializable]
public enum JellySetup
{
    Distance_Based,
    Manual
}