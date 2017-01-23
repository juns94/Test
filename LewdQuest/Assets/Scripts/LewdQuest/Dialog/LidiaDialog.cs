using UnityEngine;
using System.Collections;

public class LidiaDialog : DialogHub {
	public LidiaDialog(){
		this.defaultReplies = new string[] {
			" Stop resisting! I'm trying to stab you!" ,
			" Didn't your mother teach you not to go alone in the forest?" ,
			" Hand over the purse of coins already."
		};
		this.initialDialog = "Give up your gold idiot or I'm gonna make your face even uglier!";
	
	}

}
