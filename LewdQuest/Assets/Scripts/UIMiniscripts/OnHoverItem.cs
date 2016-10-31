using UnityEngine;
using System.Collections;

public class OnHoverItem : MonoBehaviour {

	// Use this for initialization
	GameObject canvas;
	float y  = 0;
	string text = "";
	bool show = false;
	void Start () {
	
	
		//canvas = GameObject.Find ("Canvas");
	//	GetComponent<Canvas> ();
	}


	// Update is called once per frame
	void Update () {
	
	}

	public void displayText( string text  ){
		this.text = text;
		show = true;
	}

	public void hideText(){
		show = false;

	}

	void OnGUI(){


	/*	Vector2 pos = Vector2.one;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (
			canvas.GetComponent<RectTransform> (), Input.mousePosition, Camera.main, out pos);
*/
		if (show) {
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			//Camera.main.scr

			Vector3 temp = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 1));
			GUI.TextArea (new Rect (new Vector2 (x+8, Screen.height - y), new Vector2 (150, 100)), text);
		}

	}
}
