using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GiftShopManager : MonoBehaviour {

	ItemContainer itemContainer;
	ArrayList shopItems;
	public GameObject shopContainer;
	public GameObject itemUI;
	private Item currentItem;
	private int currentPrice;
	int timesTouched;

	public Image maidImage;
	public Text UIPrice;
	public Text UIName;
	public Text UIDescription;
	public Text UIGold;
	public DialogManager dialogManager;
	bool blushing;

	void Start () {
		if (PlayerPrefs.GetInt ("giftits", 0) == 1) {
			dialogManager.showBoxAutoHide (new string[]{ "Alisa: Oh... it's you... Welcome." });	 
			maidImage.sprite = Resources.Load <Sprite> ("maido_n");  
		} else {
			dialogManager.showBoxAutoHide (new string[]{ "Alisa: Welcome!" });	
		}
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
				gameObject.GetComponent<AudioSource> ().PlayOneShot(Resources.Load <AudioClip>("Sounds/money\t"));
			}
		} else {
			scold ();
		}
	}

	public void updateUI(){
		if (blushing)currentPrice = currentPrice * 2;
		UIName.text 		= currentItem.name;
		UIPrice.text 		= "Price: "+ currentPrice;
		UIDescription.text	= currentItem.description;
		UIGold.text		 	= "Gold: " +PlayerPrefs.GetInt ("gold", 0);


	}



	void AddItemsToShop(){
		int[] list  = new int[]{ 200, 201, 202, 203	, 204	,205 , 206};
		int[] PRICE = new int[]{ 100, 100, 100, 100	, 100	,150 , 569};


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


	public void talk(){

		if(PlayerPrefs.GetInt("giftits", 0) == 0){
			switch(Random.Range(0,3)){
			case 0: dialogManager.showBox (new string[] {"Alisa: Are you looking for a special gift for someone? " ," We've got something for everyone!"});break;
			case 1: dialogManager.showBox (new string[]{"Alisa: I personally recommend you the chocolates" , " I like chocolates... "});	break;
			case 2: dialogManager.showBox (new string[]{"Alisa: It's sometimes hard to live away from the capital" , "...but at least it isn't so uptight here though!"});	break;
			}
		}
		else{
			switch(Random.Range(0,3)){
		case 0: dialogManager.showBox (new string[] {"Alisa: Are you looking for a special gift for someone? " ," We've got something for everyone!"});break;
		case 1: dialogManager.showBox (new string[]{"Alisa: I personally recommend you the chocolates" , " I like chocolates... "});	break;
		case 2: dialogManager.showBox (new string[]{"Alisa: It's sometimes hard to live away from the capital" , "...but at least it isn't so uptight here though!"});	break;
		}





		}
		}


	public void scold(){

		dialogManager.showBox (new string[]{"Alisa: You haven't picked anything to buy, silly!" });
	}

	public void blush(){
		switch(timesTouched){
		case 0:
			dialogManager.showBox (new string[]{ "Alisa: Hey! Watch it!" });			break;
		case 1 :dialogManager.showBox (new string[]{"Alisa: Those are not for sale!" });break;
		case 2 :dialogManager.showBox (new string[]{"Alisa: I'm going to kick you out!" });break;
		case 3 :dialogManager.showBoxShake (new string[]{"Alisa: I am dead serious!!" });break;
		case 4:
			dialogManager.showBox (new string[]{ "Alisa: Prices just went up for you, how's that?" });
			blushing = true; 
			break;
		case 5 :dialogManager.showBoxShake (new string[]{"Alisa: UGhnn STOP! NOW!" });
			maidImage.sprite = Resources.Load <Sprite> ("maido_h");  
			break;
		case 6 :dialogManager.showBox (new string[]{"Alisa: <color=#ff00ffff> AAHnmn!~  </color>" });break;
		case 7:
			dialogManager.showBox (new string[]{ "Alisa: That's it, you're getting <b>OUT</b>. " });
			Invoke ("exitShop", 1.5f);
			PlayerPrefs.SetInt ("giftits", 1);
			PlayerPrefs.Save ();
			break;
		}

		timesTouched++;
	
	}


	public void exitShop(){

		SceneManager.LoadScene("GrandCityScene");

	}
}
