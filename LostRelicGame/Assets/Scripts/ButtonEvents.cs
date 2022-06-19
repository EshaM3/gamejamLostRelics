using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    public static bool end = false;
    public static bool candyForAnt = false;

    // Scene to switch to when pressed
    [SerializeField] private string sceneToSwitchTo;
    public string SceneToSwitchTo
    {
        get
        {
            return sceneToSwitchTo;
        }
    }

    // Function to run when pressed
    [SerializeField] private string functionToRun;
    public string FunctionToRun
    {
        get
        {
            return functionToRun;
        }
    }

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        
    }

    private void Update()
    {
        
    }

    void TaskOnClick() //unity action
    {
        ButtonFunction(functionToRun, sceneToSwitchTo);
    }

    //will call the button's function/scene that it must switch to
    public void ButtonFunction(string functionToRun, string sceneToSwitchTo)
    {
        if (functionToRun != null && functionToRun != "")
            Invoke(functionToRun, 0);
        if (sceneToSwitchTo != null && sceneToSwitchTo != "")
            SceneManager.LoadScene(sceneToSwitchTo);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        //loads to title screen
        SceneManager.LoadScene(0);
    }

    public void InventorySelection()
    {
        if(gameObject.GetComponentInParent<UIInventoryItemSlot>().getLabel() == "Key")
        {
            if(GameObject.Find("well").GetComponent<LocationTrigger>().inLocation)
                SceneManager.LoadScene("Level1_Underground");
        }

        //this doesn't work...
        if (gameObject.GetComponentInParent<UIInventoryItemSlot>().getLabel() == "Candy")
        {
            candyForAnt = true;
        }

        if (gameObject.GetComponentInParent<UIInventoryItemSlot>().getLabel() == "Rope")
        {
            if (GameObject.Find("ropeItemSpace").GetComponent<LocationTrigger>().inLocation)
            {
                end = true;
                Debug.Log("pls");
            }    
        }
    }
    
}
