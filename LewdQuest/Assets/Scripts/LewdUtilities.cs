using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LewdUtilities : MonoBehaviour  {


	public static int aliveCount(UIManagerScript manager){
		ArrayList enemyMap = manager.getEnemyMap ();
		int counter = 0;
		for (int x = 0; x < enemyMap.Count; x++) {
			if(((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive()) counter++;

		}
		return counter;
	}


	public static int getLastAlivePosition(UIManagerScript manager){
		ArrayList enemyMap = manager.getEnemyMap ();
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getCharacter().getAlive ())
				return x;

		}
		return 	0;

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
		for (int x = 0; x < buttons.Length; x++) {
			Destroy (buttons [x].gameObject);
		}


	}
}
