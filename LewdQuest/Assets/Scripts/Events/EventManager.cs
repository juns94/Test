using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	Event currentEvent;
	GameObject mapManager;
	int region;


	public GameObject mainImage;
	public GameObject textPanel;
	public GameObject buttonPanel;


	public void REEEE(){

		Debug.Log ("REEEE");
	}
		
	void Start () {
		mapManager = GameObject.Find("MapManager");
		if (mapManager != null) {
			region = mapManager.GetComponent<MapManager> ().region;
		} else
			region = 2;


		currentEvent = createEvent (region);
		Debug.Log (currentEvent);
		currentEvent.buttonPanel 	= buttonPanel;
		currentEvent.textPanel		= textPanel;
		currentEvent.mainImage		= mainImage;
		currentEvent.prepareScene ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public Event createEvent(int region){

		switch (region) {
		case 1:
			{ /// FOREST
				break;
			}
		case 2:
			{ /// MOUNTAINS
				return gameObject.AddComponent<ChestEvent>();


			}

	




		}
		return null;

	}

}
