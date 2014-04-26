using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
class Spline : MonoBehaviour
{
    public float SegmentRate = 0.5f;
    public float Width = 0.5f;

    public Mesh SplineMesh;

    void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        SplineMesh = (meshFilter.mesh = new Mesh());
    }

    float cacheChecksum;
    public void Update()
    {
        // go through child nodes and check if checksum matches
        float newChecksum = 0;
        var childNodes = GetComponentsInChildren<SplineNode>();
        foreach (var node in childNodes)
            newChecksum += Vector3.Dot(node.transform.position, Vector3.one);
        cacheChecksum += Width;
        cacheChecksum += SegmentRate;

        if (cacheChecksum != newChecksum)
        {
            // only rebuild mesh if checksum changed
            Rebuild(childNodes);
            //Debug.Log("Rebuilt!");
            cacheChecksum = newChecksum;
        }
    }

    public void Rebuild(SplineNode[] nodes)
    {
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uv = new List<Vector2>();

        var nodePositions = nodes.Select(x => x.transform.position).ToList();

        SplineMesh.Clear();
        SplineMesh.subMeshCount = 1;

        var segments = Math.Round(nodes.Length * 100 * SegmentRate);

        float accumulatedTexCoord = 0;
        for (int i = 0; i <= segments; i++)
        {
            var s = i / (float)segments;

            var center = MathfPlus.BSpline(nodePositions, s);
            Vector3 nextCenter = MathfPlus.BSpline(nodePositions, (i + 1) / (float)segments);

            var diff = Vector3.Normalize(nextCenter - center);
            var tangent = Vector3.Cross(diff, Vector3.up);

            var c = vertices.Count;

            accumulatedTexCoord += Vector3.Distance(nextCenter, center) * 0.1f;

            vertices.Add(center - tangent * Width); uv.Add(new Vector2(0, accumulatedTexCoord));
            vertices.Add(center + tangent * Width); uv.Add(new Vector2(1, accumulatedTexCoord));

            if (i != segments)
            {
                triangles.Add(c); triangles.Add(c + 1); triangles.Add(c + 2);
                triangles.Add(c + 2); triangles.Add(c + 1); triangles.Add(c + 3);

                triangles.Add(c); triangles.Add(c + 2); triangles.Add(c + 1);
                triangles.Add(c + 2); triangles.Add(c + 3); triangles.Add(c + 1);
            }
        }

        if (vertices.Count > 0)
        {
            try
            {
                SplineMesh.vertices = vertices.ToArray();
                SplineMesh.uv = uv.ToArray();
                SplineMesh.SetTriangles(triangles.ToArray(), 0);

                SplineMesh.RecalculateNormals();
                SplineMesh.RecalculateBounds();
                SplineMesh.Optimize();
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }
        }
    }

}
