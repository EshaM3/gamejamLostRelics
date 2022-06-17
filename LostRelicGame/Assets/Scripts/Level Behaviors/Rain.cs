using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public ParticleSystem rain;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            Vector2 position = new Vector2(transform.position.x + 26.24f, transform.position.y);
            Instantiate(rain, position, transform.rotation);
        }

        if (player.transform.position.x > transform.position.x + 26.24 || player.transform.position.x < transform.position.x - 26.24)
        {
            Destroy(gameObject);
        }
    }
}
