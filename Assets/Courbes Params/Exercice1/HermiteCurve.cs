using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class HermiteCurve : MonoBehaviour
{
    public GameObject start, startTangentPoint, end, endTangentPoint;

    public Color color = Color.green;
    public float width = 0.2f;
    public int numberOfPoints = 20;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    void Update()
    {
        // check parameters and components
        if (null == lineRenderer || null == start || null == startTangentPoint
            || null == end || null == endTangentPoint)
        {
            return; // no points specified
        }

        // update line renderer
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        if (numberOfPoints > 0)
        {
            lineRenderer.positionCount = numberOfPoints;
        }

        // set points of Hermite curve (startpoint & endpoint)
        Vector3 p0 = start.transform.position;
        Vector3 p1 = end.transform.position;
        
        //tangents of Start and End point
        Vector3 v0 = startTangentPoint.transform.position - start.transform.position;
        Vector3 v1 = endTangentPoint.transform.position - end.transform.position;

        float t; //Hermite curve H(t) for t from 0 to 1
        Vector3 position;

        // index i from 0 to numberOfPoints-1
        for (int i = 0; i < numberOfPoints; i++)
        {
            //H(t) = [(2*t3 - 3*t²)*p0] + [(t3 - 2*t² + t)*vO] + [(-2*t3 + 3*t²)*p1] + [(t3 - t²)*v1]

            // from this index i a parameter t from 0 to 1 is computed
            t = i / (numberOfPoints - 1.0f); 
            position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 //H(t) = [(2*t3 - 3*t²)*p0]
          + (t * t * t - 2.0f * t * t + t) * v0 //[(t3 - 2*t² + t)*vO] 
          + (-2.0f * t * t * t + 3.0f * t * t) * p1 //[(-2*t3 + 3*t²)*p1]
          + (t * t * t - t * t) * v1; //[(t3 - t²)*v1]

            lineRenderer.SetPosition(i, position);
        }
    }
}