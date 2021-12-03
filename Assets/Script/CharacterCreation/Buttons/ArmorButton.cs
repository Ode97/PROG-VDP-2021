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
        if(builder.bodyValue <= PartsBuilder.max_Value) {
            builder.bodyValue += 1;
            builder.change = true;
        }
    }
    void prev()
    {
        if(builder.bodyValue > 0) {
            builder.bodyValue -= 1;
            builder.change = true;
        }
    }
}
