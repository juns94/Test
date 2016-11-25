using UnityEngine;
using System.Collections;

public class AureliaScene:SceneHub {
	

	public AureliaScene(){
		count = 1;
		names = new string[]{ "Titfuck" };
		refuseText = " I refuse to do such disgusting things with you.";
		acceptText = " Understood, Master. " ;
	}

	public override Scene get( int pos ){


		switch (pos) {

		case 0:
			return titFuck();
			break;

		default:
			return titFuck();
		}

	}


	public Scene titFuck(){
		TextAsset asset =   Resources.Load <TextAsset> ("Text/_aurelia_titfuck");
		return new Scene ("TitFuck",asset.text ,"_aurelia_titfuck"

		);
	}
}
