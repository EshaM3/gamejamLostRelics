using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringGrapple : MonoBehaviour
{
    private SpringJoint2D playerSpring;
    private LineRenderer web;
    public Camera mainCamera;
    public bool isGrappling;
    public bool canSwing = false;
    public float decreaseRate = 2.5f;
    public float initialDivider = 1.2f;

    private PlayerController playerControls;

    // Start is called before the first frame update
    void Start()
    {
        playerSpring = GetComponent<SpringJoint2D>();
        web = GetComponent<LineRenderer>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Ensure spring is disabled at the start of game.
        playerSpring.enabled = false;
        web.enabled = false;

        // Player controls definition
        playerControls = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrappling = playerSpring.enabled;

        // Turning mouse position into world point
        Vector2 mouseCoordinate = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Generate a web when mouse is pressed down (one instance)
        if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing)
        {
            // Create the spring
            playerSpring.distance = (Vector2.Distance(transform.position, mouseCoordinate)) / initialDivider;
            playerSpring.connectedAnchor = mouseCoordinate;
            
            playerSpring.enabled = true;

            // Set the end point of web to the mouse click (this particular instance)
            web.SetPosition(1, mouseCoordinate);

        } else if (Input.GetKeyUp(KeyCode.Mouse0))   
        {
            // When mouse is released disable web and line renderer.
            playerSpring.enabled = false;
            web.enabled = false;
        }


        // Update the web throughout each instance
        if (playerSpring.enabled == true)
        {
            web.SetPosition(0, transform.position);
            web.enabled = true;
        }

        // Shorten the web as time goes on
        playerSpring.distance -= decreaseRate * Time.deltaTime;
    }
}
