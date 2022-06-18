using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script can be used later for spawning enemies & scene changes

public class GameManager : MonoBehaviour
{
    
    public GameObject restart_level; //restart level button revealed by pressing 'esc' key

    // Start is called before the first frame update
    void Start()
    {
        
        restart_level.GetComponent<Button>().onClick.AddListener(TaskOnClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick() //for restart when u die / want to restart
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
