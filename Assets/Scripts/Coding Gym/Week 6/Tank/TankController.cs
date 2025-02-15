using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    /*
     * Psuedocode:
     * 
     * vector2 move
     * set move x to input axis horizontal
     * translate tank position by move * speed * time.deltatime
     * 
     * bind tank position between sides of screen
     * 
     * get mouseposition
     * get world position of mouse
     * use mathf.atan2 to get angle between tank and mouse
     * apply angle to tank barrel
     * 
     * on mouse click:
     *  spawn prefab bullet
     *  access prefab bullet script and pass in the movement direction
     * 
     * extra functions to be used by ui settings script:
     * setmovement, setsize
     * 
     * 
     */

    public float moveSpeed;

    private Vector2 screenBounds;
    

    // Start is called before the first frame update
    void Start()
    {
        // calculate and store screen bounds
        screenBounds = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -Camera.main.transform.position.z)).x);
    }

    // Update is called once per frame
    void Update()
    {

        // tank movement
        Vector2 move = new Vector3(Input.GetAxis("Horizontal"), 0);

        transform.Translate(move * moveSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenBounds.x, screenBounds.y), transform.position.y, transform.position.z);

        // barrel rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 targPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direc = targPos - transform.position;

        float angle = Mathf.Atan2();

        
    }
}
