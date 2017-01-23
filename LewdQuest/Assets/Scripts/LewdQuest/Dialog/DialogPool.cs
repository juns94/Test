using UnityEngine;
using System.Collections;

public class DialogPool {
	public static DialogHub get(int id){


		switch (id) {

		//case 0: return
		case 2: return new LidiaDialog();
		
		
		
		
		case 31: return new ManaBlobDialog();
		default:
			return new DefaultDialog();

		}






	}
}
