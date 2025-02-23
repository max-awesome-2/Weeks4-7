using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePanel : MonoBehaviour
{

    // direction from which this panel slides in
    public Vector2 slideDirection;

    // the anchors of the object when it's slid into the screen
    public Vector2 inAnchorsMin, inAnchorsMax;

    // anchors when it's slid out of the screen
    private Vector2 outAnchorsMin, outAnchorsMax;

    private RectTransform rt;

    // determines whether this panel is reset to its OUT anchors or its  IN anchors when the reset method is called
    public bool resetToOut = true;

    // used to keep track of whether the anchors have been calculated or not - since we're calling reset from the GameManager script in its Start method we might get some weird behaviour
    // unless we ensure that initialization of the panels happens before the resetpanel method does its stuff
    private bool initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!initialized) Initialize();
    }

    private void Initialize()
    {
        initialized = true;
        rt = GetComponent<RectTransform>();

        // get anchors
        inAnchorsMin = rt.anchorMin;
        inAnchorsMax = rt.anchorMax;

        outAnchorsMin = inAnchorsMin + slideDirection;
        outAnchorsMax = inAnchorsMax + slideDirection;

        rt.anchorMin = outAnchorsMin;
        rt.anchorMax = outAnchorsMax;
    }

    public void Slide(float val)
    {
        // lerp anchors between min and max by val
        rt.anchorMin = Vector3.Lerp(outAnchorsMin, inAnchorsMin, val);
        rt.anchorMax = Vector3.Lerp(outAnchorsMax, inAnchorsMax, val);

    }

    // called at the very start of the game, then at the end of the playthrough to reset everything
    public void ResetPanel()
    {
        if (!initialized) Initialize();

        // reset anchors to either inside or outside anchors depending on the resetToOut variable
        rt.anchorMin = resetToOut ? outAnchorsMin : inAnchorsMin;
        rt.anchorMax = resetToOut ? outAnchorsMax : inAnchorsMax;

    }
}
