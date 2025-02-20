using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : Collideable
{
    /*
     * 
     * private rect myrect
     * 
     * variables for jumping:
     * grounded = false;
     * public float gravity = -9.8f
     * public float fallingGravityMult = 1.5; (little cheat thing - gravity is faster when falling, makes movement feel more responsive)
     * 
     * variables for moving:
     * float movespeed = 10f;
     * 
     * (keep a velocity value for y, but not x)
     * yVelocity;
     * public float jumpVelocity = 20;
     * private float groundedVelocity = -0.01;
     * 
     * void start {
     *  myrect = new rect(transform.position, transform.localscale);
     * }
     * 
     * void update {
     * 
     *  (movement code)
     *  float x = input.getaxis horizontal
     *  
     *  tranform.position += vector3.right * x * movespeed * time.deltatime
     *  
     *  
     *  (jumping / vertical movement code)
     *  
     *  if (grounded) {
     *  
     *      if (input.getkeydown space) {
     *      
     *          grounded = false
     *          yvelocity = jumpvelocity
     *      } else {
     *          (while grounded, set velocity to a consistent -0.01)
     *          yvelocity = groundedVelocity;
     *      }
     *      
     *      
     *      
     *  } else {
     *  
     *     // add gravity, 
     *     
     *     yVelocity += gravity * (yVelocity < 0 ? fallingGravityMult : 1);

     *  }
     * 
     * 
     *  (check collisions with all rects)
     *  for each rect in rectlist:
     *      int collisiondirec = collides (this, rect):
     *      
     *     if (collisiondirec >= 0) {
     *     
     *      (place ourself at the side of the object / resolve collision)
     *      bool widthOrHeight = (collisiondirec % 2 == 1) // bool telling us whether to use the width or height to replace
     *      
     *      transform.position = rect.position + ((collisionDirec < 2 ? 1 : -1) * ((widthOrHeight ? rect.width/2 + myRect.width/2 : rect.height/2 + myRect.height/2)));
     *      
     *      if (collisionDirec == 0)
     *          grounded = true;
     *          yvelocity = groundedVelocity;
     *     }
     * 
     * }
     * 
     * 
     * 
     * 
     * 
     */


    private bool grounded = false;
    public float gravity = -9.8f;
    public float fallingGravityMult = 1.5f;

    // move variables
    public float moveSpeed = 10f;

    float yVelocity = 0;
    public float jumpVelocity = 20;
    private float groundedVelocity = -0.01f;


    private void Start()
    {
        GenerateRect();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.position += Vector3.right * x * moveSpeed * Time.deltaTime;

        if (grounded) {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                grounded = false;
                yVelocity = jumpVelocity;
            } else
            {
                // while grounded set velocity to consistent negative value
                yVelocity = groundedVelocity;
            }

        } else
        {
            // add gravity

            yVelocity += gravity * (yVelocity < 0 ? fallingGravityMult : 1);
        }

        // check collisions
        foreach (Collideable c in CameraManager.instance.allCollideables)
        {
            int collisionDirec = c.rect.CollidesWith(rect);

            if (collisionDirec >= 0)
            {

                bool widthOrHeight = (collisionDirec % 2 == 1);

                transform.position = c.transform.position + ((collisionDirec < 2 ? 1 : -1) * ((widthOrHeight ? (c.rect.width / 2 + rect.width / 2) : (c.rect.height / 2 + rect.height / 2))));

            }
        }
    }

}
