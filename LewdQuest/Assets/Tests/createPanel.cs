using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class createPanel : MonoBehaviour {



	public GameObject button;
	public GameObject panel;	
	public UIManagerScript manager;
	// Use this for initialization
	void Start () {

	//	button = GameObject.Find ("_popUpButton");
	//	panel  = GameObject.Find ("_popUpPanel");

		manager = GameObject.Find ("UIManager").GetComponent<UIManagerScript>();
		Debug.Log (" EL MANAGER ES " + manager);
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


	public void create(){




		ArrayList enemyMap = manager.getEnemyMap ();

		if (enemyMap.Count > 1 & aliveCount() > 1) {

			GameObject popUp = Instantiate (panel);
			popUp.transform.parent = gameObject.transform;
			popUp.transform.localScale = Vector3.one;
			popUp.transform.localPosition = new Vector3 (gameObject.transform.position.x + 170, gameObject.transform.position.y, 0);




			for (int x = 0; x < enemyMap.Count; x++) {
				Character character = ((Chara_UI_Map)enemyMap [x]).getCharacter ();

				if (character.getAlive ()) {
					GameObject buttonPopUp = Instantiate (button);
					buttonPopUp.transform.parent = popUp.transform;
					buttonPopUp.transform.localScale = Vector3.one;
					buttonPopUp.GetComponentInChildren<Text> ().text = character.getName ();
					buttonPopUp.transform.localPosition = Vector3.zero;
					buttonPopUp.name = "" + x;

					buttonPopUp.GetComponent<Button> ().onClick.AddListener (() => {
						Destroy (popUp);
						this.gameObject.GetComponent<Button> ().enabled = true;	
						manager.dealDamage (int.Parse (buttonPopUp.name));

					});
				}

			}

			GameObject cancelButton = Instantiate (button);
			cancelButton.transform.parent = popUp.transform;
			cancelButton.transform.localScale = Vector3.one;
			cancelButton.transform.localPosition = Vector3.zero;
			cancelButton.GetComponentInChildren<Text> ().text = "Cancel";
			cancelButton.GetComponentInChildren<Button> ().onClick.AddListener (() => {
				Destroy (popUp);
				this.gameObject.GetComponent<Button> ().enabled = true;
			});



			gameObject.GetComponent<Button> ().enabled = false;



		} else {
			manager.dealDamage(getLastAlivePosition());
		}
	}
}
