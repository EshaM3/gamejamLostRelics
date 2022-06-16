using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject MainCamera;
    public float length;
    public float origin;

    public float parallax;

    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        origin = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = MainCamera.transform.position.x * (1-parallax);
        float distance = MainCamera.transform.position.x * parallax;

        transform.position = new Vector3(origin + distance, transform.position.y, transform.position.z);

        if (temp > origin + length)
        {
            origin += length;
        } else if (temp < origin - length)
        {
            origin -= length;
        }
    }
}
