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
        options = new string[]{"Fire", "Electric"};
        atkSpeed = new float[]{Constants.LOW_DMG, Constants.HIGH_DMG};
        atkDamage = new float[]{Constants.HIGH_RATE, Constants.LOW_RATE};
        atkType = new float[]{Constants.HIGH_RATE, Constants.LOW_RATE};
        atkValue = new int[]{16,30,6};
        text.text = options[actual];
        BotManager.attackSpeed = atkSpeed[actual];
        BotManager.attackDamage = atkSpeed[actual];
        BotManager.attackType = atkType[actual];
        BotManager.atkType = options[actual];
        BotManager.atk = actual;
        
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        if(builder.tailValue <= PartsBuilder.max_Value) {
            actual = (actual + 1) % options.Length;
            text.text = options[actual];
            builder.tailValue = atkValue[actual];
            builder.change = true;
            BotManager.atk = actual;
            BotManager.attackSpeed = atkSpeed[actual];
            BotManager.attackDamage = atkSpeed[actual];
            BotManager.attackType = atkType[actual];
            BotManager.atkType = options[actual];
        }
    }
    void prev()
    {
        if(builder.tailValue > 0) {
            actual = (actual - 1 + options.Length) % options.Length;
            text.text = options[actual];
            builder.tailValue = atkValue[actual];
            builder.change = true;
            BotManager.atk = actual;
            BotManager.attackSpeed = atkSpeed[actual];
            BotManager.attackDamage = atkSpeed[actual];
            BotManager.attackType = atkType[actual];
            BotManager.atkType = options[actual];
        }
    }
}
