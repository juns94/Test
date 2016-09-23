﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {


	private GameObject partyPanel;
	private Scene scene;
	private Character character;
	private UIManagerScript manager;
	private GameObject enemyPanel;
	private CombatLog combatLog;
	private GameObject fuckButton;
	public  States state;
	public  string[] sceneParts;
	int currentScenePosition = 0;
	public void setEverything( GameObject enemyPanel ,GameObject partyPanel , Scene scene, Character character, UIManagerScript manager){
		this.partyPanel = partyPanel;
		this.scene 		= scene		;
		this.character 	= character	;
		this.manager 	= manager	;
		this.enemyPanel = enemyPanel;
		this.combatLog  = manager.combatLog;
		this.fuckButton = manager.fuckButton; 
	}


	public enum States {
		STARTED,
		WAIT
	}

	public void startManager() {

		Destroy(enemyPanel.GetComponentsInChildren<Transform>()[1].gameObject);
		GameObject fuckpanel = GameObject.Find("fuckPanel");
		fuckpanel.GetComponentsInChildren<Image>()[1].sprite =  Resources.Load <Sprite> (scene.image);
		Transform transform = fuckpanel.transform;
		transform.localPosition = new Vector3(transform.position.x,transform.position.y,27);
		fuckpanel.transform.parent = enemyPanel.transform;
		sceneParts 		= scene.sceneText.Split ('|');
		LewdUtilities.deleteAllButtons (partyPanel);
		advanceScene (0); // Comienza a ver la escena.







	
	}

	void Update () {

		switch (state) {

		case States.STARTED:
			break;

		case States.WAIT:

			break;
		}
	}

	public void advanceScene(int newPosition){
		LewdUtilities.deleteAllButtons (partyPanel);
		if (newPosition == sceneParts.Length) {
			makeExitButton ();
		} else {
			combatLog.clear ();
			combatLog.logText (sceneParts[newPosition]);
			makeNextButton ();
		}
	}

	public void makeNextButton(){
		GameObject next = Instantiate (fuckButton);
		next.transform.parent 		 = partyPanel.transform;
		next.transform.localScale 	 = Vector3.one;
		next.transform.localPosition = Vector3.zero;
		next.GetComponentInChildren<Text> ().text = "Next ->";
		next.GetComponentInChildren<Button> ().onClick.AddListener (() => {
			currentScenePosition += 1;
			advanceScene(currentScenePosition );
		});

	}


	public void makeExitButton(){
		GameObject mapManager = GameObject.Find ("MapManager");
		Destroy (mapManager);
		GameObject exit = Instantiate (fuckButton);
		exit.transform.parent 		 = partyPanel.transform;
		exit.transform.localScale 	 = Vector3.one;
		exit.transform.localPosition = Vector3.zero;
		exit.GetComponentInChildren<Text> ().text = "Exit";
		exit.GetComponentInChildren<Button> ().onClick.AddListener (() => {
			SceneManager.LoadScene("MapScene");
		});
	}
}
