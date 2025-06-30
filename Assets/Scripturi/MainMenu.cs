using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   

    
 

    public void StartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
    public GameObject ChangeRulesPanel;


    public void closeRulesPlanel()
    {

        ChangeRulesPanel.SetActive(false);
    }
    public void openRulesPanel()
    {
        
        ChangeRulesPanel.SetActive(true);
    }

}
