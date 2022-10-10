using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombManager : MonoBehaviour
{
    public static bombManager instance_bm; 
    private List<GameObject> bombs = new List<GameObject>();
    [SerializeField]
    private int noOfBombs;
    [SerializeField]
    private GameObject bomb_prefab;

    void Awake()
    {
        if(instance_bm == null)
        {
            instance_bm = this;
        }
    }

    void Start()
    {
        GenerateBombs();
    }

    void GenerateBombs()
    {
        for(int i = 0; i < noOfBombs; i++)
        {
            GameObject b = Instantiate(bomb_prefab);
            b.SetActive(false);
            bombs.Add(b);
        }
    }

    public GameObject getBombs()
    {
        for(int i = 0; i < bombs.Count; i++)
        {
            if(!bombs[i].activeInHierarchy)
            {
                return bombs[i];
            }
        }
        return null; 
    }
}
