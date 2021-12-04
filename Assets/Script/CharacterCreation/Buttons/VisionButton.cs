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
        actual = 0;
        options = new string[]{"Long", "Balanced", "Wide"};
        fov = new float[]{Constants.MID_FOV, Constants.HIGH_FOV, Constants.LOW_FOV};
        distance = new float[]{Constants.MID_LOOKAHEAD, Constants.LOW_LOOKAHEAD, Constants.HIGH_LOOKAHEAD};
        eyeValue = new int[]{16,30,6};
        text.text = options[actual];
        BotManager.fov = fov[actual];
        BotManager.viewDistance = distance[actual];
        BotManager.visType = options[actual];
        BotManager.vis = actual;
        
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
        // save Value for game
    }

    // Update is called once per frame
    void next()
    {
        if(builder.eyesValue <= PartsBuilder.max_Value) {
            actual = (actual + 1) % options.Length;
            text.text = options[actual];
            builder.eyesValue = eyeValue[actual];
            builder.change = true;
            BotManager.vis = actual;
            BotManager.fov = fov[actual];
            BotManager.viewDistance = distance[actual];
            BotManager.visType = options[actual];
        }
    }
    void prev()
    {
        if(builder.eyesValue > 0) {
            actual = (actual - 1 + options.Length) % options.Length;
            text.text = options[actual];
            builder.eyesValue = eyeValue[actual];
            builder.change = true;
            BotManager.vis = actual;
            BotManager.fov = fov[actual];
            BotManager.viewDistance = distance[actual];
            BotManager.visType = options[actual];
        }
    }
}
