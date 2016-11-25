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
	


		if (Input.GetKeyDown(KeyCode.G)) {
			PlayerPrefs.SetInt ("gold", 600);
		}
	}



	public int getEnemyNumber(){
		if (region == 2) {
			return 1;
		}
			
		switch (region) {
		case 1: ///FOREST
			return Random.Range (1, 4);

		}
		return 0;

	}


	public void rest(){

	//	PlayerPrefs.SetInt ("hp", 230);
		PlayerPrefs.SetInt ("hp",PlayerPrefs.GetInt("hpTotal",0	));
		PlayerPrefs.SetInt ("energy", 100);
		PlayerPrefs.Save ();
		LewdUtilities.getGameData (GameObject.Find ("GameData")).restSluts();

	}




	public void encounterPlains(){
		if (hasEnergyCheck ()) {
			region = 3;
			if (Random.Range (0, 5) < 3)
				SceneManager.LoadScene ("EncounterScene");
			else
				SceneManager.LoadScene ("BattleSceneTest");	
		}

	}


	public void encounterMountain(){
		if (hasEnergyCheck ()) {
			region = 2;
			if (Random.Range (0, 5) < 1)
				SceneManager.LoadScene ("EncounterScene");
			else
				SceneManager.LoadScene ("BattleSceneTest");	
		}

	}


	public void encounterForest(){

		if (hasEnergyCheck ()) {
			region = 1;
			if (Random.Range (0, 5) < 1)
				SceneManager.LoadScene ("EncounterScene");
			else
				SceneManager.LoadScene ("BattleSceneTest");	
		}
	}



	public void resetAllCharacters(){
		DataAccess.RESET ();
		for (int x = 0; x < 100; x++) {

			PlayerPrefs.SetInt (x + "", 0);
		}

		PlayerPrefs.Save ();


	}


	public void goToParty(){
		SceneManager.LoadScene ("PartyScene");
	}

	public void goToGrandCity(){
		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("GrandCityScene");
	}

	public void goToMap(){
		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("MapScene");

	}

	public void goToStats(){
		Destroy(GameObject.Find ("MapManager"));
		SceneManager.LoadScene ("StatsScene");

	}



	public bool hasEnergyCheck(){

		if (PlayerPrefs.GetInt ("energy", 100) > 0) {
			PlayerPrefs.SetInt ("energy", PlayerPrefs.GetInt ("energy", 100) - 10);
			PlayerPrefs.Save ();
			return true;

		}
			
		return false;
	}
}
