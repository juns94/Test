using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class LewdUtilities : MonoBehaviour  {
	static int lastAlivePos = 0;

	public static int aliveCount(UIManagerScript manager){
		ArrayList enemyMap = manager.getEnemyMap ();
		int counter = 0;
		for (int x = 0; x < enemyMap.Count; x++) {
			if(((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive()) counter++;

		}
		return counter;
	}

	public static ArrayList getPartyBitches(){
		ArrayList temp = new ArrayList ();
		for (int x = 0; x < 201; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 2) {
				temp.Add(x);
			}
		}
		return temp;
	}

	public static bool AliveEnemiesRemain(UIManagerScript manager) {
		ArrayList enemyMap = manager.getEnemyMap ();
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive ()) {
				return true;
			}
		}
		return 	false;


	}


	public static GameData getGameData( GameObject GO){
		GameData temp;
		if (GO != null) {
			temp = GO.GetComponent<GameDataContainer> ().getData ();
			if (temp == null) {
				return DataAccess.Load ();
			}
		} else {
			return getGameData ();
		}
		return temp;
	}

	public static GameData getGameData(){
		return DataAccess.Load();
	}

	public static int getPartyAlivePos(ArrayList party){
		///TODO MAKES THIS LESS SHITTY
		/*for (int x = 0; x < party.Count; x++) {
			if (((Chara_UI_Map)party [x]).getCharacter ().getAlive ()) {
				
			}
		}
		return 	lastAlivePos;*/
		if(party.Count > 1 )
		if (((Chara_UI_Map)party [1]).getCharacter ().getAlive ()) {
			return Random.Range (0, 2);
		} else
			return 0;
	
		return 0;
	}

	public static int getLastAlivePosition(UIManagerScript manager){
		ArrayList enemyMap = manager.getEnemyMap ();
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive ()) {
				lastAlivePos = x;	
				return x;
			}
		}
		return 	lastAlivePos;

	}


	public static int getLastVisiblePosition( UIManagerScript manager){
		ArrayList enemyMap = manager.getEnemyMap ();
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getGameObject() != null)
				return x;

		}
		return 	0;
	}



	public static void deleteAllButtons (GameObject gameObject)
	{

		Button[] buttons = gameObject.GetComponentsInChildren<Button> ();
		if(buttons != null)
		for (int x = 0; x < buttons.Length; x++) {
			Destroy (buttons [x].gameObject);
		}


	}
}
