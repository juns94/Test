using UnityEngine;
using System.Collections;

public class GameDataContainer : MonoBehaviour {



	public static GameData data = null;

	void Start () {
		DontDestroyOnLoad (this);

		data = DataAccess.Load ();
		if (data == null) {
			data = new GameData ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameData getData(){

		return data;
	}
}
