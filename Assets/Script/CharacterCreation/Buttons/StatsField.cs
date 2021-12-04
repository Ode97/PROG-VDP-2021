using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsField : MonoBehaviour
{
    public Text text;

	void Start()
    {
		
	}

	void Update() {
		text.text = 
		"Bot Stats \n \n" +
		"Atack: \n" + 
		"  " + BotManager.atkType + "\n" +
		"Armor: \n" + 
		"  " + BotManager.armType + "\n" +
		"Movement: \n" + 
		"  " + BotManager.movType + "\n" +
		"Vision: \n" + 
		"  " + BotManager.visType + "\n";
	}

}