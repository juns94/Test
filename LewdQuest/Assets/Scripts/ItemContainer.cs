using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour {

	private string	 	allItemString;
	private string[]	itemCombo;
	private int[] 		itemArray;
	private ArrayList 	itemsArray;


	void Start () {

		allItemString	 	= "0,4;1,2;2,1;3,10";
		itemsArray 			= new ArrayList();
		allItemString 		= PlayerPrefs.GetString ("items","");
		itemCombo 			= allItemString.Split (';');




		for ( int x = 0; x < itemCombo.Length ; x++) {
			//	Debug.Log (" intento parsear :" + itemCombo [x].Split (',') [0]);
			if((itemCombo[x])!= ""){
				int id	 										= int.Parse(itemCombo [x].Split (',') [0]);
				int itemAmount 									= int.Parse(itemCombo [x].Split (',') [1]);
				Item item 										= ItemCreator.createItem(id, itemAmount);
				itemsArray.Add (item);

			}

		}



	}


	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			PlayerPrefs.SetString("items","0,4;1,2;2,1;3,10")	;
			PlayerPrefs.Save ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Destroy (GameObject.Find ("MapManager"));
			SceneManager.LoadScene ("MapScene");
		}

	}


	public void removeItem (int itemId){
		Item item = getItemById (itemId);
		item.amount -= 1;
		if (item.amount == 0) {
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

	public ArrayList getItems(){
		return itemsArray;
	}
}
