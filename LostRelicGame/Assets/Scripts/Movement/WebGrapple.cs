using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGrapple : MonoBehaviour
{
    public Camera mainCamera;
    private LineRenderer line;
    private DistanceJoint2D pivot;
    public bool isGrappling;


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        pivot = GetComponent<DistanceJoint2D>();

        pivot.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isGrappling = true;
            // Converting mouse position to physical point for reference
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Establishing a visible web
            line.SetPosition(0, mousePos);
            line.SetPosition(1, transform.position);
            line.enabled = true;

            // Sets the anchor point to mouse position (converted from cam to actual point object)
            pivot.connectedAnchor = mousePos;
            pivot.enabled = true;

        } else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isGrappling = false;
            pivot.enabled = false;
            line.enabled = false;
        }
        
        // If web is showing make sure that it updates accordingly
        if (pivot.enabled)
        {
            line.SetPosition(1, transform.position);
        }
    }
}
