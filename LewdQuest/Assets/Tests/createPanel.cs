using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class createPanel : MonoBehaviour {



	public GameObject button;
	public GameObject panel;	
	// Use this for initialization
	void Start () {

	//	button = GameObject.Find ("_popUpButton");
	//	panel  = GameObject.Find ("_popUpPanel");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void create(){

		GameObject popUp = Instantiate (panel);
		popUp.transform.parent = gameObject.transform;
		popUp.transform.localScale = Vector3.one;
		popUp.transform.localPosition = new Vector3(gameObject.transform.position.x + 170, gameObject.transform.position.y,0);

		for (int x = 0; x < 3; x++) {
			GameObject buttonPopUp = Instantiate (button);
			buttonPopUp.transform.parent = popUp.transform;
			buttonPopUp.transform.localScale = Vector3.one;
			buttonPopUp.transform.localPosition = Vector3.zero;


			buttonPopUp.GetComponent<Button>().onClick.AddListener(() =>
				{
					Destroy(popUp);
					this.gameObject.GetComponent<Button>().enabled = true;	
				});

		}

		GameObject cancelButton = Instantiate (button);
		cancelButton.transform.parent = popUp.transform;
		cancelButton.transform.localScale = Vector3.one;
		cancelButton.transform.localPosition = Vector3.zero;
		cancelButton.GetComponentInChildren<Text>  ().text = "Cancel";
		cancelButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
			{
				Destroy(popUp);
				this.gameObject.GetComponent<Button>().enabled = true;
			});



		gameObject.GetComponent<Button> ().enabled = false;




	}
}
