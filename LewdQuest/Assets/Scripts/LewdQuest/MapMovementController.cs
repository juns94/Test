using UnityEngine;
using System.Collections;

public class MapMovementController : MonoBehaviour {

	GameObject go;
	Transform got;
	Rigidbody2D body;
	Transform lastPosition;
	public MapManager mapmanager;
	public MapUIManager uiMap;
	Vector3 mousePosition;
	bool moving = false;
	bool deactivated;
	private static float move = 2f;
	void Start () {
		go 		= gameObject;
		got 	= go.transform;
		body 	= GetComponent<Rigidbody2D> ();

		float posX = PlayerPrefs.GetFloat ("posX", 2f);
		float posY = PlayerPrefs.GetFloat ("posY", 2f);
		got.position = new Vector3 (posX, posY, 0);	

	}
	void Update () {
	
		if (!deactivated) {

			if (Input.GetKey (KeyCode.W)) {
				got.localPosition = new Vector3 (got.localPosition.x, got.localPosition.y + move);
				moving = false;
			}
			if (Input.GetKey (KeyCode.S)) {

				got.localPosition = new Vector3 (got.localPosition.x, got.localPosition.y - move);
				moving = false;
			}
			if (Input.GetKey (KeyCode.A)) {

				got.localPosition = new Vector3 (got.localPosition.x - move, got.localPosition.y);
				moving = false;
			}
			if (Input.GetKey (KeyCode.D)) {

				got.localPosition = new Vector3 (got.localPosition.x + move, got.localPosition.y);
				moving = false;
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				mapmanager.encounter ();
			}




			if (Input.GetMouseButtonDown (0) & Input.mousePosition.y > 100) {

				mousePosition = Input.mousePosition;
				moving = true;
			}

			if (moving) {
				got.position = Vector2.MoveTowards (got.position, Camera.main.ScreenToWorldPoint (mousePosition), 0.04f);
			} 

		}
	}


	void OnCollisionEnter2D(Collision2D coll) {


	}

	void OnTriggerEnter2D(Collider2D coll) {
		int type = int.Parse (coll.gameObject.name);
		if (type == 100) {
			uiMap.setRestButtonState (true);
		} else {
			uiMap.setExploreButtonState (true);
		}
		mapmanager.setCurrentRegionStepped(type);
		PlayerPrefs.SetFloat("posX", coll.transform.position.x);
		PlayerPrefs.SetFloat("posY", coll.transform.position.y);
		PlayerPrefs.Save ();

	}

	void OnTriggerExit2D(Collider2D coll) {
		mapmanager	.setCurrentRegionStepped(0);
		uiMap		.setExploreButtonState(false);
		uiMap		.setRestButtonState	(false);
	}

	public void saveCurrentPosition(){
		PlayerPrefs.SetFloat("posX", gameObject.transform.position.x);
		PlayerPrefs.SetFloat("posY", gameObject.transform.position.y);
		PlayerPrefs.Save ();
	}

	public void deactivate(bool state){
		deactivated = state;
	}


}
