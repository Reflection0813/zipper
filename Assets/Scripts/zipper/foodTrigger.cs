using UnityEngine;
using System.Collections;

public class foodTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "food") {
			gameObject.transform.parent.gameObject.GetComponent<zipperScript> ().searchForFood (col.gameObject);
		}
	}
}
