using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class createSelectPanelStat : MonoBehaviour {



	public GameObject button;
	public GameObject panel;	
	public GameObject empty;
	//public UIManagerScript manager;
	public StatItemManager itemManager;
	GameObject panelParent;
	GameObject popUp;
	ArrayList party;
	Character you;


	bool panelIsActive = false;


	void Start () {
		
		itemManager = GetComponent<StatItemManager>();
		party = new ArrayList ();
		Character character = HeroUtils.getHero ();
		//party.Add (character);
		for (int x = 0; x < 100; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) >

				1) {
				//party.Add(EnemyCreator.create(0,x));
			}
		}

		you = HeroUtils.getHero();


	}

	// Update is called once per frame
	void Update () {

	}

	int aliveCount(){
		ArrayList enemyMap = party;
		int counter = 0;
		for (int x = 0; x < enemyMap.Count; x++) {
			if(((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive()) counter++;

		}
		return counter;
	}


	int getLastAlivePosition(){
		ArrayList enemyMap = party;
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive ())
				return x;

		}
		return 	0;

	}


	public void create(int itemId){


		if (!panelIsActive) {
			ArrayList enemyMap = party;
			panelParent = GameObject.Find ("ItemContainer");
			//	if (enemyMap.Count > 1 & aliveCount () > 1) {

			Animator animator = panelParent.transform.parent.gameObject.GetComponent<Animator> ();
			popUp = Instantiate (panel);
			popUp.transform.parent = gameObject.transform;
			popUp.transform.localScale = Vector3.one;
			popUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);
			popUp.transform.parent = empty.transform;
			popUp.transform.localScale = Vector3.one;
			popUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);



			for (int x = 0; x < enemyMap.Count; x++) {
				Character character = ((Character)enemyMap [x]);

				if (character.getAlive ()) {
					GameObject buttonPopUp = Instantiate (button);
					buttonPopUp.transform.parent = popUp.transform;
					buttonPopUp.transform.localScale = Vector3.one;
					buttonPopUp.GetComponentInChildren<Text> ().text = character.getName ();
					buttonPopUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);	
					buttonPopUp.name = "" + x;
					panelIsActive = true;
					buttonPopUp.GetComponent<Button> ().onClick.AddListener (() => {

						Destroy (popUp);
						selectAction (itemId, you);
						if (itemManager != null) {
							//	itemManager.hideItemPanel();
							itemManager.removeItem (itemId);
						}

					});


				}

			}

			panelIsActive = true;
			GameObject youButton = Instantiate (button);
			youButton.transform.parent = popUp.transform;
			youButton.transform.localScale = Vector3.one;
			youButton.transform.localPosition = Vector3.zero;
			youButton.GetComponentInChildren<Text> ().text = "You";
			youButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				selectAction (itemId, you);
				Destroy (popUp);
				panelIsActive = false;
				if (itemManager != null) {
					//	itemManager.hideItemPanel();
					itemManager.removeItem (itemId);
				}
				//animator.Play("itemPanelSlideOut");
				//this.gameObject.GetComponent<Button> ().enabled = true;
			});




			GameObject cancelButton = Instantiate (button);
			cancelButton.transform.parent = popUp.transform;
			cancelButton.transform.localScale = Vector3.one;
			cancelButton.transform.localPosition = Vector3.zero;
			cancelButton.GetComponentInChildren<Text> ().text = "Cancel";
			cancelButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				Destroy (popUp);
				panelIsActive = false;
		
			});

		}

	}
		


	void selectAction(int itemId, Character character){

		ItemCreator.useItem (character, itemId);
		HeroUtils.saveHero (character);
			
	}



}
