using UnityEngine;
using System.Collections;

public class triggerChildScript : MonoBehaviour {
	GameObject parent;

	void Start () {
		
		parent = gameObject.transform.parent.gameObject;
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player"){
			if (gameObject.tag == "area") {
				parent.SendMessage ("RedirectedOnTriggerEnter", collider);
			} else {
				parent.GetComponent<switchManagerScript> ().nazotoki (gameObject.tag);
			}
		}
	}

}
