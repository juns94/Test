using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour,IEndDragHandler, IBeginDragHandler, IDragHandler {
	

	public static GameObject itemDragged;
	Vector3 startPos;
	Vector3 screenPos;
	Vector3 startPosition;
	public static Transform startParent;
	Transform canvas;

	bool start=true;

	void Start () {
		canvas = GameObject.Find ("PartyCanvas").transform;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemDragged = gameObject;
		startPos = transform.position;
		startParent = transform.parent;
		startPosition = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		//GetComponent<LayoutElement>().ignoreLayout = true;
		itemDragged.transform.SetParent(canvas);
		}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemDragged = null;
		if (transform.parent == startParent || transform.parent == canvas) {
			transform.position = startPos;
			transform.SetParent(startParent);
		}
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	//	GetComponent<LayoutElement>().ignoreLayout = false;
	}

	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		Vector3 screenPoint = Input.mousePosition; screenPoint.z = 40.0f; //distance of the plane from the camera 
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
	//	transform.position = new Vector3 (transform.position.x,transform.position.y, 	400);
	//	screenPos = Input.mousePosition; screenPos.z = 1f; //distance of the plane from the camera 
	//	transform.position = Camera.main.ScreenToWorldPoint(screenPos);
	//	Debug.Log ("OnDRAG");
		//transform.position = GetComponentInParent<Canvas> ().worldCamera.ScreenToWorldPoint(eventData.position);
	//	transform.position = Camera.main.ViewportToScreenPoint(eventData.position);
	
	
	}
	#endregion
}
