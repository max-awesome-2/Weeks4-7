using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // variables for our sliders, buttons, etc.
    public Slider slider1, slider2, slider3, slider4, slider5, slider6, slider7;
    public Slider bar1;

    // references to all the panels
    public SlidePanel panel1, panel2, panel3, panel4, panel5, fakePanel0;

    // prefabs for the fallign buttons / slider
    public GameObject fallingButtonPrefab, fallingSliderPrefab;

    // colors of the 3 color buttons in panel 2
    private int b3AColor, b3BColor, b3CColor;

    // set to true once all the buttons are set to the same color - while true, they can't be changed further
    bool p2ButtonsLocked = false;

    // variables for the mask graphic in panel 3
    public Vector2 maskXMinMax, maskYMinMax;
    public RectTransform p3MaskGraphic;

    // all the button objects we need references to
    public GameObject button2, button3Parent;

    // references to btns 3A - 3C - we'll need to change the color of their images
    public Image button3A, button3B, button3C;

    // # of different colors the buttons can cycle through
    private Color[] btnColors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow };

    // panel 3 button challenge variables
    // time that the buttons will reset after
    public float p3ButtonChallengeTime = 10f;
    // timer to reset the challenge
    float p3ButtonTimer;
    // bool indicating whether the timer has started
    bool buttonTimerActive = false;

    // counter for how many buttons have been pressed so far
    private int p3ButtonsPressed = 0;
    public ToggleButton[] p3Buttons;

    public TextMeshProUGUI p3ButtonsText;

    // panel 4 button challenge variables
    // counter for how many buttons have been pressed so far
    private int p4ButtonsPressed;
    // transforms that the falling buttons will be spawned at  / parented to
    public Transform[] p4ButtonSpawns;
    // transform that the falling slider will be parented to
    public Transform fallingSliderSpawn;
    // reference to the button that starts the challenge
    public GameObject p4StartButton;
    private void Start()
    {
        // set min and max values for the timer bar
        bar1.minValue = 0f;
        bar1.maxValue = p3ButtonChallengeTime;

        ResetAll();
    }

    public void OnSliderValueChanged(string sliderName)
    {
        if (sliderName == "slider1")
        {
            panel1.Slide(slider1.value);
        }
        else if (sliderName == "slider2")
        {
            panel2.Slide(slider2.value);
        }
        else if (sliderName == "slider3")
        {
            panel3.Slide(slider3.value);
        }
        else if (sliderName == "slider4")
        {
            Vector3 pos = p3MaskGraphic.position;
            pos.x = Map(slider4.value, 0, 1, maskXMinMax.x, maskXMinMax.y);
        }
        else if (sliderName == "slider5")
        {
            Vector3 pos = p3MaskGraphic.position;
            pos.x = Map(slider5.value, 0, 1, maskXMinMax.x, maskXMinMax.y);
        }
        else if (sliderName == "slider6")
        {
            panel4.Slide(slider6.value);
        }
        else if (sliderName == "slider7")
        {
            if (slider7 != null)
            {
                float slideVal = slider7.value;

                if (slideVal >= 0.98f)
                {
                    // destroy the slider so it doesn't trigger the falling off screen method and reset itself
                    Destroy(slider7.gameObject);
                    slideVal = 1;
                }

                fakePanel0.Slide(slideVal);


            }
        }
    }

    // performs all the actions called on button presses
    public void OnButtonPressed(string btnName)
    {
        if (btnName == "btn1")
        {
            slider1.gameObject.SetActive(true);
        }
        else if (btnName == "btn2")
        {
            button2.SetActive(false);
            button3Parent.SetActive(true);
        }
        else if (btnName == "btn3a")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(1);
            CycleBtn3Color(2);

        }
        else if (btnName == "btn3b")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(0);
            CycleBtn3Color(2);

        }
        else if (btnName == "btn3c")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(1);
            CycleBtn3Color(0);

        }
        else if (btnName == "p4StartButton")
        {
            p4StartButton.SetActive(false);
            SpawnFallingButton(p4ButtonSpawns[0]);
        }
    }

    // called when a toggle button is pressed - specifically one from panel 3 or panel 4
    public void OnToggleButtonPressed(int panelNum)
    {
        if (panelNum == 3)
        {
            // increment the number of buttons pressed, and set the text to reflect that
            p3ButtonsPressed++;
            p3ButtonsText.text = p3ButtonsPressed + "/7";

            if (p3ButtonsPressed == p3Buttons.Length)
            {
                // set all the buttons inactive
                foreach (ToggleButton b in p3Buttons)
                {
                    b.gameObject.SetActive(false);
                }

                buttonTimerActive = false;

                // set slider 6 active
                slider6.gameObject.SetActive(true);
            }
            else if (p3ButtonsPressed == 1)
            {
                // start the timer when the first button is pressed
                buttonTimerActive = true;
                p3ButtonTimer = Time.time + p3ButtonChallengeTime;
            }
        }
        else if (panelNum == 4)
        {
            p4ButtonsPressed++;
            if (p4ButtonsPressed == p4ButtonSpawns.Length)
            {
                // instantiate slider 7
                GameObject s7 = Instantiate(fallingSliderPrefab);

                // set our slider 7 reference to the new object's slider component
                slider7 = s7.GetComponent<Slider>();

                // set slider 7's parent and reset its position to the parent's position
                s7.transform.parent = fallingSliderSpawn;
                s7.transform.localPosition = Vector3.zero;
            }
            else
            {
                // spawn the next falling button
                SpawnFallingButton(p4ButtonSpawns[p4ButtonsPressed]);
            }
        }
    }

    private void SpawnFallingButton(Transform t)
    {
        // instantiate the falling button prefab
        GameObject newButton = Instantiate(fallingButtonPrefab);

        // set the button's parent to the given transform and reset its position to zero
        newButton.transform.parent = t;
        newButton.transform.localPosition = Vector3.zero;
    }

    // called at the end of the game, resets everything
    private void ResetAll()
    {
        panel1.ResetPanel();
        panel2.ResetPanel();
        panel3.ResetPanel();
        panel4.ResetPanel();
        panel5.ResetPanel();

        slider1.gameObject.SetActive(false);
        button2.SetActive(true);
        button3Parent.SetActive(false);
        SetBtn3Color(0, 0);
        SetBtn3Color(1, 2);
        SetBtn3Color(2, 4);

        p2ButtonsLocked = false;
        slider3.gameObject.SetActive(false);
        p4StartButton.SetActive(true);

        foreach (ToggleButton b in p3Buttons)
        {
            b.ToggleReset();
            b.gameObject.SetActive(true);
        }

        p3ButtonsPressed = 0;
        p4ButtonsPressed = 0;

        slider6.gameObject.SetActive(false);

        bar1.value = 0;

        if (slider7 != null) slider7 = null;


    }

    // map function for mapping ranges
    private float Map(float val, float inMin, float inMax, float newMin, float newMax)
    {
        float ratio = (val - inMin) / (inMax - inMin);
        ratio = Mathf.Clamp(ratio, 0, 1);

        return newMin + ratio * (newMax - newMin);
    }


    // sets the color and image color of a button from panel 2
    private void SetBtn3Color(int btn, int color)
    {
        if (btn == 0)
        {
            b3AColor = color;
            button3A.color = btnColors[color];
        }
        else if (btn == 1)
        {
            b3BColor = color;
            button3B.color = btnColors[color];
        }
        else if (btn == 2)
        {
            b3CColor = color;
            button3C.color = btnColors[color];
        }
    }

    // cycles the color of a given button from panel 2
    private void CycleBtn3Color(int btn)
    {
        if (btn == 0)
        {
            b3AColor++;
            b3AColor %= btnColors.Length;
            SetBtn3Color(btn, b3AColor);

        }
        else if (btn == 1)
        {
            b3BColor++;
            b3BColor %= btnColors.Length;
            SetBtn3Color(btn, b3BColor);

        }
        else if (btn == 2)
        {
            b3CColor++;
            b3CColor %= btnColors.Length;
            SetBtn3Color(btn, b3CColor);
        }
    }

    // checks if the colors of btn3a, btn3b, and btn3c match - if so, lock the buttons and set the slider active
    private void CheckBtn3Colors()
    {
        if (b3AColor == b3BColor && b3BColor == b3CColor)
        {
            p2ButtonsLocked = true;
            slider3.gameObject.SetActive(true);
        }
    }

    public void OnFallingButtonFall()
    {
        p4ButtonsPressed = 0;
        p4StartButton.SetActive(true);
        fakePanel0.ResetPanel();
    }

    private void Update()
    {
        if (buttonTimerActive)
        {
            // set timer bar value
            bar1.value = Mathf.Clamp((p3ButtonTimer - Time.time) / p3ButtonChallengeTime, 0, 1);

            if (Time.time >= p3ButtonTimer)
            {
                p3ButtonsText.text = "0/7";
                p3ButtonsPressed = 0;
                buttonTimerActive = false;
                foreach (ToggleButton b in p3Buttons)
                {
                    b.ToggleReset();
                }
            }
        }
    }
}
