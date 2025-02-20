using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
        allCollideables = new List<Collideable>();
    }

    /*
     * arraylist<Collidable> allwalls
     * 
     * int gameState;
     * 
     * void update {
     *   depending on game state, go up or down
     * }
     * 
     */

    public List<Collideable> allCollideables;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollideable(Collideable c)
    {
        allCollideables.Add(c);
    }
}
