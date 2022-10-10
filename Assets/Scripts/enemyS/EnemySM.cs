using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : MonoBehaviour
{
    EBS currentS; 
    public EFS enemyFleeingState = new EFS(); 
    public ERS enemyRunningState = new ERS(); 
    

    void Start()
    {
        
        ignoreColl();
        //currentS = enemyRunningState; 
        currentS = enemyRunningState; 

        currentS.EnterS(this); 
    }

    // Update is called once per frame
    void Update()
    {
         
        currentS.UpdateS(this);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col !=null)
            currentS.OnCollisionEnter2D(this,col);                      // Error points here due to load order of initialization! Rendering the awake initialization of colliders null
    }

    void OnCollisionExit2D(Collision2D col)
    {
        currentS.OnCollisionExit2D(this,col); 
    }

    public void SwitchS(EBS state)
    {
        currentS = state;
        state.EnterS(this); 
    }

    public void ignoreColl()            //State Independent
    {
        Physics2D.IgnoreLayerCollision(3,3,true); 
        Physics2D.IgnoreLayerCollision(3,6,true); 
    }

}
