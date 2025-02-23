using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{

    private Image img;

    public bool toggled;

    public GameManager manager;

    // indicates whether this button is part of panel 3 or panel 4
    public int panelNum;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to image component
        img = GetComponent<Image>();
    }

    // called when pressed
    public void OnPressed()
    {
        if (!toggled)
        {
            toggled = true;
            img.color = Color.red;
            manager.OnToggleButtonPressed(panelNum);
        }
    }

    public void ToggleReset()
    {
        toggled = false;
        img.color = Color.white;
    }
}
