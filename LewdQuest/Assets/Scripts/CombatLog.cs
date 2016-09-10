using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CombatLog : MonoBehaviour {

	// Use this for initialization
	Text text;
	void Start () {
		text = GetComponent<Text> ();
		text.text += " \n\n\n ";
	}
	
	// Update is called once per frame
	void Update () {

		//text.text += "/AAA " ;
	
	}


	public void logText(string newText){

		text.text += "\n" + newText;

	}

	public void logEnemyText( string newText){

		text.text += "\n<color=#ff0000ff>" + newText + "</color>";


	}

	public void logEnemyPain(string newText){


		text.text += "\n<color=#ff0000ff>" + newText + "</color>";
	}


	public void logEnemyDeath(string name, bool female){


		switch(Random.Range(0,3)){

		case 0:
			text.text += "\n \n" + name + " groans and slumps on the ground as she faces death.";
			break;
		case 1:
			text.text += "\n \n" + name + " moans loudly as her body suddenly gives up. Her eyes close as she stops breathing.";
			break;
		case 2:
			text.text += "\n \n" + name + " stumbles upon her feet and suffers a painful death.";
			break;
		case 3:
			text.text += "\n \n" + name + " gives a last cry of pain. Her hand struggles to move up, but it finally gives up and her eyes close.";
			break;

		}






	}

	public void logGreen(string name, bool female){
		

		if (!female) {
			text.text += "\n \n Woa, what the fuck are you doing you sick fuck? That has a penis. ";
			return;
		}

		switch(Random.Range(0,4)){

		case 0:
			text.text += "\n \n<color=#008000ff> You grope " + name + "'s chest, provoking a mimld blush on her face as she steps back swiftly.</color>";
			break;
		case 1:
			text.text += "\n \n<color=#008000ff> Moans escape from " + name + "'s lips as you grab her buttocks. </color>" ;
			break;
		case 2:
			text.text += "\n \n<color=#008000ff> You succesfully feel up " + name + " in the middle of the fight. </color>";
			break;
		case 3:
			text.text += "\n \n<color=#008000ff>" + name + "  jerks and tries to hide a moan as you feel her body. Your hands are swatted shortly afterwards. </color>";
			break;

		}

		//text.text += " ( " + name + "'s hornyness increased. ) ";


	}

	/*
	 * LOGS COMBAT LOGS FOR VERY HORNY CHARACTERS
	 * */
	public void logGreenNeedy(string name, bool female){


		if (!female) {
			text.text += "\n \n You grope "+ name + " who seems to enjoy it. " ;
			return;
		}

		switch(Random.Range(0,4)){

		case 0:
			text.text += "\n \n<color=#008000ff> You grope " + name + "'s chest, making her blush. </color>";
			break;
		case 1:
			text.text += "\n \n<color=#008000ff> "+ name + " no longer hides her moans as you grope her body. </color>" ;
			break;
		case 2:
			text.text += "\n \n<color=#008000ff> " + name + " gives you bed eyes as you feel her up. </color>";
			break;
		case 3:
			text.text += "\n \n<color=#008000ff>" + name + "  holds your hand as you touch her and doesn't let go. </color>";
			break;

		}

		//text.text += " ( " + name + "'s hornyness increased. ) ";


	}
}
