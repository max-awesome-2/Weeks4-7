using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    // current y velocity
    float yVelocity;

    // acceleration value to be applied every frame
    public float gravity = -10;

    // reference to the gamemanager script
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // apply gravity to velocity, then velocity to position
        yVelocity += gravity * Time.deltaTime;

        transform.position += Vector3.up * yVelocity * Time.deltaTime;

        // if we're off the edge of the screen, inform the gamemanager that a button has fallen
        if (transform.position.y < transform.lossyScale.y)
        {
            Destroy(gameObject);
            manager.OnFallingButtonFall();
        }
    }
}
