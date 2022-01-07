using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialButton : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;
    public GameObject bot;
    private PartsBuilder builder;
    public Text text;
    private string[] options;
    private int[] specValue;
    private int actual;

    // Start is called before the first frame update
    void Start()
    {
        
        actual = 0;
        options = ButtonsValues.specLabels;
        specValue = ButtonsValues.specValues;
        text.text = options[actual];
        BotManager.specType = actual;

        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        actual = (actual + 1) % options.Length;
        builder.specialValue = specValue[actual];
        text.text = options[actual];
        BotManager.specType = actual;
        builder.change = true;
    }
    void prev()
    {
        actual = (actual - 1 + options.Length) % options.Length;
        builder.specialValue = specValue[actual];
        text.text = options[actual];
        BotManager.specType = actual;
        builder.change = true;
    }
}
