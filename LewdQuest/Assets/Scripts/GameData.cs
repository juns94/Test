using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData {

	public GameData(){
		party = new List<Character> ();
	}

	private List<Character> party;


	public List<Character> getBitches(){
		List<Character> temp = new List<Character>();
		for (int x = 0; x < 100; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 1) {
				temp.Add(EnemyCreator.create(0,x));
			}
		}
		return temp;
	}

	public void addSlut(int id){
		party.Add(EnemyCreator.create(0,id));
	}





	public Character getCharacterById( int id){

		foreach (Character character in party) {
			if (character.id == id)
				return character;
		}
		return null;
	}

	/*
	// Update is called once per frame
	void Update () {
	
	}*/ 

	//public static DataControl control;
	/*
	void Awake() {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else {
			if(control != this)
			Destroy (gameObject);
		}

	}
	void Start () {
		party = new List<Character> ();
		addSlut (0);	
	}
*/
}
