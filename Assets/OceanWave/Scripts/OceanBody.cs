using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanBody : MonoBehaviour
{
    [SerializeField] private Vector2Int spawnCount;
    [SerializeField] private GameObject oceanTile;

    void Start()
    {
        for(int i = 0; i < spawnCount.y; i++)
        {
            for(int j = 0; j < spawnCount.x; j++)
            {
                Instantiate(oceanTile, new Vector3(8f * j, 0f, -8f * i), Quaternion.identity, transform);
            }
        }
    }

}
