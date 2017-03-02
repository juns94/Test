using UnityEngine;
using System.Collections;

public class Chara_UI_Map  {

	private GameObject gameObject;
	private Character character;
	private DialogHub dialogHub;



	public Chara_UI_Map(GameObject x , Character y ){
		this.gameObject = x;
		this.character = y;
	}


	public Chara_UI_Map(GameObject x , Character y,DialogHub hub ){
		this.gameObject = x;
		this.character = y;
		this.dialogHub = hub;
	}


	public GameObject getGameObject() {
		return gameObject;
	}

	public Character getCharacter(){
		return character;
	}

	public DialogHub getHub(){
		return dialogHub;
	}

}
