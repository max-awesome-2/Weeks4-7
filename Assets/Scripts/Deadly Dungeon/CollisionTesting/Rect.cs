using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rect
{
   /*
    * x, y, x2, y2
    * width / height
    * 
    * detect collision (rect)
    *   if (a.x1 < b.x2 && a.x2 > b.x1 && a.y1 > b.y2 && a.y2 < b.y1)
    *       collision
    *       
    *    vector3 direction = playerposition - transform.position
    *    
    *    bool xNegative = direction.x < 0;
    *    bool yNegative = direction.y < 0;
    *    
    *    bool xMagGreater = mathf.Abs(direction.x) > mathf.abs(direction.y);
    *    
    *    collisionside = xMagGreater ? (xNegative ? 3 : 1) : (yNegative ? 2 : 0);
    *    
    * move(newx, newy)
    *   set x, y, x2, y2 based on new pos + width and height 
    *   
    *   
    * 
    */
}
