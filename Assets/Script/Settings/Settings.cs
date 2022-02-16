using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button settingsButton;
    public Button creditsButton;
    public Button closeButtonSettings;
    public Button closeButtonCredits;
    public Canvas settingsScreen;
    public Canvas creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        settingsScreen.enabled = false;
        creditsScreen.enabled = false;
        settingsButton.onClick.AddListener(showSettings);
        creditsButton.onClick.AddListener(showCredits);
        closeButtonSettings.onClick.AddListener(hideSettings);
        closeButtonCredits.onClick.AddListener(hideCredits);
    }

    // Update is called once per frame
    void showSettings()
    {
        settingsScreen.enabled = true;
    }

    void showCredits()
    {
        creditsScreen.enabled = true;
    }

    
    void hideSettings()
    {
        settingsScreen.enabled = false;
        creditsScreen.enabled = false;
    }

    void hideCredits()
    {
        settingsScreen.enabled = false;
        creditsScreen.enabled = false;
    }
}
