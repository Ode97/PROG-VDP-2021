using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;
    public GameObject bot;
    private PartsBuilder builder;
    public Text text;
    private string[] options;
    private float[] movSpeed;
    private float[] movAccuracy;
    private int[] speedValue;
    private int actual;

    // Start is called before the first frame update
    void Start()
    {
        actual = 0;
        options = ButtonsValues.movLabels;
        speedValue = ButtonsValues.movValues;
        text.text = options[actual];
        BotManager.movType = actual;
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        Debug.Log(actual + " - " + speedValue[actual]);
        actual = (actual + 1) % options.Length;
        text.text = options[actual];
        builder.legValue = speedValue[actual];
        BotManager.movType = actual;
        builder.change = true;
    }
    void prev()
    {
        actual = (actual - 1 + options.Length) % options.Length;
        text.text = options[actual];
        builder.legValue = speedValue[actual];
        BotManager.movType = actual;
        builder.change = true;
    }
}
