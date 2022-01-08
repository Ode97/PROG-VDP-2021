using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabMan : MonoBehaviour
{
    [Header("UI")]
    public InputField userInput;
    public InputField passwordInput;
    //public InputField emailInput;
    public Text messageText;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;
    private string playerId;
    public static string username;

    // Start is called before the first frame update
    void Start()
    {
        passwordInput.contentType = InputField.ContentType.Password;
        registerButton.onClick.AddListener(RegisterButton);
        loginButton.onClick.AddListener(LoginButton);
        Debug.Log(Application.persistentDataPath);
        //Login();
        
    }

    /*void LoginButton(){
        var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }*/

    public void RegisterButton(){
        if(passwordInput.text.Length < 6){
            messageText.text = "Password too short\nit needs at least 6 characters or numbers";
            return;
        }else if(userInput.text.Length < 3){
            messageText.text = "Username too short,\nit needs at least 3 characters or numbers";
            return;
        }

        var request = new RegisterPlayFabUserRequest{
            Username = userInput.text,
            DisplayName = userInput.text,
            //Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }

    public void LoginButton(){
        var request = new LoginWithPlayFabRequest{
            Username = userInput.text,
            //Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true,
            }
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLogInSuccess, OnError);
    }

    void OnRegisterSucces(RegisterPlayFabUserResult result){
        messageText.text = "Registered and logged in!";
        username = result.Username;
        StartCoroutine(GoToMain());
    }

    private IEnumerator GoToMain(){
        yield return new WaitForSeconds(1f);
        SceneHandler.MainMenu();
    }

    void OnSucces(LoginResult result){
        messageText.text = "Succesful login/account create!";
        username = "a";
        StartCoroutine(GoToMain());
    }

    void OnLogInSuccess(LoginResult result){
        messageText.text = "Logged in!";
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            username = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        StartCoroutine(GoToMain());
    }

    void OnError(PlayFabError error){
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());

    }
    private void UpdateDisplayName(string displayname)
    {
        Debug.Log($"Updating Playfab account's Display name to: {displayname}");
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameSuccess, OnFailure);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"You have updated the displayname of the playfab account!");
   
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"There was an issue with your request: {error.GenerateErrorReport()}");
    }
}


