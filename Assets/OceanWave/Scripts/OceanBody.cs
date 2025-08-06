using System.Collections.Generic;
using UnityEngine;

public class OceanBody : MonoBehaviour
{
    //Public Field Variables
    [SerializeField] private Mesh oceanTile;
    [Tooltip("The scale of the mesh object in meters.")] [SerializeField] private float oceanTileScale = 1f;
    [Tooltip("The number of tiles from the camera that ocean should extend.")] [SerializeField] private float oceanTileCount = 100f;
    [SerializeField] private Material oceanMaterial;

    //Tile Locations
    private List<Matrix4x4> matrices;

    //Camera Data
    private Camera cam;
    private Vector3 lastCameraPosition;

    //Stored Variable for calculating if a tile is visible to the camera or not
    private float cameraVisibility;

    void Start()
    {
        cam = Camera.main;

        //Set Stored Variables
        cameraVisibility = -0.75f * oceanTileScale;
    }

    void LateUpdate()
    {
        CheckCameraUpdate();
        UpdateMatrices();

        Graphics.DrawMeshInstanced(oceanTile, 0, oceanMaterial, matrices);
    }

    private void CheckCameraUpdate()
    {
        Vector3 currentCameraPosition = RoundCameraPostion();

        bool b = Mathf.Abs(lastCameraPosition.x - currentCameraPosition.x) < oceanTileScale && Mathf.Abs(lastCameraPosition.z - currentCameraPosition.z) < oceanTileScale;
        if (!b)
            lastCameraPosition = currentCameraPosition;
            
        
    }

    private Vector3 RoundCameraPostion()
    {
        return Vector3Int.RoundToInt(cam.transform.position);
    }

    private void UpdateMatrices()
    {
        matrices = new List<Matrix4x4>();
        Vector3 startPoint = lastCameraPosition - (Vector3.right * oceanTileCount * oceanTileScale + Vector3.forward * oceanTileCount * oceanTileScale);

        var planes = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i = 0; i < 2 * oceanTileCount; i++)
        {
            for (int j = 0; j < 2 * oceanTileCount; j++)
            {
                bool addPointToList = true;

                Vector3 point = new Vector3(startPoint.x + oceanTileScale * j, 0f, startPoint.z + oceanTileScale * i);
                foreach (var plane in planes)
                    if (plane.GetDistanceToPoint(point) < cameraVisibility)
                        addPointToList = false;

                if(addPointToList)
                    matrices.Add(Matrix4x4.TRS(point, Quaternion.identity, Vector3.one));
            }
        }
    }
}