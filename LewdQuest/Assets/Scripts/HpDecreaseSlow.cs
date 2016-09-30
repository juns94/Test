using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HpDecreaseSlow : MonoBehaviour {


	Image image;
	float currentHp;
	float totalHp;
	float dealt;
	float factor = 1;
	float blinkTime = 10;
	float lastHp;
	Transform initialPosition;

	public GameObject damagePrefab;

	// Use this for initialization
	void Start () {
		initialPosition = gameObject.transform;
		image = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

//		image.transform.localScale = new Vector3 (currentHp / totalHp, 1, 1);
//		/*
		if (lastHp > currentHp) {	CreateDamagePopup (lastHp - currentHp);}

		float currentDamageDealt = totalHp - currentHp;
		lastHp = currentHp;


		if (currentDamageDealt > dealt) {
			image.transform.localScale = new Vector3 ((totalHp - dealt) / totalHp, 1, 1);
			dealt += 0.1f * factor;
			factor += 0.4f;
		} else {
			factor = 1;
			if (currentDamageDealt < dealt) {
				image.transform.localScale = new Vector3 (currentHp / totalHp, 1, 1);
			}
		}
	


















		/*
		float difference = 0 ;
		if (lastHp > currentHp) {
			CreateDamagePopup (lastHp - currentHp);
			difference = currentHp - totalHp ;
		} else {
			if (lastHp < currentHp) {
				difference = totalHp - currentHp;
			}
		}
			
		if(difference != 0 & (difference < 100))Debug.Log (difference);
		float target = totalHp - difference;
		float currentDamageDealt = totalHp - currentHp;

		if (currentDamageDealt > dealt) {
			image.transform.localScale = new Vector3 ((totalHp - dealt) / totalHp, 1, 1);
			dealt += 0.1f* factor;
			factor += 0.2f;
		} else {
			factor = 1;

			if (currentDamageDealt < dealt) {
				image.transform.localScale = new Vector3 (currentHp / totalHp, 1, 1);
			}
	
		}*/

	
	}

	public void setDamage(float currentHp,  float totalHp){
		this.currentHp = currentHp;
		this.totalHp = totalHp;
	}


	private void blink(){

		if (blinkTime < 0) {
			image.CrossFadeAlpha(0,0.01f,false);
			blinkTime = 100;
		} else {
			image.CrossFadeAlpha(1,0.01f,false);
			blinkTime--;
		}



	}


	public void CreateDamagePopup(float damage){
		GameObject damageGameObject = (GameObject)Instantiate(damagePrefab,initialPosition.position,initialPosition.rotation);
		damageGameObject.transform.parent = gameObject.transform.parent.transform;
		damageGameObject.transform.localScale = Vector3.one;
		damageGameObject.GetComponentInChildren<Text>().text = damage.ToString();
	}



}
