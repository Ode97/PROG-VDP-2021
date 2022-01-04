using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Button buttonUp;
    public Button buttonDown;
    public GameObject bot;
    private PartsBuilder builder;
    public Text text;
    private string[] options;
    private float[] atkSpeed;
    private float[] atkDamage;
    private float[] atkType;
    private int[] atkValue;
    private int actual;

    // Start is called before the first frame update
    void Start()
    {
        actual = 0;
        options = ButtonsValues.atkLabels;
        atkValue = ButtonsValues.atkValues;
        text.text = options[actual];
        BotManager.atkType = actual;

        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        actual = (actual + 1) % options.Length;
        text.text = options[actual];
        BotManager.atkType = actual;
        builder.tailValue = atkValue[actual];
        builder.change = true;
    }
    void prev()
    {
        actual = (actual - 1 + options.Length) % options.Length;
        text.text = options[actual];
        BotManager.atkType = actual;
        builder.tailValue = atkValue[actual];
        builder.change = true;
    }
}
