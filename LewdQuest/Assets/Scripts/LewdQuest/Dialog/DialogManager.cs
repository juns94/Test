using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour {


	public Text text;
	public 	GameObject box;
	Animator animator;
	string[] dialogue = new string[]{""};
	int currentPos = 0;
	bool firstTime  = true;

	public bool isActive;
	void Start () {

		animator = GetComponentInChildren<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {


	}


	public void showBox(string dialogue){


		box.SetActive (true);
		isActive = true;
		text.text = dialogue;

	}

	public void showBox(string[] newDialogue){

		dialogue = newDialogue;
		box.SetActive (true);
		text.text = dialogue[0];

	}

	public void showBoxShake(string[] newDialogue){

		dialogue = newDialogue;
		box.SetActive (true);
		text.text = dialogue[0];
		animator.Play ("dialogBoxShake");

	}
	public void showBoxAutoHide(string[] newDialogue){

		dialogue = newDialogue;
		box.SetActive (true);
		text.text = dialogue[0];
		Invoke ("hideBox",3f);

		if (firstTime) {
			animator.Play ("dialogBoxAnimatio");
		}
	}

	public void scrollText() 
	{
		if (dialogue.Length > 1) {

			if (currentPos++ == dialogue.Length) {
				hideBox ();
			}
			else 
				text.text = dialogue [currentPos++];



		} else {

			hideBox ();
		}


	}

	void hideBox(){
		animator.Play ("New State");
		currentPos = 0;
		box.SetActive (false);

	}

}
