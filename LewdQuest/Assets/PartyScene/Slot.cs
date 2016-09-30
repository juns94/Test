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
			Transform aux = DragHandler.itemDragged.transform.parent;
			DragHandler.itemDragged.transform.SetParent(transform); 
			item.transform.SetParent(aux);  
		}
	}

	#endregion
}
