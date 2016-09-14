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
	public int enemyNumber = 1;
	int partyNumber = 1;
	bool waiting;
	bool fightFinished = false;
	public AudioClip kek;

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
		enemyMap = new ArrayList ();
		partyMap = new ArrayList ();
		state = States.PLAYERCHOICE;
		enemyPanel = GameObject.Find ("EnemyPanel");

		enemyNumber = Random.Range (1, 3);
		for (int x = 0; x < enemyNumber; x++) {
			GameObject newItem = Instantiate (itemPrefab) as GameObject;
			newItem.name = gameObject.name + " item at (" + x + ")";
			newItem.transform.parent = enemyPanel.gameObject.transform;
			newItem.transform.localScale = Vector3.one;
			newItem.transform.localPosition = Vector3.zero;
			Character character = reRollCharacter(EnemyCreator.create(Random.Range(0,3)));
			newItem.GetComponentsInChildren<Text> () [2].text = character.getName ();
			newItem.GetComponentsInChildren<Image> ()[3].overrideSprite = Resources.Load <Sprite> (character.image);
			enemyMap.Add(new Chara_UI_Map(newItem, character ));
		//	GameObject hpBar = GameObject.Find("hpCount").GetComponentInChildren<Text>();

		
		}


		for (int x = 0; x < partyNumber; x++) {
			GameObject item = GameObject.Find ("hpCombo");
			Character character = new Character (0, "you", 230, 1, 1, 1, "", false);
			partyMap.Add(new Chara_UI_Map(item, character ));
		}
			

	}

	private Character reRollCharacter(Character character){
		if (character.female) {

			while (isAlreadyInBattle (character)) {
				character = EnemyCreator.create (Random.Range (0, 2));
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



			if (currentChara.horny > 69) {
				try{
				currentUI.GetComponentsInChildren<Image> ()[3].overrideSprite = Resources.Load <Sprite> (currentChara.image+"_h");
				}catch(System.Exception e){
				currentUI.GetComponentsInChildren<Image> ()[3].overrideSprite = Resources.Load <Sprite> (currentChara.image);
				}

			}

			if (!currentChara.getAlive()) {

				if (LewdUtilities.aliveCount (this) == 0) {
					/// This is called if the enemy is alone and it's qt grill
					/// 
					if (currentChara.female) {
						if (!currentChara.gone) {
							Debug.Log (" Trapped en opening fuckpanel ");
							combatLog.clear (true);
							combatLog.logExhausted (currentChara.getName ());
							openFuckPanel (currentChara);
							currentChara.gone = true;

						}
					} else {

						if (!fightFinished) {
							GameObject panel = GameObject.Find ("PartyInfo");
							LewdUtilities.deleteAllButtons (panel);
							GameObject exit = Instantiate (fuckButton);
							exit.transform.parent = panel.transform;
							exit.transform.localScale = Vector3.one;
							exit.transform.localPosition = Vector3.zero;
							exit.GetComponentInChildren<Text> ().text = "Exit";
							exit.GetComponentInChildren<Button> ().onClick.AddListener (() => {
								SceneManager.LoadScene ("MapScene");


							});
							fightFinished = true;
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
			state = States.WAITPLAYER;
			waiting = true;
			StartCoroutine("playerCouroutine", position);
		}

	}

	public void openFuckPanel(Character character){

		Debug.Log (" Abrio fuckpanel");
		GameObject panel  = GameObject.Find ("PartyInfo");
		LewdUtilities.deleteAllButtons (panel);
		Scene scene = LewdSceneManager.getSceneFromId (character.id);

		if (scene != null) {
			for (int x = 0; x < 1; x++) {
				GameObject button = Instantiate (fuckButton);
				button.transform.parent = panel.transform;
				button.transform.localScale = Vector3.one;
				button.transform.localPosition = Vector3.zero;
				button.GetComponentInChildren<Text> ().text = scene.sceneName;
				button.GetComponentInChildren<Button> ().onClick.AddListener (() => {

					Destroy(enemyPanel.GetComponentsInChildren<Transform>()[1].gameObject);

					GameObject fuckpanel = GameObject.Find("fuckPanel");
					fuckpanel.GetComponentsInChildren<Image>()[1].sprite =  Resources.Load <Sprite> (scene.image);
					Transform transform = fuckpanel.transform;
					transform.localPosition = new Vector3(transform.position.x,transform.position.y,27);
					fuckpanel.transform.parent = enemyPanel.transform;

					LewdUtilities.deleteAllButtons (panel);
					combatLog.logText (scene.sceneText);
					GameObject exit = Instantiate (fuckButton);
						exit.transform.parent = panel.transform;
						exit.transform.localScale = Vector3.one;
						exit.transform.localPosition = Vector3.zero;
						exit.GetComponentInChildren<Text> ().text = "Exit";
						exit.GetComponentInChildren<Button> ().onClick.AddListener (() => {
							SceneManager.LoadScene("MapScene");


						});

				
				});
			}
		}


		GameObject recruit = Instantiate (fuckButton);
		recruit.transform.parent = panel.transform;
		recruit.transform.localScale = Vector3.one;
		recruit.transform.localPosition = Vector3.zero;
		recruit.GetComponentInChildren<Text> ().text = "Attempt Recruit";
		recruit.GetComponentInChildren<Button> ().onClick.AddListener (() => {

			attemptRecruit();
			LewdUtilities.deleteAllButtons(panel);
		});
	}
			

	public void dealDamageParty(int position){


		GameObject currentUI = ((Chara_UI_Map)partyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)partyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			int totalalive = LewdUtilities.aliveCount (this);
			character.receiveDamage (5);

			} 
			//((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
		//	combatLog.logText ("You smack the bitch with a dried cucumber angrily.");
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
				yield return new WaitForSeconds (.5f);
			else
				yield return new WaitForSeconds (.01f);
		}
		waiting = false;
		//Debug.Log ("DONE");
			
	}

	IEnumerator playerCouroutine(int position) {


		GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
		Character character = ((Chara_UI_Map)enemyMap [position]).getCharacter ();
		if (character.getAlive ()) {
			
			character.receiveDamage (Random.Range(4,7));
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<Animator> ().Play ("Hit");
			((Chara_UI_Map)enemyMap [position]).getGameObject ().GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/hit"+ Random.Range(1,5)));
			combatLog.logText ("You attack " + ((Chara_UI_Map)enemyMap [position]).getCharacter().name +" with your fists.");
		}
		yield return new WaitForSeconds(.5f);
	
		state = States.ENEMYCHOICE;
		waiting = false;
	//	Debug.Log ("DONE");

	}


	public void actEnemy( int x ) {
		GameObject currentUI = ((Chara_UI_Map)enemyMap [x]).getGameObject ();
		Character currentChara = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

		if (currentChara.getAlive ()) {
			// busca un atack aqui de la lista del

			if (currentChara.horny > 80) {
				combatLog.logText (currentChara.getName () + " is too horny to attack!");
			} else {
				currentUI.GetComponent<Animator> ().Play ("Attack");
				combatLog.logEnemyText (currentChara.getName () + " attacks you with the force of a thousand suns.");
				dealDamageParty (0);
			}
		}
	}


	IEnumerator deleteEnemyUI(GameObject enemyUI) {
		
		yield return new WaitForSeconds(.5f);
		Destroy (enemyUI);

	}

	IEnumerator Lose() {

		yield return new WaitForSeconds(.5f);
		SceneManager.LoadScene ("LoseScene");

	}

	IEnumerator delayedEnemyChoice() {

		yield return new WaitForSeconds(.3f);
		waiting = false;
	}

	IEnumerator delayedRecruit() {
		yield return new WaitForSeconds(.3f);
		if (Random.Range (0, 3) == 1) {

			combatLog.logText (" The enemy decides to join you. ");


		} else {
			
			combatLog.logText (" The enemy used the chance to run away. ");
			int position = LewdUtilities.getLastVisiblePosition(this);
			GameObject currentUI = ((Chara_UI_Map)enemyMap [position]).getGameObject ();
			currentUI.GetComponent<Animator> ().Play ("Dead");

		}

		GameObject panel  = GameObject.Find ("PartyInfo");
	
			for (int x = 0; x < 1; x++) {
				GameObject button = Instantiate (fuckButton);
				button.transform.parent = panel.transform;
				button.transform.localScale = Vector3.one;
				button.transform.localPosition = Vector3.zero;
				button.GetComponentInChildren<Text> ().text = "Exit";
				button.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				SceneManager.LoadScene("MapScene");


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
				SceneManager.LoadScene ("MapScene");
			}
			combatLog.logText (" You attempt to run!!.... but <b>fail</b>.");
			state = States.ENEMYCHOICE;

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




	public void attemptRecruit(){
		combatLog.logText ("You attempt to recruit the enemy as part of your harem");
		combatLog.logSlowly ("...................................");
		StartCoroutine ("delayedRecruit");
	}
}
