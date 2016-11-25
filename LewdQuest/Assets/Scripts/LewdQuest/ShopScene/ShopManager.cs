using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

	ItemContainer itemContainer;
	ArrayList shopItems;
	public GameObject shopContainer;
	public GameObject itemUI;
	private Item currentItem;
	private int currentPrice;

	public Text UIPrice;
	public Text UIName;
	public Text UIDescription;
	public Text UIGold;

	void Start () {
	

		itemContainer = this.GetComponent<ItemContainer> ();
		AddItemsToShop ();
		UIGold.text		 	= "Gold: " +PlayerPrefs.GetInt ("gold", 0);
	}
	
	// Update is called once per frame
	void Update () {
	




	}

	public void attemptBuy(){

		if (currentItem != null) {
			int currentGold = PlayerPrefs.GetInt ("gold", 0);
			if (currentPrice <= currentGold) {
				itemContainer.AddItemToInventory (currentItem.id, 1);
				PlayerPrefs.SetInt ("gold", currentGold - currentPrice);
				PlayerPrefs.Save ();
				updateUI ();
			}
		}

	}

	public void updateUI(){

		UIName.text 		= currentItem.name;
		UIPrice.text 		= "Price: "+ currentPrice;
		UIDescription.text	= currentItem.description;
		UIGold.text		 	= "Gold: " +PlayerPrefs.GetInt ("gold", 0);
	}



	void AddItemsToShop(){
		int[] list  = new int[]{ 100, 101, 102, 0	,	1	,2 ,	4};
		int[] PRICE = new int[]{ 100, 100, 100, 100	,  300	,500 , 150};


		for (int x = 0; x < list.Length; x++) {
			int price 										= PRICE [x];
			Item item 										= ItemCreator.createItem(list[x], 1);
			GameObject temp 								= Instantiate(itemUI);
			temp.transform.parent 							= shopContainer.transform;
			temp.transform.localScale 						= Vector3.one;
			temp.transform.localPosition 					= Vector3.one;
			temp.GetComponentsInChildren<Text> ()[0].text 	= item.name;
			temp.GetComponentsInChildren<Text> ()[1].text   = PRICE [x] + "g";
			temp.GetComponentInChildren<Button>().onClick.AddListener (() => {
				currentPrice= price;
				currentItem = item;
				updateUI();
			});

		}




	}


	public void exitShop(){

		SceneManager.LoadScene("GrandCityScene");

	}
}
