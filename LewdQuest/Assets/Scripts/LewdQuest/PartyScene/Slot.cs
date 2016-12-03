using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler{
	public GameObject item{
		get{
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if(DragHandler.itemDragged != null)
		if (!item) {
			DragHandler.itemDragged.transform.SetParent (transform);

			if (transform.parent.name.Equals ("PartyBench")) {
				Debug.Log (" TIRO EL " + DragHandler.itemDragged.name + " EN EL PartyBench");
				PlayerPrefs.SetInt (DragHandler.itemDragged.name, 2);
				PlayerPrefs.Save ();
			}else
			if (transform.parent.name.Equals ("Bench")) {
				Debug.Log (" TIRO EL " + DragHandler.itemDragged.name + " EN EL Bench");
				PlayerPrefs.SetInt (DragHandler.itemDragged.name, 1);
				PlayerPrefs.Save ();
			}


		} else {

			Debug.Log ("SWAPPED " + item.transform.name + " con  " + DragHandler.itemDragged.name);
			int itemState = PlayerPrefs.GetInt (item.transform.name, 0);
			PlayerPrefs.SetInt (item.transform.name, PlayerPrefs.GetInt(DragHandler.itemDragged.name,0));
			PlayerPrefs.SetInt (DragHandler.itemDragged.name, itemState);
			Transform aux = DragHandler.startParent;
			DragHandler.itemDragged.transform.SetParent(transform); 
			item.transform.SetParent(aux);  
		}
	}

	#endregion
}
