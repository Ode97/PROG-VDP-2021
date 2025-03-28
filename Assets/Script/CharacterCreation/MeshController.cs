using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public float ScaleX = 1.0f;
    public float ScaleY = 1.0f;
    public float ScaleZ = 1.0f;
    public float RotationX = 1.0f;
    public bool RecalculateNormals = false;
    public bool inGame = false;
    private Vector3[] _baseVertices;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Update()
    {
        if(!inGame) {
            var mesh = GetComponent<MeshFilter>().mesh;
            if (_baseVertices == null)
                _baseVertices = mesh.vertices;
            var vertices = new Vector3[_baseVertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                var vertex = _baseVertices[i];
                vertex.x = vertex.x * ScaleX;
                vertex.y = vertex.y * ScaleY;
                vertex.z = vertex.z * ScaleZ;
                vertices[i] = vertex;
            }
            mesh.vertices = vertices;
            if (RecalculateNormals)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
        inGame = true;
    }
}
