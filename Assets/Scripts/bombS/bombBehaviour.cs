using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bombBehaviour : MonoBehaviour
{
    private AudioSource bs; 
    void Start()
    {
        bs = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        bs.Play();
        yield return new WaitForSeconds(3.0f);
        Collider2D[] hitColl = Physics2D.OverlapCircleAll(this.transform.position, 3f);
        foreach(var hitCol in hitColl)
        {
            if(hitCol.gameObject.tag == "Enemy" || hitCol.gameObject.tag == "Block_des")
            {
                hitCol.gameObject.SetActive(false);
            }

            if(hitCol.gameObject.tag == "Player")
                playerBehaviour.instance_p.isDead = true;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        this.gameObject.SetActive(false);

    }
}
