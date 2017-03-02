using UnityEngine;
using System.Collections;

public abstract class DialogHub{

	public string initialDialog{ get; set;}
	public string[] defaultReplies;
	int counter = -1;


	public string getRandom(){
		if(defaultReplies != null)
		return defaultReplies [Random.Range(0,defaultReplies.Length)];
		return null;
	}

	public string getSequential(){
		if (defaultReplies != null) {
			if (counter <= defaultReplies.Length) {
				counter++;
				return defaultReplies [counter];
			}
		}
		return null;

	}

}
