using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiftManager : MonoBehaviour {

	public GameObject itemUI;
	public GameObject itemPanel;
	public GameObject partyButtons;

	public  ArrayList 	items;
	private string	 	allItemString;
	private string[]	itemCombo;
	private GameObject 	itemContainer;
	private int[] 		itemArray;
	private ArrayList 	itemsArray;

	public InteractionManager interactionManager;
	public GameObject button;
	public GameObject panel;	
	public GameObject empty;
	//public UIManagerScript manager;
	GameObject panelParent;
	GameObject popUp;
	ArrayList party;
	Character slut;

	bool panelIsActive = false;


	void Start () {

		itemContainer 		= gameObject;
		itemsArray 			= new ArrayList();
		allItemString 		= PlayerPrefs.GetString ("items","");
		itemCombo 			= allItemString.Split (';');
		slut 				= interactionManager.currentCharacter;


		for ( int x = 0; x < itemCombo.Length ; x++) {
			if((itemCombo[x])!= ""){
				int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
				int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
				Item item 										= ItemCreator.createItem(id, itemAmount);
				if (item.type == Item.TYPE.GIFT) {

					GameObject temp = Instantiate (itemUI);
					temp.transform.parent = itemContainer.transform;
					temp.transform.localScale = Vector3.one;
					temp.transform.localPosition = Vector3.one;
					temp.GetComponentsInChildren<Text> () [0].text = item.name;
					temp.GetComponentsInChildren<Text> () [1].text = itemAmount + "x";

					temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {



						create (item.id);





					});
					item.uiRef = temp;

				}
				itemsArray.Add (item);
			}

		}
		/*

		GameObject backButton = Instantiate(itemUI);
		backButton.transform.parent = itemPanel.transform;
		backButton.transform.localScale = Vector3.one;
		backButton.transform.localPosition = Vector3.one;
		backButton.GetComponentInChildren<Text> ().text = "Back";
		backButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
			hideItemPanel();

		});
*/


	}


	void Update () {


		if (Input.GetKeyDown (KeyCode.Space)) {
			AddItemToInventory (200,1);
		}

	}


	public void showItemPanel(){
		partyButtons.SetActive (false);
		itemPanel	.SetActive (true);
		itemPanel	.GetComponent<Animator>	().Play ("itemPanelSlide");
	}

	public void hideItemPanel(){
		partyButtons.SetActive (true);
		itemPanel	.GetComponent<Animator>().Play ("itemPanelSlideOut");
	}


	public void removeItem (int itemId){
		Item item = getItemById (itemId);
		item.amount -= 1;

		item.uiRef.GetComponentsInChildren<Text> () [1].text = item.amount + "x";
		if (item.amount == 0) {
			Destroy (item.uiRef);
			itemsArray.Remove (item);
		}
		saveCurrentInventory ();
	}


	public Item getItemById(int itemId){

		//		Debug.Log (itemCombo.Length);
		//		Debug.Log (((Item)itemsArray [0]).id);
		for (int x = 0; x < itemsArray.Count; x++) {

			if (((Item)itemsArray [x]).id == itemId)
				return ((Item)itemsArray [x]);
		}

		return null;
	}




	public string AddItemToInventory( int id , int amount ){


		string currentItemList = PlayerPrefs.GetString ("items", "");
		//ArrayList items =

		string result = "";

		Debug.Log (" intento agregar " + id + " a la lista de items ");
		Item item = getItemById (id) ;
		if (item != null) {
			item.amount += amount;
			result = item.name;
		} else {
			Item temp = ItemCreator.createItem (id, amount);
			itemsArray.Add (temp);
			result = temp.name;
		}


		saveCurrentInventory ();
		return result;



	}

	public void saveCurrentInventory(){
		string newItemList = "";
		for (int x = 0; x < itemsArray.Count; x++) {
			Item temp = (Item)itemsArray [x];
			newItemList += temp.id + "," + temp.amount + ";";
		}
		PlayerPrefs.SetString ("items", newItemList);
		Debug.Log("salvo :" +newItemList);
		PlayerPrefs.Save ();
	}


	public void create(int itemId){


		if (!panelIsActive) {
			ArrayList enemyMap = party;
			panelParent = GameObject.Find ("ItemContainer");
			//	if (enemyMap.Count > 1 & aliveCount () > 1) {

	//		Animator animator = panelParent.transform.parent.gameObject.GetComponent<Animator> ();
			popUp = Instantiate (panel);
			popUp.transform.parent = gameObject.transform;
			popUp.transform.localScale = Vector3.one;
			popUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);
			popUp.transform.parent = empty.transform;
			popUp.transform.localScale = Vector3.one;
			popUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);

			/*

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
						selectAction (itemId, character);
						removeItem (itemId);


					});


				}

			}/*/

			panelIsActive = true;
			GameObject youButton = Instantiate (button);
			youButton.transform.parent = popUp.transform;
			youButton.transform.localScale = Vector3.one;
			youButton.transform.localPosition = Vector3.zero;
			youButton.GetComponentInChildren<Text> ().text = "Use";
			youButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				selectAction (itemId, interactionManager.currentCharacter);
				Destroy (popUp);
				panelIsActive = false;
				removeItem (itemId);

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


		Debug.Log (character.love);
		ItemCreator.useGift (character, itemId);
		Debug.Log (character.love);

		DataAccess.Save (interactionManager.gameData);


	}






}
