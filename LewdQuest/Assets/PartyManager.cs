using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PartyManager : MonoBehaviour {


	GameObject partyBench;
	GameObject bench;
	ArrayList bitches;
	ArrayList partyBitches;
	public GameObject miniSlot;
	public GameObject slot;
	public GameObject item;
	private GameObject detailPanel;
	void Start () {
		partyBench 		= GameObject.Find ("PartyBench");	
		bench 			= GameObject.Find ("Bench");
		bitches 		= getBitches();
		partyBitches 	= getPartyBitches ();
		detailPanel 	= GameObject.Find ("DetailPanel");

		for (int x = 0; x < 3; x++) {
			GameObject go;
			go = Instantiate (slot);
			go.transform.parent = bench.transform;
			go.transform.localScale = Vector3.one;
		
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

		for (int x = 0; x < 4 ; x++) {
			GameObject go;
			go = Instantiate (miniSlot);
			go.transform.parent = partyBench.transform;
			go.transform.localScale = Vector3.one;

			int totalPartyBitches = partyBitches.Count;
			if ((x+1) <= totalPartyBitches) {
				
				Character character = ((Character)partyBitches [x]);
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
		for (int x = 0; x < 100; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 1) {
				Debug.Log (" encontro al id " + x);
				temp.Add(EnemyCreator.createOwned(x));
			}
		}
		return temp;
	}

	public ArrayList getPartyBitches(){
		ArrayList temp = new ArrayList ();
		for (int x = 0; x < 100; x++) {
			if (PlayerPrefs.GetInt (x + "", 0) == 2) {
				temp.Add(EnemyCreator.createOwned (x));
			}
		}
		return temp;
	}

	public void setDetails(Character character){

		Text[] details;
		detailPanel.GetComponentsInChildren<Image>()[1].sprite = Resources.Load <Sprite> (character.image);
		details = detailPanel.GetComponentsInChildren<Text> ();


		details [0].text = "Name: "+character.name;
		details [1].text = "Obedience: "+character.obedience;
		details [2].text = "Horny: "+character.horny;
		details [3].text = "HP: "+character.totalHP;

	}


	public void goToMapScene(){

		SceneManager.LoadScene ("MapScene");

	}
}
