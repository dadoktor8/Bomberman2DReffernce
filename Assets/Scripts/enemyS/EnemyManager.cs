using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public tileSetManager ts;                                          // Reffernce free tiles for enemy instantiation. 
    //------------------------------------------------------- Enemy Instantiation.
    public static EnemyManager instance_EM; 
    private bool hasWon = false;
    private int activeCurrent; 
    //public static int level; 
    private List<GameObject> c_e = new List<GameObject>(); 
    public EnemySM chnageS;
    private List<GameObject> enemies = new List<GameObject>(); 
    [SerializeField]
    private int NoOfEnemies; 
    [SerializeField]
    private GameObject enemies_Prefab;

    private bool  isDone  = false; 
    void Awake()
    {
        //ts = AddComponent<tileSetManager>(); 
        if(instance_EM == null)
        {
            instance_EM = this; 
        }

    }
    void Start()
    {
        GenerateEnemies();
        InstantiateEnemies();   
        //chnageS.SwitchS(chnageS.enemyFleeingState);
    }


    void Update()
    {
        //chnageS.SwitchS(chnageS.enemyFleeingState);
         
        if(!isDone){
            ChangeState(chnageS); 
             
        }

        //getLevel();
        //getScore();
        
            
    }

    void GenerateEnemies()
    {
        for(int i = 0; i < NoOfEnemies; i++)
        {
            GameObject e = Instantiate(enemies_Prefab); 
            e.SetActive(false);
            enemies.Add(e); 
        }
    }


    public GameObject getEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(!enemies[i].activeInHierarchy)
            {
                return enemies[i]; 
            }
           
        }
        return null; 
    }


    void InstantiateEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(!enemies[i].activeInHierarchy)
            {
                foreach (GameObject e in enemies)
                {
                    //Debug.Log(ts.GetComponent<tileSetManager>().freeTiles.Count()); 
                    e.transform.position = ts.GetComponent<tileSetManager>().freeTiles[Random.Range(0,ts.freeTiles.Count)].transform.position; 
                    e.SetActive(true);                  
                }   
            }
        }   
    }


    void ChangeState(EnemySM enemy)
    {
        c_e = enemies.Where(obj => obj.activeInHierarchy).ToList();
        activeCurrent = c_e.Count;  
       // Debug.Log(activeCurrent);
        if(activeCurrent < 3){
            foreach(var obj in c_e)
            {
                //Debug.Log(activeCurrent);
                obj.GetComponent<EnemySM>().SwitchS(enemy.enemyFleeingState); 
                isDone = true; 
            }     
        }
    }

    public int getScore()
    {
        c_e = enemies.Where(obj => obj.activeInHierarchy).ToList();
        activeCurrent = c_e.Count; 
        //Debug.Log(activeCurrent);
        return NoOfEnemies - activeCurrent; 
    }

    public bool getLevel()
    {
        c_e = enemies.Where(obj => obj.activeInHierarchy).ToList();
        activeCurrent = c_e.Count; 
       // Debug.Log(activeCurrent);
        if(activeCurrent == 0)
        {
            return true;
            
        }
        return false;
    }

    public bool getVict()
    {
        return hasWon; 
    }
}
