using UnityEngine;
using System.Collections;

public class foodScript : MonoBehaviour {
	private PlayerScript playerScript;


	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("Player").GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 物にさわった時に呼ばれる
	void OnCollisionEnter(Collision col) {
		// もしPlayerにさわったら


		if (col.gameObject.tag == "terrain") {
			Destroy (this.gameObject,2);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Zipper") {
			col.gameObject.GetComponent<zipperScript> ().eatFood (playerScript.foodNum);
				
			Destroy (this.gameObject);
		}


	}


}
