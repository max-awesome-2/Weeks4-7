using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Button : MonoBehaviour
{

    /*public boolean to check if button is active
     * public boolean to check if button is pressed
     * spriterender of button 
     * 
     * 
     * timer float
     * public float for timer break
     * 
     * assign componenent to sprite render
     * getcurrent color value
     * 
     *if pressed is true (interected with by player)
     *change color to darker color
     *timer using delta time to incrementally increase
     *active is switched
     *
     *if statement running when timer reaches break value
     *set pressed to false
     *set color back to originial
     */

    public SpriteRenderer button;
    Color original;
    public bool active;
    public bool pressed;

    float timer = 0;
    public float breakPoint;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<SpriteRenderer>();
        original = button.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed == true)
        {
            Color interact = button.color;
            interact.b = original.b - 100;
            interact.g = original.b - 100;
            interact.r = original.b - 100;
            button.color = interact;

            timer += Time.deltaTime;
        }

        if(timer == breakPoint)
        {
            pressed = !pressed;
            button.color = original;
        }
    }
}
