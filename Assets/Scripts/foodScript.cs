using UnityEngine;
using System.Collections;

public class foodScript : MonoBehaviour {
	private PlayerScript playerScript;


	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("Player").GetComponent<PlayerScript> ();

		/*
		if (gameObject.tag == "food") {
			GameObject[] zippers = GameObject.FindGameObjectsWithTag ("Zipper");
			print (gameObject.transform.position);
			for (var i = 0; i < zippers.Length; i++) {
				
				zippers [i].GetComponent<zipperScript> ().serchForFood (gameObject.transform.position);
			}
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {



	}

	// 物にさわった時に呼ばれる
	void OnCollisionEnter(Collision col) {
		

		//置く用以外は落ちると消滅
		if (col.gameObject.tag == "terrain" && gameObject.tag != "item1" && gameObject.tag != "item2") {
			Destroy (this.gameObject,2);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Zipper" && gameObject.tag == "food") {
			col.gameObject.GetComponent<zipperScript> ().eatFood (playerScript.foodNum);
				
			Destroy (this.gameObject);
		}


	}


}
