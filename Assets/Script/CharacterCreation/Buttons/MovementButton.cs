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
        options = new string[]{"Turtle", "Runner", "Precise", "Fast"};
        movSpeed = new float[]{Constants.LOW_SPEED, Constants.MID_SPEED, Constants.LOW_SPEED, Constants.HIGH_SPEED};
        movAccuracy = new float[]{Constants.MID_MOV_PRECISION, Constants.MID_MOV_PRECISION, Constants.HIGH_MOV_PRECISION, Constants.LOW_MOV_PRECISION};
        speedValue = new int[]{0,10,20,30};
        text.text = options[actual];
        BotManager.movementSpeed = movSpeed[actual];
        BotManager.movementAccuracy = movAccuracy[actual];
        BotManager.movType = options[actual];
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        if(builder.legValue <= PartsBuilder.max_Value) {
            actual = (actual + 1) % options.Length;
            text.text = options[actual];
            builder.legValue = speedValue[actual];
            builder.change = true;
            BotManager.movementSpeed = movSpeed[actual];
            BotManager.movementAccuracy = movAccuracy[actual];
            BotManager.movType = options[actual];
        }
    }
    void prev()
    {
        if(builder.legValue > 0) {
            actual = (actual - 1 + options.Length) % options.Length;
            text.text = options[actual];
            builder.legValue = speedValue[actual];
            builder.change = true;
            BotManager.movementSpeed = movSpeed[actual];
            BotManager.movementAccuracy = movAccuracy[actual];
            BotManager.movType = options[actual];
        }
    }
}
