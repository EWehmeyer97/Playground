using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Wake : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    [SerializeField] private float offset = 0.01f;
    [SerializeField] private TrailRenderer lr;
    [SerializeField] private Engine engine;

    private void OnValidate()
    {
        lr = GetComponent<TrailRenderer>();
    }
    void Start()
    {
        lr.time = duration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetPoint(engine.GetEnginePosition());
    }

    private Vector3 GetPoint(Vector3 v)
    {
        return new Vector3(v.x, offset, v.z);
    }
}
