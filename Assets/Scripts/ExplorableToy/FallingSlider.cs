using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingSlider : MonoBehaviour
{

    // reference to the game manager
    public GameManager manager;

    // called when the attached slider changes values
    public void OnValueChanged()
    {
        manager.OnSliderValueChanged("slider7");
    }
}
