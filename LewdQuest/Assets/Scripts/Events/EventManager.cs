using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	Event currentEvent;
	GameObject mapManager;
	int region;


	public GameObject mainImage;
	public GameObject textPanel;
	public GameObject buttonPanel;
	public GameObject dialogPanel;
	public ItemManager itemManager;
	public GameObject scriptedFight;

		
	void Start () {
		mapManager = GameObject.Find("MapManager");
		if (mapManager != null) {
			region = mapManager.GetComponent<MapManager> ().region;
		} else
			region = 1;


		currentEvent = createEvent (region);
		Debug.Log (currentEvent);
		currentEvent.buttonPanel 	= buttonPanel;
		currentEvent.textPanel		= textPanel;
		currentEvent.mainImage		= mainImage;
		currentEvent.dialogPanel	= dialogPanel;
		currentEvent.itemManager 	= itemManager;
		currentEvent.scriptFight 	= scriptedFight;
		currentEvent.prepareScene ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public Event createEvent(int region){

		switch (region) {
		case 1:
			{ /// FOREST




				if (!(PlayerPrefs.GetInt ("0", 0) > 0)) {
					return gameObject.AddComponent<AureliaEvent> ();
				} else {
					return gameObject.AddComponent<ForestEvent> ();
				}
				break;

			}
		case 2:
			{ /// MOUNTAINS
				return gameObject.AddComponent<ChestEvent>();


			}

	




		}
		return null;

	}

	public ItemManager getItemManager(){
		return itemManager;
	}

}
