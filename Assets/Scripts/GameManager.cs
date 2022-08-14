using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    public List<GameObject> targets;
    private float spawnRate=3.0f;


    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnTarget()
    {   
        while(isGameActive)
        {
        yield return new WaitForSeconds(spawnRate);
        int index=Random.Range(0,targets.Count);
        Instantiate(targets[index]);

        }
    }
    public void GameOver(){

        gameOverText.gameObject.SetActive(true);
        isGameActive=false;
        restartButton.gameObject.SetActive(true);
    }    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void UptadeScore(int scoreToAdd)
    {
        score +=scoreToAdd;
        scoreText.text="Score: " + score;
    }
    public void StartGame(int difficulty)
    {
        spawnRate= spawnRate/difficulty;
        isGameActive=true;
        StartCoroutine(SpawnTarget());
        score=0;
        UptadeScore(0);
        titleScreen.gameObject.SetActive(false);
        
    }
}
