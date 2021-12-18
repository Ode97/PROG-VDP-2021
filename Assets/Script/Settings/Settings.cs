using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button settingsButton;
    public Button closeButton;
    public Canvas settingsScreen;
    // Start is called before the first frame update
    void Start()
    {
        settingsScreen.enabled = false;
        settingsButton.onClick.AddListener(show);
        closeButton.onClick.AddListener(hide);
    }

    // Update is called once per frame
    void show()
    {
        settingsScreen.enabled = true;
    }

    
    void hide()
    {
        settingsScreen.enabled = false;
    }
}
