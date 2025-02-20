using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    /*
     * Success boolean to check when button sequence is correct
     * public arraylist to contain buttons
     * 
     * public gameobject to contain door
     * 
     * 
     * Success always start as true - success = true
     * for loop checking each buttons boolean
     * if statement checking button pressed boolean - if (buttonArray[i].avtive == false)
     * if button returns false check is failed - success = false
     * 
     * if statement opening door when button check is successful - if (success == true)
     * doorGameObject.setActive
     * 
     */

    bool success;
    public List<Button> buttons;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        success = true;

        foreach(Button b in buttons)
        {
            if(b.active == false)
            {
                success = false;
            }
        }

        if(success == true)
        {
            door.SetActive(false);
        }
    }
}
