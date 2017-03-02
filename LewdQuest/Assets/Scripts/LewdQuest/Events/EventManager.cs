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
	public GameObject npcImage;
		
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
		currentEvent.npcImage 		= npcImage;
		currentEvent.prepareScene ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public Event createEvent(int region){

		switch (region) {
		case 1:
			{ /// FOREST

				switch (Random.Range (0, 2)) {

				case 0:
					if (!(PlayerPrefs.GetInt ("0", 0) > 0)) {
						return gameObject.AddComponent<AureliaEvent> ();
					} else {
						return gameObject.AddComponent<ForestEvent> ();
					}
				case 1:
					if (LewdUtilities.getPartyBitches ().Count > 0) {
						return gameObject.AddComponent<ForestObdEvent> ();
					} else
						return createEvent (region);
				//case 2:
				//	return gameObject.AddComponent<AssassinEvent> ();
				}
					
				break;
			}
		case 2:
			{ /// MOUNTAINS

				switch (Random.Range (0, 5)) {

				case 0:
					return gameObject.AddComponent<ChestEvent> ();
				case 1:
					return gameObject.AddComponent<ChestEvent> ();
				case 2:
					return gameObject.AddComponent<ChestEvent> ();
				case 3:
					if (!(PlayerPrefs.GetInt ("200", 0) > 0))
						return gameObject.AddComponent<AssassinEvent> ();
					else
						return createEvent (region);
				case 4:
					return gameObject.AddComponent<ChestEvent> ();
				}
				break;
			}


		case 3:
			{ /// PLAINS

				switch (Random.Range (0, 2)) {

				case 0:
					if (PlayerPrefs.GetInt ("btnLake!", 0) != 1)
						return gameObject.AddComponent<DiscoverLakeEvent> ();
					else
						return createEvent (region);
					break;

				case 1:
					return gameObject.AddComponent<RunningEvent> ();
				case 2:
					return gameObject.AddComponent<AssassinEvent> ();
				case 3:
					return gameObject.AddComponent<AssassinEvent> ();
				case 4:
					return gameObject.AddComponent<ChestEvent> ();
				}
				break;
			}


		case 201:
			{	
				return gameObject.AddComponent<AugustusEvent> ();
				break;
			}

	



		}
		return null;

	}

	public ItemManager getItemManager(){
		return itemManager;
	}

}
