using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour {


	public RawImage 	fader;
	public GameObject 	itemUI;
	public GameObject 	itemPanel;
	public GameObject 	partyButtons;
	private createSelectPanel panelCreator;
	private createSelectPanelMap panelCreatorMap;
	public  ArrayList 	items;
	private string	 	allItemString;
	private string[]	itemCombo;
	private GameObject 	itemContainer;
	private int[] 		itemArray;
	private ArrayList 	itemsArray;


	void Start () {

		try{
		itemContainer 		= itemPanel.transform.GetChild (0).gameObject;
		}catch(Exception e){		}
	
		panelCreator 		= GetComponent<createSelectPanel> ();
		itemsArray 			= new ArrayList();
		allItemString 		= PlayerPrefs.GetString ("items","");
		itemCombo 			= allItemString.Split (';');
		if (panelCreator == null) {	panelCreatorMap = GetComponent<createSelectPanelMap> ();	}



		for ( int x = 0; x < itemCombo.Length ; x++) {
			if((itemCombo[x])!= ""){
			int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
			int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
			Item item 										= ItemCreator.createItem(id, itemAmount);
				if (item.type == Item.TYPE.CONSUMABLE) {
					GameObject temp = Instantiate (itemUI);
					temp.transform.parent = itemContainer.transform;
					temp.transform.localScale = Vector3.one;
					temp.transform.localPosition = Vector3.one;
					temp.GetComponentsInChildren<Text> () [0].text = item.name;
					temp.GetComponentsInChildren<Text> () [1].text = itemAmount + "x";

					EventTrigger.Entry eventEntry = new EventTrigger.Entry ();
					eventEntry.eventID = EventTriggerType.PointerEnter;
					eventEntry.callback.AddListener ((eventData) => {
						temp.GetComponent<OnHoverItem> ().displayText (item.description);
					});

					EventTrigger.Entry eventExit = new EventTrigger.Entry ();
					eventExit.eventID = EventTriggerType.PointerExit;
					eventExit.callback.AddListener ((eventData) => {			
						temp.GetComponent<OnHoverItem> ().hideText ();
					});


					temp.AddComponent<EventTrigger> ();
					temp.GetComponent<EventTrigger> ().triggers.Add (eventEntry);
					temp.GetComponent<EventTrigger> ().triggers.Add (eventExit);		

					temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {


						if (panelCreator == null) {
							panelCreatorMap.create (item.id);
						} else {
							panelCreator.create (item.id);
						}

					});
					item.uiRef = temp;

					}
				itemsArray.Add (item);
			}

		}


		GameObject backButton = Instantiate(itemUI);
		backButton.transform.parent = itemPanel.transform;
		backButton.transform.localScale = Vector3.one;
		backButton.transform.localPosition = Vector3.one;
		backButton.GetComponentInChildren<Text> ().text = "Back";
		backButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
			hideItemPanel();
		});


	
	}



	public void showType(int type){	

		Item.TYPE itemType = Item.TYPE.CONSUMABLE;
		switch (type) {
		case 0:
			itemType = Item.TYPE.CONSUMABLE;
			break;
		case 1:
			itemType = Item.TYPE.GIFT;
			break;
		case 2:
			itemType = Item.TYPE.QUEST;
			break;
		case 3:
			itemType = Item.TYPE.PERMANENT;
			break;
		default:
			itemType = Item.TYPE.CONSUMABLE;
			break;
		}



		foreach (Transform item in itemContainer.transform) {
			Destroy (item.gameObject);
		}

		for ( int x = 0; x < itemCombo.Length ; x++) {
			if((itemCombo[x])!= ""){
				int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
				int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
				Item item 										= ItemCreator.createItem(id, itemAmount);
				if (item.type == itemType) {
					GameObject temp = Instantiate (itemUI);
					temp.transform.parent = itemContainer.transform;
					temp.transform.localScale = Vector3.one;
					temp.transform.localPosition = Vector3.one;
					temp.GetComponentsInChildren<Text> () [0].text = item.name;
					temp.GetComponentsInChildren<Text> () [1].text = itemAmount + "x";

					EventTrigger.Entry eventEntry = new EventTrigger.Entry ();
					eventEntry.eventID = EventTriggerType.PointerEnter;
					eventEntry.callback.AddListener ((eventData) => {
						temp.GetComponent<OnHoverItem> ().displayText (item.description);
					});

					EventTrigger.Entry eventExit = new EventTrigger.Entry ();
					eventExit.eventID = EventTriggerType.PointerExit;
					eventExit.callback.AddListener ((eventData) => {			
						temp.GetComponent<OnHoverItem> ().hideText ();
					});


					temp.AddComponent<EventTrigger> ();
					temp.GetComponent<EventTrigger> ().triggers.Add (eventEntry);
					temp.GetComponent<EventTrigger> ().triggers.Add (eventExit);		

					temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {

						if(type ==1){
							
						if (panelCreator == null) {
							panelCreatorMap.create (item.id);
						} else {
							panelCreator.create (item.id);
						}
						}

					});
					item.uiRef = temp;

				}
				itemsArray.Add (item);
			}

		}

	}

	public void showAllOther(){

	


		foreach (Transform item in itemContainer.transform) {
			Destroy (item.gameObject);
		}

		for ( int x = 0; x < itemCombo.Length ; x++) {
			if((itemCombo[x])!= ""){
				int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
				int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
				Item item 										= ItemCreator.createItem(id, itemAmount);
				if (item.type != Item.TYPE.CONSUMABLE) {
					GameObject temp = Instantiate (itemUI);
					temp.transform.parent = itemContainer.transform;
					temp.transform.localScale = Vector3.one;
					temp.transform.localPosition = Vector3.one;
					temp.GetComponentsInChildren<Text> () [0].text = item.name;
					temp.GetComponentsInChildren<Text> () [1].text = itemAmount + "x";

					EventTrigger.Entry eventEntry = new EventTrigger.Entry ();
					eventEntry.eventID = EventTriggerType.PointerEnter;
					eventEntry.callback.AddListener ((eventData) => {
						temp.GetComponent<OnHoverItem> ().displayText (item.description);
					});

					EventTrigger.Entry eventExit = new EventTrigger.Entry ();
					eventExit.eventID = EventTriggerType.PointerExit;
					eventExit.callback.AddListener ((eventData) => {			
						temp.GetComponent<OnHoverItem> ().hideText ();
					});


					temp.AddComponent<EventTrigger> ();
					temp.GetComponent<EventTrigger> ().triggers.Add (eventEntry);
					temp.GetComponent<EventTrigger> ().triggers.Add (eventExit);		

					temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {

						/*
						if (panelCreator == null) {
							panelCreatorMap.create (item.id);
						} else {
							panelCreator.create (item.id);
						}*/

					});
					item.uiRef = temp;

				}
				itemsArray.Add (item);
			}

		}

	}

	

	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Space)) {
		//	PlayerPrefs.SetString("items","0,4;1,2;2,1;3,10;200,5")	;
		//	PlayerPrefs.Save ();
		}*/

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (GameObject.Find ("MapManager"));
			SceneManager.LoadScene ("MapScene");
		}
	
	}


	public void showItemPanel(){
		if (fader != null)  fader.gameObject.SetActive (true);
		partyButtons.SetActive (false);
		itemPanel	.SetActive (true);
		itemPanel	.GetComponent<Animator>	().Play ("itemPanelSlide");
	}

	public void hideItemPanel(){
		if (fader != null)  fader.gameObject.SetActive (false);

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
		for (int x = 0; x < itemsArray.Count; x++) {
			if (((Item)itemsArray [x]).id == itemId)
				return ((Item)itemsArray [x]);
		}
		return null;
		}



	public string AddItemToInventory( int id , int amount ){
		string currentItemList = PlayerPrefs.GetString ("items", "");
		string result = "";
		Debug.Log (" intento agregar " + id);
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
}
