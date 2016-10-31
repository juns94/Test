using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Test : MonoBehaviour {
	/*
	private List<Character> party;

	
	// Update is called once per frame
	void Update () {
	
	}

	void Start () {
		party = new List<Character> ();
		addSlut (0);	
	}

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


	public void upload(){
		string path = EditorUtility.OpenFilePanel( "Overwrite with png", "", "" );
		FileStream file = File.Open(path,FileMode.OpenOrCreate);
		Debug.Log (file.Length);

		BinaryFormatter bf = new BinaryFormatter ();
		PartyData data = new PartyData ();
		data.party = party;
		bf.Serialize (file, data);
		file.Close ();
	}


	public void save (){


	}


	[Serializable]
	class PartyData{
		public List<Character> party;
		public PartyData(){

		}
	}*/
}
