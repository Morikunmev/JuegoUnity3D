using UnityEngine;

public class RampColliderGenerator : MonoBehaviour 
{
    void Start()
    {
        // Obtenemos el mesh actual
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Creamos un mesh collider
            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.sharedMesh;
            
            // Importante: NO hacer convex para mantener la curvatura exacta
            meshCollider.convex = false;
        }
    }
}