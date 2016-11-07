using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IEndDragHandler, IBeginDragHandler, IDragHandler {
	

	public static GameObject itemDragged;
	Vector3 startPos;
	Vector3 screenPos;
	Transform startParent;



	void Start () {
	
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
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

		}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemDragged = null;
		if (transform.parent == startParent) {
			transform.position = startPos;
		}
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		Vector3 screenPoint = Input.mousePosition; screenPoint.z = 40.0f; //distance of the plane from the camera 
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
	//	screenPos = Input.mousePosition; screenPos.z = 1f; //distance of the plane from the camera 
	//	transform.position = Camera.main.ScreenToWorldPoint(screenPos);
	//	Debug.Log ("OnDRAG");
		//transform.position = GetComponentInParent<Canvas> ().worldCamera.ScreenToWorldPoint(eventData.position);
	//	transform.position = Camera.main.ViewportToScreenPoint(eventData.position);
	}
	#endregion
}
