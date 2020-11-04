using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyPoints : MonoBehaviour
{
    public Color color = Color.red;
    public float width = 0.1f;

    public LineRenderer polyRenderer;
    private int numPoly = 4;

    public Transform point0, point1, point2, point3;

    private float speed = 3.0f;
    Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        polyRenderer = GetComponent<LineRenderer>();
        polyRenderer.useWorldSpace = true;
        polyRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));

        polyRenderer.positionCount = numPoly;
        DrawPolygone();

        currentPoint = point0;
    }

    // Update is called once per frame
    void Update()
    {
        polyRenderer.startColor = color;
        polyRenderer.endColor = color;
        polyRenderer.startWidth = width;
        polyRenderer.endWidth = width;

        DrawPolygone();
    }

    private void DrawPolygone()
    {
        Vector3[] poly = new Vector3[4];
        poly[0] = point0.position;
        poly[1] = point1.position;
        poly[2] = point2.position;
        poly[3] = point3.position;

        polyRenderer.SetPositions(poly);

        Movements();
    }

    private void Movements()
    {

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("0");
            currentPoint = point0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("1");
            currentPoint = point1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("2");
            currentPoint = point2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("3");
            currentPoint = point3;
        }

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        currentPoint.position += move * speed * Time.deltaTime;
    }
}
