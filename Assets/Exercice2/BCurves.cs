using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCurves : MonoBehaviour
{
    public Color color = Color.green;
    public float width = 0.1f;

    public LineRenderer lineRenderer;
    public Transform point0, point1, point2, point3;

    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));

        lineRenderer.positionCount = numPoints;
        DrawCubicCurve();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        DrawCubicCurve();
    }

    private void DrawCubicCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //B(t) = [(1 - t)3 *P0] + [3*(1 - t)² *t*P1] + [3*(1 - t)*t² *P2] + [t3 *P3]

        float u = 1 - t;
        float tt = t * t; //t²
        float uu = u * u; //u²
        float ttt = tt * t; //t3
        float uuu = uu * u; //u3

        Vector3 p = uuu * p0; //B(t) = [(1 - t)3 * P0]
        p += 3 * uu * t * p1; //[3*(1 - t)² *t*P1]
        p += 3 * u * tt * p2; //[3*(1 - t)*t² *P2]
        p += ttt * p3; //[t3 *P3]

        return p;
    }
}
