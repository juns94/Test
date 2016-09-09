using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour {

	public Sprite[] images;
	public GameObject itemPrefab;
	public CombatLog combatLog;
	public EnemyCreator enemyCreator;
	ArrayList partyMap;
	ArrayList enemyMap;
	GameObject enemyPanel;
	public int enemyNumber = 1;
	int partyNumber = 1;
	bool waiting;

	public enum States {
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		WIN,
		LOSE,
		WAIT,
		WAITPLAYER
	}

	States state;
	void Start () {
		enemyCreator = new EnemyCreator ();
		enemyMap = new ArrayList ();
		partyMap = new ArrayList ();
		state = States.PLAYERCHOICE;
		enemyPanel = GameObject.Find ("EnemyPanel");
		for (int x = 0; x < enemyNumber; x++) {
			GameObject newItem = Instantiate (itemPrefab) as GameObject;
			newItem.name = gameObject.name + " item at (" + x + ")";
			newItem.transform.parent = enemyPanel.gameObject.transform;
			newItem.transform.localScale = Vector3.one;
			newItem.transform.localPosition = Vector3.zero;
			Character character = prepareEnemy (0);
			newItem.GetComponentsInChildren<Text> () [2].text = character.getName ();
			newItem.GetComponentsInChildren<Image> ()[3].overrideSprite = images [Random.Range(0,2)];
			enemyMap.Add(new Chara_UI_Map(newItem, character ));
		//	GameObject hpBar = GameObject.Find("hpCount").GetComponentInChildren<Text>();

		
		}


		for (int x = 0; x < partyNumber; x++) {
			GameObject item = GameObject.Find ("hpCombo");
			Character character = new Character (0, "you", 30, 1, 1, 1, "", false);
			partyMap.Add(new Chara_UI_Map(item, character ));
		}
			

	}


	public Character prepareEnemy(int id){

		return EnemyCreator.create(id);
	}
	
	// Update is called once per frame
	void Update () {


		switch (state) {

		case States.PLAYERCHOICE:
			break;

		case States.ENEMYCHOICE:
			actEnemyTurn ();
			break;

		case States.LOSE:
			break;

		case States.WAIT:
			if (!waiting) {
				state = States.PLAYERCHOICE;
				waiting = true;
			}
			break;

		case States.WAITPLAYER:
			if (!waiting) {
				state = States.ENEMYCHOICE;
				waiting = true;
			}
			break;



		}


	
		updateEnemies ();
		updateParty   ();
	
	}



	void updateEnemies(){

		for (int x = 0; x < enemyMap.Count; x++) {

			Image[] images = null;
			GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
			Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();
			//currentUI.GetComponentsInChildren<Text> () [2].text = currentChara.getHp () + "/" + currentChara.getTotalHp ();
			if (currentUI != null) {
				images = currentUI.GetComponentsInChildren<Image> ();
				currentUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)currentChara.getHp (), (float)currentChara.getTotalHp ());
			}
			//images [2].transform.localScale = new Vector3((float)currentChara.getHp () / (float)currentChara.getTotalHp (),1,1);
				//new Vector3(0.5f, 1, 1);
			if (!currentChara.getAlive()) {

				if (!currentChara.gone) {
					if(images != null)
					images [3].CrossFadeAlpha (0.0f, 0.3f, false);
					currentUI.GetComponent<Animator> ().Play ("Dead");
					combatLog.logEnemyDeath (currentChara.getName () , currentChara.female);
					currentChara.gone = true;
					StartCoroutine ("deleteEnemyUI", currentUI);
				}

			}

		}
	}

	void updateParty(){

		for (int x = 0; x < partyMap.Count; x++) {

			Image[] images = null;
			GameObject currentUI = ((Chara_UI_Map)partyMap [x]).getGameObject ();
			Character currentChara = ((Chara_UI_Map)partyMap [x]).getCharacter ();
			//currentUI.GetComponentsInChildren<Text> () [2].text = currentChara.getHp () + "/" + currentChara.getTotalHp ();
			if (currentUI != null) {
				images = currentUI.GetComponentsInChildren<Image> ();
				currentUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)currentChara.getHp (), (float)currentChara.getTotalHp ());

			}
			//images [1].transform.localScale = new Vector3((float)currentChara.getHp () / (float)currentChara.getTotalHp (),1,1);
			//currentUI.GetComponentInChildren<Text>().gameObject.transform.parent = null;
			currentUI.GetComponentInChildren<Text>().text = currentChara.getHp() + "/" + currentChara.getTotalHp();

			//new Vector3(0.5f, 1, 1);
		/*	if (!currentChara.getAlive()) {

				if (!currentChara.gone) {
					if(images != null)
						images [3].CrossFadeAlpha (0.0f, 0.3f, false);
					currentUI.GetComponent<Animator> ().Play ("Dead");
					combatLog.logEnemyDeath (currentChara.getName () , currentChara.female);
					currentChara.gone = true;
					StartCoroutine ("deleteEnemyUI", currentUI);
				}

			}
*/
		}
	}
	public void dealDamage(int position){
		if(state == States.PLAYERCHOICE){
			StartCoroutine("playerCouroutine", position);
		}

	}

	public void dealDamageParty(int position){


		GameObject currentUI = ((Chara_UI_Map)partyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)partyMap [position]).getCharacter ();
		if (character.getAlive ()) {

			character.receiveDamage (5);
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
		//	combatLog.logText ("You smack the bitch with a dried cucumber angrily.");
		}

	}




	public void actEnemyTurn(){
		waiting = true;
		StartCoroutine("enemyCouroutine");
		changeState (States.WAIT);
	}

	IEnumerator enemyCouroutine() {
		for (int x = 0 ; x < enemyMap.Count; x++) {
			actEnemy (x);
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().alive)
				yield return new WaitForSeconds (.9f);
			else
				yield return new WaitForSeconds (.01f);
		}
		waiting = false;
		Debug.Log ("DONE");
			
	}

	IEnumerator playerCouroutine(int position) {

		if (enemyMap.Count < 1) {
			//Button buttonPrefab = UnityEngine.Resources.Load<Button>("UI/Button");
//Instantiate (buttonPrefab);
		}

		Attack attack;
		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			
			character.receiveDamage (5);
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			combatLog.logText ("You smack the bitch with a dried cucumber angrily.");
		}
		yield return new WaitForSeconds(.5f);
		state = States.ENEMYCHOICE;

		Debug.Log ("DONE");

	}


	public void actEnemy( int x ) {
		GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
		Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

		if (currentChara.getAlive ()) {
			// busca un atack aqui de la lista del
			currentUI.GetComponent<Animator> ().Play ("Attack");
			combatLog.logEnemyText (currentChara.getName () + " attacks with the force of a thousand suns.");
			dealDamageParty (0);
		}
	}


	IEnumerator deleteEnemyUI(GameObject enemyUI) {
		
		yield return new WaitForSeconds(.5f);
		Destroy (enemyUI);

	}





	public void changeState(States state){
		this.state = state;
	}



	public ArrayList getEnemyMap(){
		return enemyMap;
	}

	public void attemptRun(){

		combatLog.logText (" You attempt to run!! but fail inevitable because this feauture is not yet implemented by St0rm. ");
		state = States.ENEMYCHOICE;
	}

	public void attemptGrope(int pos){
		Character character = ((Chara_UI_Map)enemyMap[pos]).getCharacter ();
		if (character.getAlive () & Random.Range (0, 5) == 4) {
			//  
			character.makeHorny (10);
			combatLog.logGreen (character.getName (), character.female);
		} else
			combatLog.logText (" You fail to grope " + character.getName () + ".");


		state = States.ENEMYCHOICE;
	}



}
