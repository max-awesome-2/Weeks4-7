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
    public SlidePanel panel1, panel2, panel3, panel4, fakePanel0;

    // prefabs for the fallign buttons / slider
    public GameObject fallingButtonPrefab, fallingSliderPrefab;

    // colors of the 3 color buttons in panel 2
    private int b3AColor, b3BColor, b3CColor;

    // set to true once all the buttons are set to the same color - while true, they can't be changed further
    bool p2ButtonsLocked = false;

    // variables for the mask graphic in panel 3
    public Vector2 maskXMinMax, maskYMinMax;
    public RectTransform p3MaskGraphic;

    // fake background for p3 that will be replaced with the mask once
    public GameObject p3DemoBG;

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

    public GameObject p3HintArrows;


    ///// insurance variables: the whole thing breaks if a player leaves a slider unfilled so, if any slider is more than 50% full but the mouse is not currently held down, just finish filling it for them!
    public float insuranceFillSpeed = 1;
    public float insuranceFillThreshold = 0.5f;
    private bool mouseDown;

    private void Start()
    {
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

            if (slider3.value >= 0.98f)
            {
                p3MaskGraphic.gameObject.SetActive(true);
                p3DemoBG.gameObject.SetActive(false);
            }
        }
        else if (sliderName == "slider5")
        {
            Vector2 newAnchorMin = p3MaskGraphic.anchorMin, newAnchorMax = p3MaskGraphic.anchorMax;
            newAnchorMin.x = Map(slider5.value, 0, 1, maskXMinMax.x, maskXMinMax.y);
            newAnchorMax.x = newAnchorMin.x;
            p3MaskGraphic.anchorMin = newAnchorMin;
            p3MaskGraphic.anchorMax = newAnchorMax;
        }
        else if (sliderName == "slider4")
        {
            Vector2 newAnchorMin = p3MaskGraphic.anchorMin, newAnchorMax = p3MaskGraphic.anchorMax;
            newAnchorMin.y = Map(slider4.value, 0, 1, maskYMinMax.x, maskYMinMax.y);
            newAnchorMax.y = newAnchorMin.y;
            p3MaskGraphic.anchorMin = newAnchorMin;
            p3MaskGraphic.anchorMax = newAnchorMax;
        }
        else if (sliderName == "slider6")
        {
            panel4.Slide(slider6.value);

            // alter mask anchor so that the open square follows the slider head
            Vector2 newAnchorMin = p3MaskGraphic.anchorMin, newAnchorMax = p3MaskGraphic.anchorMax;
            newAnchorMin.y = Map(slider6.value, 0, 1, maskYMinMax.x, maskYMinMax.y);
            newAnchorMax.y = newAnchorMin.y;
            p3MaskGraphic.anchorMin = newAnchorMin;
            p3MaskGraphic.anchorMax = newAnchorMax;
        }
        else if (sliderName == "slider7")
        {
            if (slider7 != null)
            {
                float slideVal = slider7.value;

                fakePanel0.Slide(slideVal);


                if (slideVal >= 0.98f)
                {
                    // destroy the slider so it doesn't trigger the falling off screen method and reset itself
                    Destroy(slider7.gameObject);
                    slider7 = null;
                    slideVal = 1;

                    fakePanel0.Slide(0);
                    ResetAll();
                   
                }



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
            slider1.gameObject.SetActive(false);
            button2.SetActive(false);
            button3Parent.SetActive(true);
        }
        else if (btnName == "btn3a")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(1);
            CycleBtn3Color(2);
            CheckBtn3Colors();


        }
        else if (btnName == "btn3b")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(0);
            CycleBtn3Color(2);
            CheckBtn3Colors();


        }
        else if (btnName == "btn3c")
        {
            if (p2ButtonsLocked) return;

            CycleBtn3Color(1);
            CycleBtn3Color(0);
            CheckBtn3Colors();


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

                // set hint arrow active
                p3HintArrows.SetActive(true);

                RectTransform arrowRT = p3HintArrows.GetComponent<RectTransform>();
                arrowRT.anchorMin = p3MaskGraphic.anchorMin;
                arrowRT.anchorMax = p3MaskGraphic.anchorMax;
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
                s7.GetComponent<FallingSlider>().manager = this;
                s7.GetComponent<FallingObject>().manager = this;


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
        newButton.GetComponent<ToggleButton>().manager = this;
        newButton.GetComponent<FallingObject>().manager = this;

        // set the button's parent to the given transform and reset its position to zero
        newButton.transform.parent = t;
        newButton.transform.localPosition = Vector3.zero;
    }

    // called at the end of the game, resets everything
    private void ResetAll()
    {
        // reset slider values
        slider1.value = 0;
        slider2.value = 0;
        slider3.value = 0;
        slider4.value = 1;
        slider5.value = 0;
        slider6.value = 0;


        // reset panels
        panel1.ResetPanel();
        panel2.ResetPanel();
        panel3.ResetPanel();
        panel4.ResetPanel();

        // reset gameobjects back to their original states
        slider1.gameObject.SetActive(false);
        slider3.gameObject.SetActive(false);
        button2.SetActive(true);
        button3Parent.SetActive(false);

        // reset button collors
        SetBtn3Color(0, 0);
        SetBtn3Color(1, 2);
        SetBtn3Color(2, 3);

        // reset other variables
        p2ButtonsLocked = false;
        slider3.gameObject.SetActive(false);
        p4StartButton.SetActive(true);

        p3MaskGraphic.gameObject.SetActive(false);
        p3HintArrows.SetActive(false);
        p3DemoBG.SetActive(true);

        // reset all toggle buttons
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

        Vector2 resetAnchors = new Vector2(maskXMinMax.x, maskYMinMax.y);
        p3MaskGraphic.anchorMin = resetAnchors;
        p3MaskGraphic.anchorMax = resetAnchors;

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
            button2.SetActive(false);
        }
    }

    public void OnFallingButtonFall(GameObject g)
    {
        // restart the challenge if a button falls of screen - but ONLY if it's an un-toggled button, or a slider
        ToggleButton t = g.GetComponent<ToggleButton>();
        if (t != null && t.toggled) return;

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

        // do insurance stuff - if any slider is more than half full and the mouse button is up, finish filling it for them - otherwise, make it go back down
        if (Input.GetMouseButtonDown(0)) mouseDown = true;
        if (Input.GetMouseButtonUp(0)) mouseDown = false;

        if (!mouseDown)
        {
            ApplySliderInsurance(slider1);
            ApplySliderInsurance(slider2);
            ApplySliderInsurance(slider3);
            
            ApplySliderInsurance(slider6);
            if (slider7 != null) ApplySliderInsurance(slider7);
        }
    }

    private void ApplySliderInsurance(Slider s)
    {
        if (s.value > insuranceFillThreshold)
        {
            s.value = Mathf.Clamp(s.value + insuranceFillSpeed * Time.deltaTime, 0, 1);
        }
        else
        {
            s.value = Mathf.Clamp(s.value - insuranceFillSpeed * Time.deltaTime, 0, 1);

        }
    }
}
