using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
#pragma warning disable 0649
public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private InputField loginUsername;

    [SerializeField]
    private InputField loginPassword;

    [SerializeField]
    private InputField registerUsername;

    [SerializeField]
    private InputField registerEmail;

    [SerializeField]
    private InputField registerPassword;

    [SerializeField]
    private InputField registerConfirmPassword;
    [SerializeField]
    private GameObject passwordResetPanel;
    [SerializeField]
    private InputField requestedEmail;
    [SerializeField]
    private Text emailText;

    public void Login()
    {
        AccountInfo.Login(loginUsername.text, loginPassword.text);
        
    }

    public void Register()
    {
        if (registerConfirmPassword.text == registerPassword.text)
            AccountInfo.Register(registerUsername.text, registerEmail.text, registerPassword.text);
        else
            Debug.LogError("Passwords do not match!");
       
    }

    public void ChangeMenu(int i)
    {
        GameFunctions.ChangeMenu(menus.ToArray(), i);
    }

    public void changePasswordResetPanel()
    {
        if (passwordResetPanel.activeSelf)
        {
            passwordResetPanel.SetActive(false);
        }
        else
            passwordResetPanel.SetActive(true);
    }

    public void RequestPassword()
    {
        string text = requestedEmail.text;
        if (text != "")
        {
            var request = new SendAccountRecoveryEmailRequest();
            request.Email = text;
            request.TitleId= "A1B53";
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoveryEmailSuccess, OnRecoveryEmailError);
        }
    }

    private void OnRecoveryEmailError(PlayFabError error)
    {
        Debug.Log(error);
        showMailError();
    }

    private void OnRecoveryEmailSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Email corect! Se solicita date!");
        showMailSuccess();
    }

    public void showMailError()
    {
        emailText.text = "Wrong email adress!";
    }

    public void showMailSuccess()
    {
        emailText.text = "Recovery email sent!";
    }

    
}
