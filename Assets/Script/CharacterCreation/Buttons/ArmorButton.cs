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
        options = new string[]{"Medium"," Light", "Armored"};
        thickness = new float[]{Constants.MID_PROTECTION, Constants.LOW_PROTECTION, Constants.HIGH_PROTECTION};
        bodyValue = new int[]{16,30,6};
        text.text = options[actual];
        builder.bodyValue = bodyValue[actual];
        BotManager.bodyThickness = thickness[actual];
        BotManager.armType = options[actual];

        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        if(builder.bodyValue <= PartsBuilder.max_Value) {
            actual = (actual + 1) % options.Length;
            text.text = options[actual];
            builder.change = true;
            builder.bodyValue = bodyValue[actual];
            BotManager.bodyThickness = thickness[actual];
            BotManager.armType = options[actual];
        }
    }
    void prev()
    {
        if(builder.bodyValue > 0) {
            actual = (actual - 1 + options.Length) % options.Length;
            text.text = options[actual];
            builder.change = true;
            builder.bodyValue = bodyValue[actual];
            BotManager.bodyThickness = thickness[actual];
            BotManager.armType = options[actual];
        }
    }
}
