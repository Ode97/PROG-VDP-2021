using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorButton : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;
    public GameObject bot;
    private PartsBuilder builder;
    public Text text;
    private string[] options;
    private float[] thickness;
    private int[] bodyValue;
    private int actual;

    // Start is called before the first frame update
    void Start()
    {
        actual = 0;
        options = ButtonsValues.armLabels;
        bodyValue = ButtonsValues.armValues;
        text.text = options[actual];
        BotManager.armType = actual;

        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        actual = (actual + 1) % options.Length;
        text.text = options[actual];
        builder.change = true;
        builder.bodyValue = bodyValue[actual];
        BotManager.armType = actual;
    }
    void prev()
    {
        actual = (actual - 1 + options.Length) % options.Length;
        text.text = options[actual];
        builder.change = true;
        builder.bodyValue = bodyValue[actual];
        BotManager.armType = actual;
    }
}
