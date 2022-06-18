using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingablePlatform : MonoBehaviour
{
    private SpringGrapple web;

    // Start is called before the first frame update
    void Start()
    {
        web = GameObject.Find("Player").GetComponent<SpringGrapple>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        web.canSwing = true;
    }

    private void OnMouseExit()
    {
        web.canSwing = false;
    }
}
