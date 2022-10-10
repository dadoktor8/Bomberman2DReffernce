using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText; 
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private GameObject wonT; 
    [SerializeField]
    private GameObject lostT;
    [SerializeField]
    private GameObject ds; 
    [SerializeField]
    private GameObject menu;
    private int enemyScore;
    private static int currentLevel = 1; 
    private static bool firstOpen = true;
    void Start()
    {
        if(firstOpen)
            Time.timeScale = 0;
        else{

            Time.timeScale = 1;
            menu.SetActive(false);
        }
    }


    void Update()
    {
        scoreUpdate(); 
        deathUI();
        wonUI();
    }

    void scoreUpdate()
    {
        enemyScore = EnemyManager.instance_EM.getScore(); 
        //currentLevel = EnemyManager.instance_EM.getLevel();
        scoreText.text = "Score: " + enemyScore; 
        levelText.text = "Level: " + currentLevel;
    }

    void deathUI()
    {
        if(playerBehaviour.instance_p.getDeath())
        {
            lostT.SetActive(true);
            ds.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void wonUI()
    {
        Debug.Log(EnemyManager.instance_EM.getLevel());
        if(EnemyManager.instance_EM.getLevel())
        {
            wonT.SetActive(true);
            StartCoroutine(onWin());
            
        }
    }

    IEnumerator onWin()
    {
        yield return new WaitForSeconds(1.0f);
        currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }

    public void menuS()
    {
        if(firstOpen){
            Time.timeScale = 1;
            menu.SetActive(false);
            firstOpen = false;
        }

    }
}
