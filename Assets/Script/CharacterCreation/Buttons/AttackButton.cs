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

    // Start is called before the first frame update
    void Start()
    {
        buttonUp.onClick.AddListener(next);
        buttonDown.onClick.AddListener(prev);
        builder = bot.GetComponent<PartsBuilder>();
    }

    // Update is called once per frame
    void next()
    {
        if(builder.tailValue <= PartsBuilder.max_Value) {
            builder.tailValue += 1;
            builder.change = true;
        }
    }
    void prev()
    {
        if(builder.tailValue > 0) {
            builder.tailValue -= 1;
            builder.change = true;
        }
    }
}
