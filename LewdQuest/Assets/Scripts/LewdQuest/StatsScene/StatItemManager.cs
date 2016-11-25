using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class StatItemManager : MonoBehaviour {


	public GameObject itemUI;
	public GameObject itemPanel;
	public GameObject partyButtons;

	private createSelectPanelStat panelCreatorMap;
	public  ArrayList 	items;
	private string	 	allItemString;
	private string[]	itemCombo;
	private GameObject 	itemContainer;
	private int[] 		itemArray;
	private ArrayList 	itemsArray;


	void Start () {

		itemContainer 		= itemPanel.transform.GetChild (0).gameObject;
		itemsArray 			= new ArrayList();
		allItemString 		= PlayerPrefs.GetString ("items","");
		itemCombo 			= allItemString.Split (';');



			panelCreatorMap = GetComponent<createSelectPanelStat> ();




		for ( int x = 0; x < itemCombo.Length ; x++) {
			if((itemCombo[x])!= ""){
				int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
				int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
				Item item 										= ItemCreator.createItem(id, itemAmount);
				if (item.type == Item.TYPE.PERMANENT) {
					
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

					EventTrigger.Entry eventExit = new EventTrigger.Entry	 ();
					eventExit.eventID = EventTriggerType.PointerExit;
					eventExit.callback.AddListener ((eventData) => {			

						temp.GetComponent<OnHoverItem> ().hideText ();
					});


					temp.AddComponent<EventTrigger> ();
					temp.GetComponent<EventTrigger> ().triggers.Add (eventEntry);
					temp.GetComponent<EventTrigger> ().triggers.Add (eventExit);		

					temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {



							panelCreatorMap.create (item.id);
						




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
			//this.gameObject.GetComponent<Button> ().enabled = true;
		});



	}


	void Update () {

	

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (GameObject.Find ("MapManager"));
			SceneManager.LoadScene ("MapScene");
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
