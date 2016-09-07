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

		text.text += "\n \n" + newText;

	}

	public void logEnemyText( string newText){

		text.text += "\n \n<color=#ff0000ff>" + newText + "</color>";


	}

	public void logEnemyPain(string newText){


		text.text += "\n \n<color=#ff0000ff>" + newText + "</color>";
	}
}
