using UnityEngine;

public class JellyControl : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] [Min(0f)] private float spring = 10f;
    [SerializeField] [Min(0f)] private float damper = 0.2f;
    [SerializeField][Min(0f)] private float tolerance = 0.025f;

    private Rigidbody[] bodies;
    private SpringJoint[] joints;
    
    void Start()
    {
        bodies = root.GetComponentsInChildren<Rigidbody>();
        joints = root.GetComponentsInChildren<SpringJoint>();

        foreach (var sj in joints)
        {
            sj.spring = spring;
            sj.damper = damper;
            sj.tolerance = tolerance;
        }
    }
}
