using UnityEngine;
using System.Collections;

public class Chara_UI_Map  {

	private GameObject gameObject;
	private Character character;



	public Chara_UI_Map(GameObject x , Character y ){
		this.gameObject = x;
		this.character = y;
	}



	public GameObject getGameObject() {
		return gameObject;
	}

	public Character getCharacter(){
		return character;
	}


}
