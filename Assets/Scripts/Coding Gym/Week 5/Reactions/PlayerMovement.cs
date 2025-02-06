using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        move.x = x;
        move.y = y;

        if (move.magnitude > 1)
            move = move.normalized;


        move *= moveSpeed * Time.deltaTime;

        transform.position += move;

    }
}
