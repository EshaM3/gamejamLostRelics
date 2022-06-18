using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script can be used later for spawning enemies & scene changes

public class GameManager : MonoBehaviour
{  
    public GameObject restart_quit_shell; //restart level button and quit button revealed/hidden by pressing 'esc' key
    //public GameObject inventorySystemShell; //can hide inventory (and show after hidden) by pressing 'i' key
    //public GameObject SoundManager; //can mute/unmute bg music by pressing 'm' key

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowRestartQuit();
        } 
    }

    public void ShowRestartQuit()
    {
        GameObject restart_quit = restart_quit_shell.transform.Find("restart_quit").gameObject;
        if(restart_quit.activeInHierarchy) restart_quit.SetActive(false);
        else restart_quit.SetActive(true);
    }
}
