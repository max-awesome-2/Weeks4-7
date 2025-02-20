using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Rect
{
    /*
     * x, y, x2, y2
     * width / height
     * 
     * public Rect(vector3 pos, vector3 scale) {
     *   x = pos.x;
     *   y = pos.y;
     *   width = scale.x
     *   height = scale.y
     * }
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

    public float x1, y1, x2, y2;
    public float cX, cY;
    public float width, height;

    public Rect(Vector3 pos, Vector3 scale)
    {
      
        width = scale.x;
        height = scale.y;

        x1 = pos.x - width/2;
        y1 = pos.y + height/2;
        x2 = pos.x + width / 2;
        y2 = pos.y - height / 2;
        cX = pos.x;
        cY = pos.y;
    }

    /// <summary>
    /// Returns int indicating direction FROM a to b - returns -1 if no collision
    /// </summary>
    /// <returns></returns>
    public int CollidesWith(Rect B)
    {
        bool collision = x1 < B.x2 && x2 > B.x1 && y1 > B.y2 && y2 < B.y1;

        if (!collision) return -1;

        Vector3 direc = new Vector3(B.cX, B.cY) - new Vector3(cX, cY);

        bool xNegative = direc.x < 0;
        bool yNegative = direc.y < 0;
        
        bool xMagGreater = Mathf.Abs(direc.x) > Mathf.Abs(direc.y);
        
        int collisionDirec = xMagGreater ? (xNegative ? 3 : 1) : (yNegative ? 2 : 0);

        Debug.Log("collision direc: " + collisionDirec);

        return collisionDirec;
    }

    public void UpdateRect(Vector3 pos)
    {
        x1 = pos.x - width / 2;
        y1 = pos.y + height / 2;
        x2 = pos.x + width / 2;
        y2 = pos.y - height / 2;
        cX = pos.x;
        cY = pos.y;
    }
}
