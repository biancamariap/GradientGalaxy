using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
#pragma warning disable 0649
public class UIManager : MonoBehaviour
{
    private AccountInfo info;
    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }



    [SerializeField]
    private Text playerName;



   

    public static Text PlayerName
    {
        get { return Instance.playerName; }
        set { Instance.playerName = value; }
    }



    [SerializeField]
    private Text playerDisplayName;
    public static Text PlayerDisplayName
    {
        get { return Instance.playerDisplayName; }
        set { Instance.playerDisplayName = value; }
    }




    private void Awake()
    {
        if (instance != this)
            instance = this;
        info = GameObject.FindObjectOfType<AccountInfo>();
       
    }

    private void Update()
    {
        if (info == null)
            return;
        
        UpdateText();
       
        
    }
  
     void UpdateText()
    {
        if(info.Info != null)
        {
            playerName.text = info.Info.AccountInfo.Username;
            playerDisplayName.text = info.Info.AccountInfo.TitleInfo.DisplayName;
            if (nameChanged==true)
            {
              
               
                playerDisplayName.text = info.Info.AccountInfo.TitleInfo.DisplayName;
            }
            else if (String.IsNullOrEmpty(info.Info.AccountInfo.TitleInfo.DisplayName))
           
            {
                playerDisplayName.text = info.Info.AccountInfo.Username;
            }

            if (update == true)
            {
                playerName.text = " ";
                playerDisplayName.text = " ";
            }
           



        }
       
        

    }

    #region Leaderboard
    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;
    public void GetLeaderboarder()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "SCORE", MaxResultsCount = 20 };
      
        
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeadboard, OnErrorLeaderboard);
        
    }

    void OnGetLeadboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);
       
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
           
             LL.playerPositionText.text = ((int)player.Position+1).ToString(); 
           
            if (String.IsNullOrEmpty(player.DisplayName)) { LL.playerNameText.text = info.Info.AccountInfo.Username; }
            else
            {
                LL.playerNameText.text = player.DisplayName;
            }
            LL.playerScoreText.text = player.StatValue.ToString();
            Debug.Log(player.Profile.TitleId + ": " + player.StatValue);
        }
  }
    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void CloseLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
        for (int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }
  

    #endregion Leaderboard
    private bool nameChanged=false;
    [SerializeField]
    private InputField newDisplayName;
    public void UpdatePlayerName()
    {
        string text = newDisplayName.text;
        if (text != "")
        {
            var req = new UpdateUserTitleDisplayNameRequest();
            req.DisplayName = text;
            PlayFabClientAPI.UpdateUserTitleDisplayName(req, OnPlayerNameResult, OnPlayerNameError);
            
        }
        
    }
    
    private void OnPlayerNameResult(UpdateUserTitleDisplayNameResult obj)
    {
        Debug.Log("Display name");
        //playerDisplayName.text = obj.DisplayName;
        nameChanged = true;
        info.Info.AccountInfo.TitleInfo.DisplayName = obj.DisplayName;
        NicknameError.text = " ";
    }
    [SerializeField]
    public Text NicknameError;
    private void OnPlayerNameError(PlayFabError error)
    {
        NicknameError.text = "Nickname already taken!";
        Debug.LogError(error);
    }
    public GameObject ChangeDNPanel;
    public void closeNickPlanel()
    {
        
        ChangeDNPanel.SetActive(false);

        


    }
    public void openNickPanel()
    {
        ChangeDNPanel.SetActive(true);

    }
    private bool update = false;
   public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("Credentiale uitate");
        SceneManager.LoadScene("Login");
        update = true;
    }
    

}
