using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PartyManager : MonoBehaviour {


	ArrayList bitches;
	ArrayList partyBitches;
	GameData gameData;
	public GameObject miniSlot;
	public GameObject slot;
	public GameObject item;
	public GameObject detailPanel;
	public GameObject partyBench;
	public GameObject buffBench;
	public GameObject bench;
	public InteractionContainer interactionManager; 

	void Start () {

		gameData 		= LewdUtilities.getGameData (GameObject.Find ("GameData"));
		bitches 		= getBitches();
		partyBitches 	= getPartyBitches ();/*
		detailPanel 	= GameObject.Find ("DetailPanel");
		partyBench 		= GameObject.Find ("PartyBench");	
		bench 			= GameObject.Find ("Bench");*/
		for (int x = 0; x < 4; x++) {
			GameObject go;
			go = Instantiate (slot);
			go.transform.parent = bench.transform;
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = Vector3.one;
			int totalBenchBitches = bitches.Count;
			if ((x + 1) <= totalBenchBitches) {
				Character character = ((Character) bitches [x]);
				GameObject slotItem = Instantiate (item);

				Debug.Log ((character == null) + " Es null ? ");
				slotItem.name = character.id + "";
				slotItem.GetComponentInChildren<Text> ().text = character.name;
				slotItem.transform.parent = go.transform;
				slotItem.transform.localScale = Vector3.one;
				slotItem.GetComponentInChildren<Button> ().onClick.AddListener (() => {
					setDetails (character);
					detailPanel.SetActive (true);
				});
			}
		}

		/*
		for (int x = 0; x < 3; x++) { /////////BUFFBENCH
			GameObject go;
			go = Instantiate (miniSlot);
			go.transform.parent = buffBench.transform;
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = Vector3.one;


		}*/


		for (int x = 0; x < 1 ; x++) {
			GameObject go;
			go = Instantiate (miniSlot);
			go.transform.parent = partyBench.transform;
			go.transform.localScale = Vector3.one;

			int totalPartyBitches = partyBitches.Count;
			if ((x+1) <= totalPartyBitches) {
				
				Character character = gameData.getCharacterById (((Character)partyBitches[x]).id);
				GameObject slotItem = Instantiate (item);
				slotItem.name = character.id + "";
				slotItem.GetComponentInChildren<Text> ().text = character.name;
				slotItem.transform.parent = go.transform;
				slotItem.transform.localScale = Vector3.one;
				slotItem.GetComponentInChildren<Button> ().onClick.AddListener (() => {
					setDetails (character);
					detailPanel.SetActive (true);
				});
			}

		}

		detailPanel.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public ArrayList getBitches(){
		ArrayList temp = new ArrayList ();
		for (int x = 0; x < 220; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 1) {
				//Debug.Log (" encontro al id " + x);
				temp.Add(EnemyCreator.create(0,x));
			}
		}
		return temp;
	}

	public ArrayList getPartyBitches(){
		ArrayList temp = new ArrayList ();
		for (int x = 0; x < 201; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 2) {
				temp.Add(EnemyCreator.create (0,x));
			}
		}
		return temp;
	}

	public void setDetails(Character character){
		interactionManager.setId(character.id);
		Text[] details;
		detailPanel.GetComponentsInChildren<Image>()[1].sprite = Resources.Load <Sprite> (character.image);
		details = detailPanel.GetComponentsInChildren<Text> ();


		details [0].text = "Name: "+character.name;
		details [1].text = "Obedience: "+character.obedience;
		details [2].text = "Horny: "+character.horny;
		details [3].text = "Love: " + character.love;
		details [4].text = "HP: "+ character.hp + "/" +character.totalHP;


	}


	public void goToMapScene(){
		Destroy( GameObject.Find("MapManager"));
		SceneManager.LoadScene ("MapScene");

	}

	public void goToBuddyStats(){
		Destroy( GameObject.Find("MapManager"));
		SceneManager.LoadScene ("StatsBuddyScene");

	}

}
