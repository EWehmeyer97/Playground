using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyant : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, OceanSimulation.Instance.GetWaterHeight(pos), pos.z);
    }
}
