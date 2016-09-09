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

	public GameObject damagePrefab;

	// Use this for initialization
	void Start () {
	
		image = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {



		if (lastHp > currentHp)
			CreateDamagePopup (lastHp-currentHp);

		float currentDamageDealt = totalHp - currentHp;
		lastHp = currentHp;
		//Debug.Log ("dealt" + dealt + " y total" + currentDamageDealt);
		if (currentDamageDealt > dealt) {


			image.transform.localScale = new Vector3 ((totalHp - dealt) / totalHp, 1, 1);
			dealt += 0.1f * factor;
			factor += 0.4f;
		} else {

			factor = 1;


		}

		if (((totalHp - dealt) / totalHp > 0) && ((totalHp - dealt) / totalHp) < 0.05) {
			Debug.Log (" TA MUERLTO ");
			image.transform.localScale = new Vector3 (0, 1, 1);
		}


		if (currentHp < 5) {

		//	blink ();
		}

	
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
		GameObject damageGameObject = (GameObject)Instantiate(damagePrefab,
			gameObject.transform.position,
			gameObject.transform.rotation);
		damageGameObject.transform.parent = gameObject.transform;
		damageGameObject.transform.localScale = Vector3.one;
		damageGameObject.GetComponentInChildren<Text>().text = damage.ToString();
	}



}
