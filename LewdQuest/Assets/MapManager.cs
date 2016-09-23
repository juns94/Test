using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MapManager : MonoBehaviour {


	public int region = 0;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public int getEnemyNumber(){
		if (region == 2) {
			return 1;
		}
			
		switch (region) {
		case 0:
			return Random.Range (1, 3);
		case 1:
			return Random.Range (1, 3);

		}
		return 0;

	}



	public void encounterMountain(){
		region = 2;
		SceneManager.LoadScene ("BattleSceneTest");

	}


	public void encounterForest(){
		region = 1;
		SceneManager.LoadScene ("BattleSceneTest");

	}



	public void resetAllCharacters(){


		for (int x = 0; x < 100; x++) {

			PlayerPrefs.SetInt (x + "", 0);
		}

		PlayerPrefs.Save ();


	}


	public void goToParty(){

		SceneManager.LoadScene ("PartyScene");
	}
}
