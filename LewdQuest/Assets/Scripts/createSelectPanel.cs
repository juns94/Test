using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class createSelectPanel : MonoBehaviour {



	public GameObject button;
	public GameObject panel;	
	public GameObject empty;
	public UIManagerScript manager;
	public ItemManager itemManager;
	GameObject panelParent;
	GameObject popUp;



	// Use this for initialization
	void Start () {

		//	button = GameObject.Find ("_popUpButton");
		//	panel  = GameObject.Find ("_popUpPanel");
		itemManager = GetComponent<ItemManager>();
		manager 	= GameObject.Find ("UIManager").GetComponent<UIManagerScript>();
	}

	// Update is called once per frame
	void Update () {

	}

	int aliveCount(){
		ArrayList enemyMap = manager.getEnemyMap ();
		int counter = 0;
		for (int x = 0; x < enemyMap.Count; x++) {
			if(((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive()) counter++;

		}
		return counter;
	}


	int getLastAlivePosition(){
		ArrayList enemyMap = manager.getEnemyMap ();
		for (int x = 0; x < enemyMap.Count; x++) {
			if (((Chara_UI_Map)enemyMap [x]).getCharacter ().getAlive ())
				return x;

		}
		return 	0;

	}


	public void create(int itemId){



			ArrayList enemyMap = manager.getEnemyMap ();
			panelParent = GameObject.Find ("ItemContainer");
		//	if (enemyMap.Count > 1 & aliveCount () > 1) {
			
				Animator animator = panelParent.transform.parent.gameObject.GetComponent<Animator> ();
				popUp = Instantiate (panel);
				popUp.transform.parent 			= gameObject.transform;
				popUp.transform.localScale 		= Vector3.one;
				popUp.transform.localPosition 	= new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);
				popUp.transform.parent			= empty.transform;
				popUp.transform.localScale 		= Vector3.one;
				popUp.transform.localPosition 	= new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);
				disableOldGui ();



				for (int x = 0; x < enemyMap.Count; x++) {
					Character character = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

					if (character.getAlive ()) {
						GameObject buttonPopUp 								= Instantiate (button);
						buttonPopUp.transform.parent 						= popUp.transform;
						buttonPopUp.transform.localScale 					= Vector3.one;
						buttonPopUp.GetComponentInChildren<Text> ().text 	= character.getName ();
						buttonPopUp.transform.localPosition 				= new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);	
						buttonPopUp.name 									= "" + x;
						buttonPopUp.GetComponent<Button> ().onClick.AddListener (() => {
					
									Destroy (popUp);
									enableOldGui ();
									onHoverExit (int.Parse (buttonPopUp.name));
									selectAction (itemId, int.Parse (buttonPopUp.name), false);
									animator.Play("itemPanelSlideOut");
									itemManager.hideItemPanel();
									itemManager.removeItem(itemId);

						});

						EventTrigger.Entry eventEntry = new EventTrigger.Entry ();
						eventEntry.eventID = EventTriggerType.PointerEnter;
						eventEntry.callback.AddListener ((eventData) => {
							onHover (int.Parse (buttonPopUp.name));
						});

						EventTrigger.Entry eventExit = new EventTrigger.Entry ();
						eventExit.eventID = EventTriggerType.PointerExit;
						eventExit.callback.AddListener ((eventData) => {
							onHoverExit (int.Parse (buttonPopUp.name));
						});


						buttonPopUp.AddComponent<EventTrigger> ();
						buttonPopUp.GetComponent<EventTrigger> ().triggers.Add (eventEntry);
						buttonPopUp.GetComponent<EventTrigger> ().triggers.Add (eventExit);
					}

				}


				GameObject youButton = Instantiate (button);
				youButton.transform.parent = popUp.transform;
				youButton.transform.localScale = Vector3.one;
				youButton.transform.localPosition = Vector3.zero;
				youButton.GetComponentInChildren<Text> ().text = "You";
				youButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
					selectAction (itemId, 0, true);
					Destroy (popUp);
					enableOldGui ();	
					itemManager.hideItemPanel();
					itemManager.removeItem(itemId);
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
				enableOldGui ();
				});


	}


	void onHover(int position){
		//	Debug.Log ("entro");
		((Chara_UI_Map)manager.getEnemyMap () [position]).getGameObject ().GetComponentInChildren<Outline>().enabled = true;
	}

	void onHoverExit(int position){
		//	Debug.Log ("salio");
		((Chara_UI_Map)manager.getEnemyMap () [position]).getGameObject ().GetComponentInChildren<Outline>().enabled = false;
	}




	void disableOldGui(){
		
		Button[] oldButtons = panelParent.GetComponentsInChildren<Button> ();
		if(oldButtons != null)
			for (int x = 0; x < oldButtons.Length; x++) {
				oldButtons [x].enabled = false;
			}

	}



	void enableOldGui(){
		

		Button[] oldButtons = panelParent.GetComponentsInChildren<Button> ();
		if(oldButtons != null)
			for (int x = 0; x < oldButtons.Length; x++) {
				oldButtons [x].enabled = enabled;
			}

	}


	void selectAction(int itemId, int pos, bool isPlayer){
		manager.useItem(pos, ItemCreator.createItem (itemId,1), isPlayer);
		}



}
