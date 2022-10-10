using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileBehaviour : MonoBehaviour
{ 
    private bool isCollisionTile; 
    public  bool checkTileCollision()
    {
        if(isCollisionTile)
        {
            return true;
        }
        return false; 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        isCollisionTile = true; 

    }

}
