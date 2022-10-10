using UnityEngine;

public abstract class EBS 
{
    public abstract void EnterS(EnemySM enemy); 
    public abstract void UpdateS(EnemySM enemy);
    public abstract void OnCollisionEnter2D(EnemySM enemy,Collision2D col); 
    public abstract void OnCollisionExit2D(EnemySM enemy,Collision2D col);
}
