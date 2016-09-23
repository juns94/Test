using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnHover : MonoBehaviour {
//	public CanvasRenderer rend;
	private Outline outline;
	void Start() {
		//rend = GetComponent<CanvasRenderer>();
		outline = GetComponentInChildren<Outline> ();
	}
	public void OnMouseEnter() {

		if(outline != null)
		outline.enabled = true;

		//Debug.Log (" ENTRO ");
		//rend.GetMaterial().color = Color.red;
	}
	public void OnMouseOver() {
		//rend.GetMaterial().color -= new Color(0.1F, 0, 0) * Time.deltaTime;
	}
	public  void OnMouseExit() {
		if(outline != null)
		outline.enabled = false;
		//Debug.Log (" SALIO ");
		//rend.GetMaterial().color = Color.white;
	}
}