using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Lewd utilities. MUDA MUDA MuDA MUDA MUADA MUDA
/// RODO RORA DA 
/// STANDO POWA
/// CHEKU MAITO DA
/// DARE NI WO MAKENAI
/// ICHIBAN GA SUKI DA, *NUMA ONE DA*
/// </summary>
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
			return DataAccess.Load ();
			//temp = GO.GetComponent<GameDataContainer> ().getData();
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
		if (party.Count > 1) {
			
			if (((Chara_UI_Map)party [1]).getCharacter ().getAlive ()) {
				return Random.Range (0, 2);
			} else
				return 0;
		}
	
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

	public static Color getTypeColor( Attack.SUBTYPE type){
		
		switch(type){
		case Attack.SUBTYPE.NORMAL:
			return Color.gray;
		case Attack.SUBTYPE.PAPER:
			return Color.red;
		case Attack.SUBTYPE.SCISSOR:
			return Color.yellow;
		case Attack.SUBTYPE.ROCK:
			return Color.green;

		}
		return Color.white;
	}


	public static Color getAttackTypeColor(Attack.SUBTYPE type){

		switch(type){
		case Attack.SUBTYPE.NORMAL:
			return Color.white;
		case Attack.SUBTYPE.PAPER:
			return Color.red;
		case Attack.SUBTYPE.SCISSOR:
			return Color.yellow;
		case Attack.SUBTYPE.ROCK:
			return Color.green;

		}
		return Color.white;
	}

	/// <summary>
	/// RETURNS A GAMEOBJECT WITH A BUTTON DESIGNED FOR DISPLAYING ATTACKS IN BATTLE
	/// </summary>
	/// <returns>The attack button.</returns>
	/// <param name="type">Type.</param>
	public static GameObject getAttackButton(Attack.SUBTYPE type){
		switch(type){
		case Attack.SUBTYPE.NORMAL:
			return Instantiate( (GameObject) Resources.Load("Prefabs/AttackTypeButtons/Grey"));
		case Attack.SUBTYPE.PAPER:
			return Instantiate( (GameObject) Resources.Load("Prefabs/AttackTypeButtons/Red"));
		case Attack.SUBTYPE.SCISSOR:
			return Instantiate( (GameObject) Resources.Load("Prefabs/AttackTypeButtons/Yellow"));
		case Attack.SUBTYPE.ROCK:
			return Instantiate( (GameObject) Resources.Load("Prefabs/AttackTypeButtons/Green"));

		}
		return Instantiate( (GameObject) Resources.Load("Prefabs/AttackTypeButtons/Grey"));
	}

	public static int getAttackButtonNumber(Attack.SUBTYPE type){
		switch(type){
		case Attack.SUBTYPE.NORMAL:
			return 3; //GREY
		case Attack.SUBTYPE.PAPER:
			return 2;  //RED 
		case Attack.SUBTYPE.SCISSOR:
			return 1; //YELLOW
		case Attack.SUBTYPE.ROCK:
			return 0; //GREEN

		}
		return 4; //GREY
	}

	/// <summary>
	/// RETURNS TRUE IF THE FIRST SUBTYPE IS WEAK TO THE SECOND ARGUMENT, USE WISELY LIKE I USED YOUR MOM LAST NIGHT.
	/// </summary>
	/// 
	/// <param name="target">Target.</param>
	/// <param name="attack">Attack.</param>
	public static bool isWeak( Attack.SUBTYPE target , Attack.SUBTYPE attack){


		if (target == Attack.SUBTYPE.PAPER && attack == Attack.SUBTYPE.SCISSOR)
			return true;

		if (target == Attack.SUBTYPE.ROCK && attack == Attack.SUBTYPE.PAPER)
			return true;

		if (target == Attack.SUBTYPE.SCISSOR && attack == Attack.SUBTYPE.ROCK)
			return true;

		return false;
		


	}





}
