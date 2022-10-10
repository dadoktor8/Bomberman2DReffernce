using UnityEngine;

public class ERS : EBS
{

    private int moveSpeed = 1; 
    //private bool isStuck; 
    private Vector2 lastFrame;
    private bool isBlocked = false; 
    private Vector2 changeValues = new Vector2(2,3); 
    private float changeValueHolder = 0;
    private Vector3[] directions = new Vector3[] {Vector3.right, Vector3.left, Vector3.up, Vector3.down};       // Holds all the direction
    private int currentDirection;  


    public override void EnterS(EnemySM enemy)
    {
        //Debug.Log("Is Currently Running");
        changeValueF();                                                                                         //Called once during  load to be provided a random set of values
        ChooseDirection();                                                                                      // Called once during load for a random direction


       
    }
    public override void UpdateS(EnemySM enemy)
    {
        lastFrame = enemy.transform.position;
        enemy.transform.position += directions[currentDirection].normalized * Time.deltaTime * moveSpeed; 
        ifBlocked(); 

        

        if(changeValueHolder > 0)
        {
            changeValueHolder -= Time.deltaTime; 
        }
        else 
        {
            //Debug.Log(currentDirection); 
            changeValueF();                                                                                    //Called during collision with block or player
            ChooseDirection(); 
            ifStuck(enemy);
        }
        
    }
    public override void OnCollisionEnter2D(EnemySM enemy, Collision2D col)
    {
        GameObject obj = col.gameObject;
        if(obj!= null){
            if(obj.CompareTag("Bomb")||obj.transform.gameObject.layer == 7)
            {
                isBlocked = true; 
                //Debug.Log(isBlocked); 
            }
        }
    }

    public override void OnCollisionExit2D(EnemySM enemy, Collision2D col)
    {
      GameObject obj = col.gameObject;
        if(obj.CompareTag("Bomb")||obj.transform.gameObject.layer == 7)
        {
            isBlocked = false; 
        }

    }
    void changeValueF()
    {
        changeValueHolder = Random.Range(changeValues.x,changeValues.y);
    }
    void ChooseDirection()
    {
        currentDirection = Mathf.FloorToInt(Random.Range(0,directions.Length)); 
    }

    int getDirectionOpp()                                                                       // The brain of the AI
    { 
        int case_ = currentDirection; 
        switch(currentDirection)
        {
            case 0:
                case_ = 1; 
                break;
            case 1:
                case_ = 0; 
                break;
            case 2:
                case_ = 3;
                break;
            case 3:
                case_ = 2;
                break;
            default:
                case_ = currentDirection;
                break;  
        }
        return case_; 
    }

    void ifBlocked()
    {
        if(isBlocked)
        {
            currentDirection = getDirectionOpp(); 
            isBlocked = false;
        }
    }

    void ifStuck(EnemySM enemy)
    {
        if(Vector2.Distance((Vector2)enemy.transform.position, lastFrame) <= 1.0f)
        {
            Vector2.Lerp(enemy.transform.position, new Vector3(enemy.transform.position.x + 2f, 0, 0 ), 0.4f);
        }

        lastFrame = enemy.transform.position; 
    }
}
