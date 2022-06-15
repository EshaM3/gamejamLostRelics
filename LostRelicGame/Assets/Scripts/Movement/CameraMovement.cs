using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float smoothness = 2.0f;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = new Vector3(0,0, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0, 0) + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, playerPosition, smoothness * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
