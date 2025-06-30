using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountInfo : MonoBehaviour
{
    private static AccountInfo instance;

    [SerializeField]
    private GetPlayerCombinedInfoResultPayload info;

    public GetPlayerCombinedInfoResultPayload Info
    {
        get { return info; }
        set { info = value; }
    }


    public static AccountInfo Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [SerializeField]
    private Text regErrorText;
    public static Text RegErrorText
    {
        get { return Instance.regErrorText; }
        set { Instance.regErrorText = value; }
    }

    [SerializeField]
    private Text logErrorText;
    public static Text LogErrorText
    {
        get { return Instance.logErrorText; }
        set { Instance.logErrorText = value; }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
     
    }

    public static void Register(string username, string email, string password)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Email = email,
            Username = username,
            Password = password
          
        };
       
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegister, OnRegisterError);
        

    }
    static void OnRegister(RegisterPlayFabUserResult result)
    {

        Debug.Log("Registered with: " + result.PlayFabId);
        Instance.GetStats();
        Instance.showRegSuccess();

    }
    static void OnRegisterError(PlayFabError error)
    {
        Debug.LogError(error);
        Instance.showRegError();


    }
    public void showRegError()
    {
        regErrorText.text = "Wrong redentials or already in use!";
    }
    public void showLogError()
    {
        logErrorText.text = "Wrong username or password!";
    }

    public void showRegSuccess()
    {
       regErrorText.text = "Succesfully registered!";
    }

 
    public static void Login(string username, string password)
    {
        
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = password
        };
       
        PlayFabClientAPI.LoginWithPlayFab(request, OnLogin, OnLoginError);
        
    }
    static void OnLogin(LoginResult result)
    {
        Debug.Log("Login with: " + result.PlayFabId);
        GetAccountInfo(result.PlayFabId);

        Instance.GetStats();
        SceneManager.LoadScene("Menu");
        

    }

    static void OnLoginError(PlayFabError error)
    {
        Debug.LogError(error);
        Instance.showLogError();
    }

    public static void GetAccountInfo(string playfabid)
    {
        GetPlayerCombinedInfoRequestParams paramInfo = new GetPlayerCombinedInfoRequestParams()
        {
            GetTitleData = true,
            GetUserAccountInfo = true,
            GetPlayerProfile = true,
            GetPlayerStatistics = true,
            GetUserData = true,
            GetUserReadOnlyData = true
            
    };
        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest()
        {
            PlayFabId = playfabid,
            InfoRequestParameters = paramInfo
        };

        PlayFabClientAPI.GetPlayerCombinedInfo(request, OnGotAccountInfo, OnGotAccountInfoError);
    }
    public static void OnGotAccountInfoError(PlayFabError error)
    {
        Debug.LogError(error);
    }

    static void OnGotAccountInfo(GetPlayerCombinedInfoResult result)
    {
        Debug.Log("Updated Account Info!");
        Instance.Info = result.InfoResultPayload;
    }





    public  void SetStats(string stat, int playerScore)
    {

        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            
            Statistics = new List<StatisticUpdate> {
         new StatisticUpdate { StatisticName = stat, Value =  playerScore
    },
     }
        },
 result => { Debug.Log("User statistics updated"); },
 error => { Debug.LogError(error.GenerateErrorReport()); });

    }


    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    public void OnGetStats(GetPlayerStatisticsResult result)
    {

        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "SCORE":
                   int playerScore = eachStat.Value;
                    break;
            }
        }
    }





  



   
}
