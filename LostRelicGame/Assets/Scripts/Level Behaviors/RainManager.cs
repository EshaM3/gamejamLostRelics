using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public ParticleSystem rain;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        GenerateRain();
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate(rain, player.transform);
        if (player.transform.position.x % 10 == 0)
        {
            GenerateRain();
        }
    }

    void GenerateRain()
    {
        Instantiate(rain, new Vector3(player.transform.position.x, 20, -1), player.transform.rotation);
    }
}
