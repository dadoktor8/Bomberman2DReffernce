using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileSetManager : MonoBehaviour
{
    //--------------------------------------------Randomness Variables
    private int randomValueX;
    private int randomValueY;
    private List<int> usedValueX = new List<int>(); 
    private List<int> usedValueY = new List<int>();
    //------------------------------------------- Grid Variables
    [SerializeField]
    private int rows = 15, collumns = 10;
    [SerializeField]
    private GameObject tile; 
    [SerializeField]
    private Camera cam; 

    private List<GameObject> allTiles;
    public  List<GameObject> freeTiles = new List<GameObject>();

    //-------------------------------------------- Block Generation Variables 
    public static tileSetManager instance;                                          // An instance of the tileSet class for generation and storing of blocks. 
    private List<GameObject> blocks = new List<GameObject>(); 
    private List<GameObject> blocksDeploy = new List<GameObject>(); 
    [SerializeField]
    private int noOfBlocks;                                                         //Keep value less to reduce chaos and avoid overlapping
    [SerializeField]
    private GameObject[] blocksPrefab; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }

        GenerateTile();                                                         // Call function for tile generation
        GenerateBlock();                                                        //Call function for block generation
        instantiateBlocks();                                                    //Call function to Display Block 
        getFreeTiles();
        
    }
    private void Start() {
        


        Debug.Log(freeTiles.Count);
    }
    public int  randomnessFunctionX()
    {
        //randomValueX = 2 + (randomValueX - 2 + Random.Range(1,rows - 2)) % (rows - 2);  // Return a non repeating value for the X pos
        randomValueX = Random.Range(2,rows);
        while(usedValueX.Contains(randomValueX))
        {
            //randomValueX = 2 + (randomValueX - 2 + Random.Range(1,rows - 2)) % (rows - 2);
            randomValueX = Random.Range(2,rows + 1);
        }

        return randomValueX; 
    }

    public int randomnessFunctionY()
    {
        //randomValueY = 2 + (randomValueY - 2 + Random.Range(1,collumns - 2)) % (collumns - 2); // Return a non repeating value for the Y pos
        randomValueY = Random.Range(2,collumns);
        while(usedValueY.Contains(randomValueY))
        {
            //randomValueY = 2 + (randomValueY - 2 + Random.Range(1,collumns - 2)) % (collumns - 2);
            randomValueY = Random.Range(2,collumns + 1);
        }

        return randomValueY; 
    }

    void GenerateTile() {          
        allTiles = new List<GameObject>();          
        for(int x = 0; x < rows; x++)                                           //Simple 2D Matrix to generate tiles
        {
            for(int y = 0; y < collumns; y++)
            { 
                GameObject tile_ins = Instantiate(tile, new Vector3(x,y), Quaternion.identity);                             //The actual instantiation. 
                tile_ins.name = $"Tile {x} {y}"; 


                allTiles.Add(tile_ins); 
            }
        }
        cam.transform.position = new Vector3((float)rows/2 -0.5f,(float)collumns/2 - 0.5f, -10 ); 
    }

    public void getFreeTiles()
    {
        foreach(GameObject t in allTiles)
        {
            if(!t.GetComponent<tileBehaviour>().checkTileCollision())
            {
                freeTiles.Add(t); 
            }
        }
    }

    void GenerateBlock()                                                         // Simple for loop to generate the blocks                
    {
        for(int i = 0; i < noOfBlocks; i++)
        {
            GameObject blocks_ins = Instantiate(blocksPrefab[Random.Range(0,blocksPrefab.Length)]);                              //Depending on the value of the range method we will get either destructible or indestructible block
            blocks_ins.SetActive(false); 
            blocks.Add(blocks_ins); 
        }
    }

    public GameObject getBlocks()                                                                                               // To return blocks if no longer used                        
    {
        for(int i = 0; i < blocks.Count; i++)
        {
            if(!blocks[i].activeInHierarchy)
            {
                return blocks[i]; 
            }
        }
        return null; 
    }

    public void instantiateBlocks()                                                                                         // Calls all the block during load 
    {
        for(int i = 0; i < blocks.Count; i++)
        {
            if(!blocks[i].activeInHierarchy)
            {
                foreach (GameObject b in blocks)
                {
                    b.transform.position = new Vector3(randomnessFunctionX(),randomnessFunctionY());
                    b.SetActive(true); 
                }
            }
        }
    }








}
