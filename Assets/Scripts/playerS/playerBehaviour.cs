using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class playerBehaviour : MonoBehaviour
{                   
    public static playerBehaviour instance_p;
    private bool isMoving;
    public bool isDead = false;
    private float bCd = 3.0f; 
    private Vector3[] directions = new Vector3[] {Vector3.right, Vector3.left, Vector3.up, Vector3.down};
    private float moveSpeed = 3; 
    private Vector3 originalP, targetP; 
    private bool bombAvailabe = true;
    [SerializeField]
    private AudioClip[] ac; 
    private AudioSource sound;
    //private float timeTomove = 0.2f; 
    void Awake()
    {
        if(instance_p == null)
        {
            instance_p = this;
        }
    }
    void Start()
    {
        sound = GetComponent<AudioSource>();
        transform.position = new Vector3(0,9,0); 
    }

    // Update is called once per frame
    void Update()
    {
        moveMent(); 
        ignoreColl();
        bombTimer();
       
    }

    private void moveMent()
    {
        if(Input.GetKey(KeyCode.W))
        {
           transform.position += directions[2] * moveSpeed * Time.deltaTime; 
        }

        if(Input.GetKey(KeyCode.S))
        {
           transform.position += directions[3] * moveSpeed * Time.deltaTime; 
        }

        if(Input.GetKey(KeyCode.A))
        {
           transform.position += directions[1] * moveSpeed * Time.deltaTime; 
             
        }

        if(Input.GetKey(KeyCode.D))
        {
           transform.position += directions[0] * moveSpeed * Time.deltaTime; 
             
        }

        if(Input.GetKey(KeyCode.Space))
        {
            GameObject bomb = bombManager.instance_bm.getBombs(); 
            if(bomb!= null && bombAvailabe)
            {
                sound.clip = ac[0];
                sound.Play();
                bombAvailabe = false;
                bomb.transform.position = this.transform.position; 
                bomb.SetActive(true);
            }
        }
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    void ignoreColl()
    {
        Physics2D.IgnoreLayerCollision(0,6,true); 
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
            isDead = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void bombTimer()
    {
        if(!bombAvailabe && bCd > 0)
        {
            bCd -= Time.deltaTime;
            if(bCd < 0.3f){
                sound.clip = ac[1];
                sound.Play();}
        }
        else
        {
            bCd = 3.0f;
            bombAvailabe = true;

        }
    }

    public bool getDeath()
    {
        return isDead; 
    }




}
