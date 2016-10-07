using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemManager : MonoBehaviour {


	public GameObject itemUI;
	public GameObject itemPanel;
	public GameObject partyButtons;



	private createSelectPanel panelCreator;
	private createSelectPanelMap panelCreatorMap;
	public  ArrayList 	items;
	private string	 	allItemString;
	private string[]	itemCombo;
	private GameObject 	itemContainer;
	private int[] 		itemArray;
	private ArrayList 	itemsArray;


	void Start () {
		itemPanel 			= GameObject.Find ("ItemPanel");
		itemContainer 		= itemPanel.transform.GetChild (0).gameObject;
		allItemString	 	= "0,4;1,2;2,1;3,10";
		itemCombo 			= allItemString.Split (';');
		panelCreator 		= GetComponent<createSelectPanel> ();
		itemsArray 			= new ArrayList();

		if (panelCreator == null) {
			panelCreatorMap = GetComponent<createSelectPanelMap> ();
		}


		for ( int x = 0; x < itemCombo.Length ; x++) {
			int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
			Item item 										= ItemCreator.createItem(x, itemAmount);
			GameObject temp 								= Instantiate(itemUI);
			temp.transform.parent 							= itemContainer.transform;
			temp.transform.localScale 						= Vector3.one;
			temp.transform.localPosition 					= Vector3.one;
			temp.GetComponentsInChildren<Text> ()[0].text 	= item.name;
			temp.GetComponentsInChildren<Text> ()[1].text 	= itemAmount+"x";
			temp.GetComponentInChildren<Button> ().onClick.AddListener (() => {


				if (panelCreator == null) {
					panelCreatorMap.create(item.id);
				}
				else{
					panelCreator.create(item.id);
				}
				

			});
			item.uiRef = temp;
			itemsArray.Add (item);


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
		}

	}


	public Item getItemById(int itemId){

		for (int x = 0; x < itemCombo.Length; x++) {
			if (((Item)itemsArray [x]).id == itemId)
				return (Item) itemsArray [x];
		}

		return new Item (0, "NULL", 0);
		}
}
