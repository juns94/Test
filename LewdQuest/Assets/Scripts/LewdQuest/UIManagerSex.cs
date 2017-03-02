using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIManagerSex : MonoBehaviour {
	public GameObject 	fuckButton;
	public GameObject 	itemPrefab;
	public CombatLog 	combatLog;
	public EnemyCreator enemyCreator;
	GameData 			gameData;
	ArrayList 			partyMap;
	ArrayList 			enemyMap;
	public int 			enemyNumber = 3;
	int 		lastAlivePosition;
	bool 		waiting;
	bool 		fightFinished 	= false;
	bool 		hasPartyMembers = false;
	GameObject 	enemyPanel;
	GameObject 	mapManager;
	GameObject 	itemPanel;
	GameObject	rigged;
	public ItemManager itemManager;
	public GameObject partyInfo;
	public GameObject buddyUI;
	public GameObject buddyButtons;
	public GameObject partyButtons;
	public GameObject buddyAttackBtn;
	public SexSceneContainer container;


	public bool safe = true;
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

		gameData = LewdUtilities.getGameData(GameObject.Find("GameData"));
		int region = 1;
		mapManager = GameObject.Find("MapManager");
		if (mapManager != null) {
			region = mapManager.GetComponent<MapManager> ().region;
			enemyNumber = mapManager.GetComponent<MapManager> ().getEnemyNumber ();
		}

		enemyMap 	= new ArrayList ();
		partyMap 	= new ArrayList ();
		state 		= States.PLAYERCHOICE;
		enemyPanel 	= GameObject.Find ("EnemyPanel");
		itemPanel 	= GameObject.Find ("ItemPanel");
	

		////// PARTYMAP THING /////////

		GameObject item = GameObject.Find 	("CharacterCombo");
		Character  hero = HeroUtils.getHero (		  );
		hero.hp 		= PlayerPrefs.GetInt( "hp", 0 );
		partyMap.Add(new Chara_UI_Map(item, hero ));

		ArrayList temp = LewdUtilities.getPartyBitches ();
		if (temp.Count != 0) {
			for (int x = 0; x < temp.Count; x++) {
				Character buddy = gameData.getCharacterById ((int)temp [0]);
				//buddyUI.GetComponentsInChildren<Text>
				partyMap.Add (new Chara_UI_Map (buddyUI, buddy));
				buddyUI.GetComponentsInChildren<Text> () [1].text = buddy.name;
				Debug.Log ("entro y el count es " + temp.Count);
			}
			hasPartyMembers = true;
		}
		else {
			buddyUI.SetActive (false);
		}


		Invoke("openFuckPanel",0.1f);

	}

	private Character reRollCharacter(Character character, int region){
		if (character.female) {
			int counter = 0;
			while (isAlreadyInBattle (character) ) {
				character = EnemyCreator.create (region,-1);
				counter++;
				if (counter == 100)
					break;
			}

			Debug.Log (counter);

		} else {
			return character;
		}
		return character;
	}
	private bool isAlreadyInBattle(Character character){
		for (int x = 0; x < enemyMap.Count; x++) {
			if (character.id == ((Chara_UI_Map)enemyMap [x]).getCharacter ().id) {

				return true;
			}
		}
		return false;
	}


	void Update () {
		//	Debug.Log (state);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			DataAccess.Save(gameData);
			Destroy (GameObject.Find ("MapManager"));
			SceneManager.LoadScene ("MapScene");
		}
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
			
	}

	void updateParty(){

		for (int x = 0; x < partyMap.Count; x++) {

			Image[] images = null;
			GameObject currentUI = ((Chara_UI_Map)partyMap [x]).getGameObject ();
			Character currentChara = ((Chara_UI_Map)partyMap [x]).getCharacter ();
			if (!currentChara.alive & x == 0)
				StartCoroutine ("Lose");

			if (currentUI != null) {
				images = currentUI.GetComponentsInChildren<Image> ();
				currentUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)currentChara.getHp (), (float)currentChara.getTotalHp ());

			}

			currentUI.GetComponentInChildren<Text>().text = currentChara.getHp() + "/" + currentChara.getTotalHp();

		}
	}



	public void useItem(int position , Item item, bool isPlayer){
		if(state == States.PLAYERCHOICE){
			state = States.WAITPLAYER;
			waiting = true;
			GameObject currentUI;
			Character character;


			if (!isPlayer) {
				currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
				character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
			} else {
				currentUI = ((Chara_UI_Map)partyMap [position]).getGameObject ();
				character = ((Chara_UI_Map)partyMap [position]).getCharacter ();
			}


			if (character.getAlive () || isPlayer) {
				ItemCreator.useItem (character, item.id);
				//character.receiveDamage (Random.Range(4,7));
				//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
				//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
				combatLog.logGreen("You use the <size=20><b>"+ item.name + "</b></size> on " + character.name +".");
			}

			Invoke ("setEnemyTurn", 0.5f);
			DataAccess.Save(gameData);
		}

	}

	public void useHealBuddy(Attack attack, int position , bool isPlayer){
		if(state == States.PLAYERCHOICE){
			state = States.WAITPLAYER;
			waiting = true;
		}

		GameObject currentUI;
		Character character;
		if (!isPlayer) {
			currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
			character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		} else {
			currentUI = ((Chara_UI_Map)partyMap [position]).getGameObject ();
			character = ((Chara_UI_Map)partyMap [position]).getCharacter ();
		}
		Character buddy = ((Chara_UI_Map)partyMap [1]).getCharacter ();
		if (character.getAlive ()) {

			character.heal ((int)(attack.getDamage () * buddy.getMagicPower()));

		}

		combatLog.logGreen (buddy.name +" heals " + character.name +" for "+((int)(attack.getDamage () * buddy.getMagicPower())) +" hitpoints!");

		Invoke("setEnemyTurn", 0.5f);


	}

	public void dealDamageBuddy(Attack  attack , int position){
		if(state == States.PLAYERCHOICE){
			state = States.WAITPLAYER;
			waiting = true;
		}
		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character enemy = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (enemy.getAlive ()) {
			Character buddy = ((Chara_UI_Map)partyMap [1]).getCharacter ();

			int attackPower= buddy.attack;
			if (attack.type == Attack.TYPE.MAGIC)
				attackPower = buddy.magicPower;

			enemy.receiveDamage (Random.Range(attackPower ,attackPower + 3));
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
			combatLog.logText (buddy.name +" attacks " + ((Chara_UI_Map)enemyMap [position]).getCharacter().name +" with "+attack.getName()+".");
		}
		Invoke("setEnemyTurn", 0.5f);
	}



	public void dealDamage(int position){
		if(state == States.PLAYERCHOICE){
			state = States.WAITPLAYER;
			waiting = true;
			StartCoroutine("playerCouroutine", position);
		}

	}

	public void openFuckPanel(){
		///		   TODO   WARNING, THE CHARACTER IS NULL MOST LIKELY
		/// 	///TODO   WARNING, THE CHARACTER IS NULL MOST LIKELY
		/// 	///TODO   WARNING, THE CHARACTER IS NULL MOST LIKELY
		/// 	///TODO   WARNING, THE CHARACTER IS NULL MOST LIKELY
		Character character = gameData.getCharacterById(0);
		GameObject panel = partyInfo;
		LewdUtilities.deleteAllButtons(panel);
		Scene scene;
		Debug.Log (container);
		scene = container.getScene ();
		if (scene != null) {
			
					///TODO   WARNING, THE CHARACTER IS NULL MOST LIKELY
					this.gameObject.AddComponent<SceneManagerScript>();
					SceneManagerScript sceneManager = this.gameObject.GetComponent<SceneManagerScript>();
					sceneManager.setEverything(enemyPanel, panel,scene,character,this);
					sceneManager.startSexManager();
				
		}


	}


	public void dealDamageParty(int position, int damage){
		
	}

	public void actEnemyTurn(){
		waiting = true;
		StartCoroutine("enemyCouroutine");
		changeState (States.WAIT);
	}

	public void setEnemyTurn(){
		state = States.ENEMYCHOICE;
		waiting = false;
	}

	IEnumerator enemyCouroutine() {
		for (int x = 0 ; x < enemyMap.Count; x++) {
			actEnemy (x);
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().alive)
				yield return new WaitForSeconds (.5f);
			else
				yield return new WaitForSeconds (.01f);
		}
		waiting = false;
	}



	IEnumerator itemCouroutine(int position, int itemId, bool playerItem) {

		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character  character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			ItemCreator.useItem (character, itemId);
			//character.receiveDamage (Random.Range(4,7));
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
			combatLog.logText ("You use the "+ " on " + ((Chara_UI_Map)enemyMap [position]).getCharacter().name +".");
		}
		yield return new WaitForSeconds(.5f);
		if (hasPartyMembers) {
			if (((Chara_UI_Map)partyMap [1]).getCharacter ().alive) {
				partyTurn ();
			}
		} else {
			state = States.ENEMYCHOICE;
			waiting = false;
		}
	}


	void partyTurn(){


		LewdUtilities.deleteAllButtons (buddyButtons);
		Character buddy = ((Chara_UI_Map)partyMap[1]).getCharacter();
		partyButtons.SetActive (false);
		buddyButtons.SetActive (true);
		List<Attack> attacks = buddy.attackList;
		for (int i = 0; i < attacks.Count; i++) {
			Attack attack = attacks [i];
			GameObject button = Instantiate (buddyAttackBtn);
			button.transform.SetParent (buddyButtons.transform);
			button.transform.localScale = Vector3.one;
			button.transform.localPosition = Vector3.one;
			button.GetComponentInChildren<Text> ().text = attack.getName ();
			button.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				button.GetComponent<CreateBuddyPanel>().create(attack, attack.type);
			});

		}


	}

	public void activateRegularButtons(){
		buddyButtons.SetActive (false);
		partyButtons.SetActive (true);
	}


	public void actEnemy( int x ) {
		GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
		Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

		if (currentChara.getAlive ()) {
			// busca un atack aqui de la lista del

			if (currentChara.horny > 80) {
				currentUI.GetComponent<Animator> ().Play ("HornyAnimation");
				combatLog.logText (currentChara.getName () + " is too horny to attack!");
			} else {

				int pos = LewdUtilities.getPartyAlivePos(partyMap);
				Attack attack = currentChara.getRandomAttack ();
				currentUI.GetComponent<Animator> ().Play ("Attack");
				combatLog.logEnemyText (currentChara.getName () + attack.getFlavorText());
				if (attack.type == Attack.TYPE.MAGIC) {dealDamageParty (  pos, (int)(attack.getDamage() * currentChara.magicPower));}
				if (attack.type == Attack.TYPE.DAMAGE) {dealDamageParty (  pos, (int)(attack.getDamage() * currentChara.attack));}
				if (attack.type == Attack.TYPE.HEAL) {healEnemy((int)(attack.getDamage() * currentChara.attack));}

			}
		}

	}

	void healEnemy(int heal){
		int random = Random.Range (0, enemyMap.Count);

		if (((Chara_UI_Map)enemyMap [random]).getCharacter ().alive) {
			((Chara_UI_Map)enemyMap [random]).getCharacter ().heal (heal);
		} else
			healEnemy (heal);

	}


	IEnumerator deleteEnemyUI(GameObject enemyUI) {

		yield return new WaitForSeconds(.5f);
		Destroy (enemyUI);

	}

	IEnumerator Lose() {
		float delta;
		DataAccess.Save(gameData);
		yield return new WaitForSeconds(1f);
		Destroy( GameObject.Find("MapManager"));
		SceneManager.LoadScene ("LoseScene");


	}

	IEnumerator delayedEnemyChoice() {

		yield return new WaitForSeconds(.3f);
		waiting = false;
	}






	public void changeState(States state){
		this.state = state;
	}


	public ArrayList getPartyMap(){
		return partyMap;
	}

	public ArrayList getEnemyMap(){
		return enemyMap;
	}

	public void attemptRun(){

		if (state == States.PLAYERCHOICE) {
			if (Random.Range (0, 3) == 1) {
				DataAccess.Save(gameData);
				Destroy( GameObject.Find("MapManager"));
				SceneManager.LoadScene ("MapScene");
			} else {
				combatLog.logText (" You attempt to run!!.... but <b>fail</b>.");
				state = States.ENEMYCHOICE;
			}
		}
	}

	public void attemptGrope(int pos){

		if (state == States.PLAYERCHOICE) {
			waiting = true;
			state = States.WAITPLAYER;
			Character character = ((Chara_UI_Map)enemyMap [pos]).getCharacter ();
			if (character.alive) {
				if (character.horny > 60 | Random.Range (0, 3) == 2) {
					//  

					character.makeHorny (20);
					if (character.horny > 69)
						combatLog.logGreenNeedy (character.getName (), character.female);
					else
						combatLog.logGreen (character.getName (), character.female);
				} else
					combatLog.logText (" You fail to grope " + character.getName () + ".");

			}
			StartCoroutine ("delayedEnemyChoice");		
		}
	}




	public void attemptRecruit(int id){
		combatLog.logText ("You attempt to recruit the enemy as part of your harem");
		combatLog.logSlowly ("...................................");
		StartCoroutine ("delayedRecruit",id);
	}



	public void attemptGetItem(Character character){


		if (character.itemPool != null & (Random.Range(0,3) == 2)) {
			string name = itemManager.AddItemToInventory (Random.Range (0, character.itemPool.Length),1);
			combatLog.logGreen (" You received a drop of :<b>" + name + "</b>.");
		} else {

			int money = (character.attack + character.getTotalHp () + character.horny) / 2;
			PlayerPrefs.SetInt ("gold", PlayerPrefs.GetInt ("gold", 0) + money);
			combatLog.logGreen (" You received <b>" + money + "</b> gold pieces.");
		}

		itemManager.saveCurrentInventory ();
		PlayerPrefs.Save ();

	}
}
