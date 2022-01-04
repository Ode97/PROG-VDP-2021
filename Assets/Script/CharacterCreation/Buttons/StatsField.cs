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
		"-> " + ButtonsValues.atkLabels[BotManager.atkType] + "\n" +
		"Armor: \n" + 
		"-> " + ButtonsValues.armLabels[BotManager.armType] + "\n" +
		"  (" + ButtonsValues.armDetail[BotManager.armType] + ")\n" +
		"Movement: \n" + 
		"-> " + ButtonsValues.movLabels[BotManager.movType] + "\n" +
		"Vision: \n" + 
		"-> " + ButtonsValues.visLabels[BotManager.visType] + "\n" +
		"Special: \n" + 
		"-> " + ButtonsValues.specLabels[BotManager.specType];
	}

}