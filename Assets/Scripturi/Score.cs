using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
   
    private float score= 0.0f;
    
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 4;
    private int scoreToNextLevel = 70;

    private bool isDead = false;



    public Text scoreText;
    public Text finalscoreText;
    public GameObject deathMenu;


    // Update is called once per frame
    void Update()
    {
        
        if (isDead)
            return;

        if (score >= scoreToNextLevel)
        {
            LevelUp();

        }

        score += Time.deltaTime * difficultyLevel;
        
        scoreText.text = "Score: " + ((int)score).ToString();
      
    }
    public void AddScore(float pointsToAdd)
    {
        score += pointsToAdd;
    }
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        
            return;
            scoreToNextLevel *= 2;
            difficultyLevel++;
            GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);

            Debug.Log(difficultyLevel);
        
    }
    public void OnDeath()
    {

        isDead = true;
        scoreText.text = "Score: " + ((int)score).ToString();
        finalscoreText.text = "Final Score: " + ((int)score).ToString();
        AccountInfo.Instance.SetStats("SCORE", (int)score);
        deathMenu.SetActive(true);

        
        
    }

     public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToMenu()
    {
        
        SceneManager.LoadScene("Menu");


    }

 

  
}