using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionButton : MonoBehaviour
{
     public Button buttonUp;
    public Button buttonDown;
    public GameObject bot;
    public Text text;
    private PartsBuilder builder;
    private string[] options;
    private float[] fov;
    private float[] distance;
    private int[] eyeValue;
    private int actual;
    // Start is called before the first frame update
    void Start()
    {
        BotData botD = Save.loadPlayerBotFile();
        if(botD != null)
            actual = botD.eyesV;
        else
            actual = 0;
            
        options = ButtonsValues.visLabels;
        eyeValue = ButtonsValues.visValues;
        text.text = options[actual];
        BotManager.visType = actual;
        
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        actual = (actual + 1) % options.Length;
        text.text = options[actual];
        builder.eyesValue = eyeValue[actual];
        BotManager.visType = actual;
        builder.change = true;
    }
    void prev()
    {
        actual = (actual - 1 + options.Length) % options.Length;
        text.text = options[actual];
        builder.eyesValue = eyeValue[actual];
        BotManager.visType = actual;
        builder.change = true;
    }
}
