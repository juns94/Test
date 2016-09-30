using UnityEngine;
using System.Collections;

public class OrbScene {

	public Scene get( int pos ){


		switch (pos) {

		case 0:
			return orbFuck();
			break;

		default:
			return orbFuck();
		}

	}


	public Scene orbFuck(){


		return new Scene ("Molest",
			"You fuck that orb.|God you're sick but love it.|One way to hell with orb sex.| You leave.",
			"_orb_molest"

		);

	}
}
