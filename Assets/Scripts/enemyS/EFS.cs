using UnityEngine;

public class EFS : EBS
{

    private int  moveSpeed = 2;
    private Vector2 lastFrame;
    private bool isBlocked;
    private Vector2 changeValues = new Vector2(2,3); 
    private float changeValueHolder = 5;
    private int selectV; 
    private bool inPos; 
    private float[] angles ={180f, 135f, 90f, 45f, 0f, -45f, -90f, -135f, -180f}; 
    private Vector3[] directions = new Vector3[] {Vector3.right, Vector3.left, Vector3.up, Vector3.down}; // Direction Cheat Sheet :)
    private Vector2[] endTiles = new Vector2[] { new Vector2(0f,0f), new Vector2(0f,9f), new Vector2(14f,9f), new Vector2(14f,0f)}; 
    public int layerIgnore = 3; 
    //int layerIgnore_ = ~(1 << 3); 
    private int currentDirection; 
    private int currentDirection_; 

    public override void EnterS(EnemySM enemy)
    {
        //Debug.Log(calculateDist(enemy)); 
        
    }
    public override void UpdateS(EnemySM enemy)
    {
        lastFrame = enemy.transform.position;
        Debug.DrawLine (enemy.transform.position, calculateDist(enemy), Color.yellow);
        //Debug.Log("Angle is " + retunrAngle(enemy)); 
        //Debug.Log(isBlocked);
        onstart(enemy); 
        //posCheck(enemy);
        changeState(enemy);
        //Debug.Log(inPos);
    }
    public override void OnCollisionEnter2D(EnemySM enemy, Collision2D col)
    {
        GameObject obj = col.gameObject;
        if(obj!= null){
            if(obj.CompareTag("Bomb") || obj.transform.gameObject.layer == 7)
            {
                isBlocked = true; 
                //Debug.Log(isBlocked); 
            }
        }
       
    }

    public override void OnCollisionExit2D(EnemySM enemy, Collision2D col)
    {
        GameObject obj = col.gameObject;
        if(obj != null){
            if(obj.CompareTag("Bomb") || obj.transform.gameObject.layer == 7)
            {
                isBlocked = false; 
                //Debug.Log(isBlocked); 
            }
        }

    }

    private  Vector2 calculateDist(EnemySM enemy)
    {   
        float minDist = float.MaxValue;
        int nearestIndex = -1; 
        for(int i = 0; i < endTiles.Length; i++)
        {
            var dist = Vector2.Distance(enemy.transform.position,endTiles[i]); 
            if(dist < minDist)
            {
                nearestIndex = i; 
                minDist = dist; 
            }
        }
        return endTiles[nearestIndex]; 

    }

    private void onstart(EnemySM enemy)
    {
        Vector2 getPos = calculateDist(enemy); 
        if(!isBlocked)
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,getPos,moveSpeed * Time.deltaTime); 
        else if(isBlocked)
        {
            enemy.transform.position += directions[OnBlock(enemy)].normalized * Time.deltaTime * moveSpeed;   
            ifStuck(enemy);
        }
    }
    


    private float retunrAngle(EnemySM enemy)
    {
        Vector2 nDist = ((Vector2)enemy.transform.position - calculateDist(enemy)).normalized; 
        float angleRad = Mathf.Atan2(nDist.x , nDist.y);
        float angle = (180 / Mathf.PI) * angleRad; 
        return angle; 
    }


    private int OnBlock(EnemySM enemy)                                                  // The brain of the AI
    {
        if(retunrAngle(enemy) <= angles[0] && retunrAngle(enemy) > angles[1])
        {
            currentDirection = 0; 
            if(isBlocked)
                currentDirection = 1;
        }
        else if(retunrAngle(enemy) <= angles[1] && retunrAngle(enemy) > angles[2])
        {
            currentDirection = 0; 
            if(isBlocked)
                currentDirection = 2;
        }
        else if(retunrAngle(enemy) <= angles[2] && retunrAngle(enemy) > angles[3])
        {
            currentDirection = 2; 
            if(isBlocked)
                currentDirection = 3;
        }
        else if(retunrAngle(enemy) <= angles[3] && retunrAngle(enemy) > angles[4])
        {
            currentDirection = 0; 
            if(isBlocked)
                currentDirection = 3;
        }
        else if(retunrAngle(enemy) <= angles[4] && retunrAngle(enemy) > angles[5])
        {
            currentDirection = 0; 
            if(isBlocked)
                currentDirection = 3;
        }
        else if(retunrAngle(enemy) <= angles[5] && retunrAngle(enemy) > angles[6])
        {
            currentDirection = 1; 
            if(isBlocked)
                currentDirection = 0;
        }
        else if(retunrAngle(enemy) <= angles[6] && retunrAngle(enemy) > angles[7])
        {
            currentDirection = 1; 
            if(isBlocked)
                currentDirection = 3;
        }
        else if(retunrAngle(enemy) <= angles[7] && retunrAngle(enemy) > angles[8])
        {
            currentDirection = 3; 
            if(isBlocked)
                currentDirection = 2;
        }
        else if(retunrAngle(enemy) <= angles[8] && retunrAngle(enemy) > angles[9])
        {
            currentDirection = 1; 
            if(isBlocked)
                currentDirection = 0;
        }
        return currentDirection; 
    }


    private bool posCheck(EnemySM enemy)
    {
        //var dist = 0.2f; 
        
        
        if(calculateDist(enemy).Equals(enemy.transform.position))
        {
            inPos = true;
               
        }
        else
        {
            inPos = false;
            //Debug.Log(calculateDist(enemy));
            //Debug.Log("Not Chnaging State");
        }
        
        return inPos; 
    }

    private void changeState(EnemySM enemy)
    {
        if(posCheck(enemy) && changeValueHolder > 0)
        {
            enemy.SwitchS(enemy.enemyRunningState); 
            changeValueHolder -= Time.deltaTime;
        }
        else
        {
            changeValueHolder = 5;
        }
    
    }

    void ifStuck(EnemySM enemy)
    {
        if(Vector2.Distance((Vector2)enemy.transform.position, lastFrame) <= 0.7f)
        {
            Vector2.Lerp(enemy.transform.position, new Vector3(enemy.transform.position.x + 2f, enemy.transform.position.y - 1f, 0 ), 0.4f);
        }

        lastFrame = enemy.transform.position; 
    }
}
