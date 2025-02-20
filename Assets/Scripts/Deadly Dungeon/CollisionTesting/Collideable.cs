using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour
{

    /*
     * parent class for everything with a rect
     * 
     * public Rect rect;
     * 
     * public void GenerateRect() {
     *  rect = new Rect(transform.position, transform.localscale);
     * }
     */

    public Rect rect;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRect();
    }

    public void GenerateRect()
    {
        rect = new Rect(transform.position, transform.localScale);

    }

}
