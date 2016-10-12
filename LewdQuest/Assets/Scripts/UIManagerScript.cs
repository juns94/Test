using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour {
	public GameObject fuckButton;
	public GameObject itemPrefab;
	public CombatLog combatLog;
	public EnemyCreator enemyCreator;
	ArrayList partyMap;
	ArrayList enemyMap;
	GameObject enemyPanel;
	public int enemyNumber = 3;
	int partyNumber = 1;
	int lastAlivePosition;
	bool waiting;
	bool fightFinished = false;
	GameObject mapManager;
	GameObject itemPanel;
	public ItemManager itemManager;
	public GameObject partyInfo;

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


	void onGUI(){

		/*
		alpha += fadeDirection * fadeSpeed * Time.deltaTime; 
		alpha = Mathf.Clamp01 (alpha);

		
		*/
			

	}


	void Start () {


		int region = 0;
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

	//	itemPanel.SetActive (false);

		for (int x = 0; x < enemyNumber; x++) {
			Character character = null;
			GameObject newItem = Instantiate (itemPrefab) as GameObject;
			newItem.transform.parent 		= enemyPanel.gameObject.transform;
			newItem.transform.localScale 	= Vector3.one;
			newItem.transform.localPosition = Vector3.zero;
			if (safe) {
				character = reRollCharacter (EnemyCreator.create (region, -1));
			} else {
				EnemyCreator.create (4,-1);
			}
			newItem.GetComponentsInChildren<Text> () [2].text = character.getName ();
			newItem.GetComponentsInChildren<Image> ()[3].overrideSprite = Resources.Load <Sprite> (character.image);
			enemyMap.Add(new Chara_UI_Map(newItem, character ));
		}





		for (int x = 0; x < partyNumber; x++) {
			GameObject item = GameObject.Find ("hpCombo");
			Character character = new Character (0, "Yourself", 230, 1, 1, 1, "", false);
			character.hp = PlayerPrefs.GetInt ("hp", 230);
			partyMap.Add(new Chara_UI_Map(item, character ));
		}


	//	gameObject.GetComponent<Fading> ().beginFade (1);
			

	}

	private Character reRollCharacter(Character character){
		if (character.female) {

			while (isAlreadyInBattle (character)) {
				character = EnemyCreator.create (Random.Range (0, 2),-1);
			}

		} else {
			return character;
		}
		return character;
	}
	private bool isAlreadyInBattle(Character character){
		for (int x = 0; x < enemyMap.Count; x++) {
			if (character.id == ((Chara_UI_Map)enemyMap [x]).getCharacter ().id)
				return true;
		}
		return false;
	}
	

	void Update () {


		if (Input.GetKeyDown (KeyCode.Escape)) {
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
		for (int x = 0; x < enemyMap.Count; x++) {


			lastAlivePosition = LewdUtilities.getLastAlivePosition (this);
			Image[] images = null;
			GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
			Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();
			if (currentUI != null) {
				images = currentUI.GetComponentsInChildren<Image> ();
				currentUI.GetComponentInChildren<HpDecreaseSlow> ().setDamage ((float)currentChara.getHp (), (float)currentChara.getTotalHp ());
			}



			if (currentChara.horny > 69) {
				try{
					Sprite temp =  Resources.Load <Sprite> (currentChara.image+"_h");
					if(temp!= null)
					currentUI.GetComponentsInChildren<Image> ()[3].overrideSprite = temp;
				}catch(System.Exception e){
				//currentUI.GetComponentsInChildren<Image> ()[3].overrideSprite = Resources.Load <Sprite> (currentChara.image);
				}

			}

			if (!currentChara.getAlive())  {  ////// SI ESTA MUERTO, SIN IMPORTAR LA POSICION!!!!


			

				if (LewdUtilities.aliveCount (this) == 0 & lastAlivePosition == x) {
					/// This is called if the enemy is alone and it's qt grill
					/// 
					if (currentChara.female) {
						if (!currentChara.gone) {

							combatLog.clear (true);
							combatLog.logExhausted (currentChara.getName ());
							openFuckPanel (currentChara);
							currentChara.gone = true;

						}
					} else {


						if (!fightFinished) {

							GameObject panel = partyInfo;
							//GameObject panel 				= GameObject.Find ("PartyInfo");
							LewdUtilities.deleteAllButtons (panel);

							GameObject exit 				= Instantiate (fuckButton);
							exit.transform.parent 			= panel.transform;
							exit.transform.localScale 		= Vector3.one;
							exit.transform.localPosition 	= Vector3.zero;
							exit.GetComponentInChildren<Text>().text = "Exit";
							exit.GetComponentInChildren<Button>().onClick.AddListener (() => {
								SceneManager.LoadScene ("MapScene");
								Destroy( GameObject.Find("MapManager"));

							});
							fightFinished = true;

							attemptGetItem (currentChara);

						}

					}



				}


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
			if (!currentChara.alive)
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
				 currentUI = ((Chara_UI_Map)partyMap [0]).getGameObject ();
				 character = ((Chara_UI_Map)partyMap [0]).getCharacter ();
			}


			if (character.getAlive ()) {
				ItemCreator.useItem (character, item.id);
				//character.receiveDamage (Random.Range(4,7));
				//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
				//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
				combatLog.logGreen("You use the <size=20><b>"+ item.name + "</b></size> on " + character.name +".");
			}

			Invoke ("setEnemyTurn", 0.5f);
			//StartCoroutine("itemCouroutine", position, itemId, isPlayer);
		}

	}
	public void dealDamage(int position){
		if(state == States.PLAYERCHOICE){
			state = States.WAITPLAYER;
			waiting = true;
			StartCoroutine("playerCouroutine", position);
		}

	}

	public void openFuckPanel(Character character){
		GameObject panel = partyInfo;
		//GameObject panel  = GameObject.Find ("PartyInfo");
		LewdUtilities.deleteAllButtons (panel);
		Scene scene = LewdSceneManager.getSceneFromId (character.id, character.horny);

		if (scene != null) {
			for (int x = 0; x < 1; x++) {

				GameObject button = Instantiate (fuckButton);
				button.transform.parent = panel.transform;
				button.transform.localScale = Vector3.one;
				button.transform.localPosition = Vector3.zero;
				button.GetComponentInChildren<Text> ().text = scene.sceneName;
				button.GetComponentInChildren<Button> ().onClick.AddListener (() => {

					this.gameObject.AddComponent<SceneManagerScript>();
					SceneManagerScript sceneManager = this.gameObject.GetComponent<SceneManagerScript>();
					sceneManager.setEverything(enemyPanel, panel,scene,character,this);
					sceneManager.startManager();
				});

			}
		}


		GameObject recruit = Instantiate (fuckButton);
		recruit.transform.parent = panel.transform;
		recruit.transform.localScale = Vector3.one;
		recruit.transform.localPosition = Vector3.zero;
		recruit.GetComponentInChildren<Text> ().text = "Attempt Recruit";
		recruit.GetComponentInChildren<Button> ().onClick.AddListener (() => {

			attemptRecruit(character.id);
			LewdUtilities.deleteAllButtons(panel);
		});
	}
			

	public void dealDamageParty(int position, int damage){
		GameObject currentUI = ((Chara_UI_Map)partyMap [position]).getGameObject ();
		Character  character = ((Chara_UI_Map)partyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			int totalalive = LewdUtilities.aliveCount (this);
			character.receiveDamage (damage);
			PlayerPrefs.SetInt ("hp", character.getHp());
			PlayerPrefs.Save ();
			} 
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

	IEnumerator playerCouroutine(int position) {


		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			character.receiveDamage (Random.Range(3 , 8) + 20);
			//character.receiveDamage (Random.Range(4,7));
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
			combatLog.logText ("You attack " + ((Chara_UI_Map)enemyMap [position]).getCharacter().name +" with your fists.");
		}
		yield return new WaitForSeconds(.5f);
	
		state = States.ENEMYCHOICE;
		waiting = false;
	}

	IEnumerator itemCouroutine(int position, int itemId, bool playerItem) {

		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			ItemCreator.useItem (character, itemId);
			//character.receiveDamage (Random.Range(4,7));
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
			combatLog.logText ("You use the "+ " on " + ((Chara_UI_Map)enemyMap [position]).getCharacter().name +".");
		}
		yield return new WaitForSeconds(.5f);

		state = States.ENEMYCHOICE;
		waiting = false;
	}


	public void actEnemy( int x ) {
		GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
		Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

		if (currentChara.getAlive ()) {
			// busca un atack aqui de la lista del

			if (currentChara.horny > 80) {
				combatLog.logText (currentChara.getName () + " is too horny to attack!");
			} else {
				Attack attack = currentChara.getRandomAttack ();
				currentUI.GetComponent<Animator> ().Play ("Attack");
				combatLog.logEnemyText (currentChara.getName () + attack.getFlavorText());
				if (attack.type == Attack.TYPE.DAMAGE) {dealDamageParty (0, (int)(attack.getDamage() * currentChara.attack));}
				if (attack.type == Attack.TYPE.HEAL) {healEnemy((int)(attack.getDamage() * currentChara.attack));}


		//		dealDamageParty (0);
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
		//delta = gameObject.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds(1f);
		Destroy( GameObject.Find("MapManager"));
		SceneManager.LoadScene ("LoseScene");

	}

	IEnumerator delayedEnemyChoice() {

		yield return new WaitForSeconds(.3f);
		waiting = false;
	}

	IEnumerator delayedRecruit(int id) {
		yield return new WaitForSeconds(.3f);
		if (Random.Range (0, 3) == 1 & id != 1) {

			combatLog.logText (" The enemy decides to join you. ");
			PlayerPrefs.SetInt ("" + id, 1);
			PlayerPrefs.Save ();


		} else {
			
			combatLog.logText (" The enemy used the chance to run away. ");
			int position = LewdUtilities.getLastVisiblePosition(this);
			GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
			currentUI.GetComponent<Animator> ().Play ("Dead");

		}

		GameObject panel = partyInfo;
		//GameObject panel  = GameObject.Find ("PartyInfo");
	
			for (int x = 0; x < 1; x++) {
				GameObject button = Instantiate (fuckButton);
				button.transform.parent = panel.transform;
				button.transform.localScale = Vector3.one;
				button.transform.localPosition = Vector3.zero;
				button.GetComponentInChildren<Text> ().text = "Exit";
				button.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				SceneManager.LoadScene("MapScene");
				Destroy(mapManager);


				});
			}

			
	}





	public void changeState(States state){
		this.state = state;
	}



	public ArrayList getEnemyMap(){
		return enemyMap;
	}

	public void attemptRun(){

		if (state == States.PLAYERCHOICE) {
			if (Random.Range (0, 3) == 1) {
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
