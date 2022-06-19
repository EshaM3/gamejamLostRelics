using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCommunicator : MonoBehaviour
{
    

    public GameObject[] bucketStuff = new GameObject[4];
    public GameObject[] endMessage = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonEvents.end == true)
        {

            for (int i = 0; i < 3; i++)
            {
                bucketStuff[i].SetActive(true);
            }


            StartCoroutine(waitTime());
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 2; i++)
        {
            endMessage[i].SetActive(true);
        }
    }
}
