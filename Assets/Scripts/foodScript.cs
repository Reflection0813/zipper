using UnityEngine;
using System.Collections;

public class foodScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 物にさわった時に呼ばれる
	void OnCollisionEnter(Collision col) {
		// もしPlayerにさわったら


		if (col.gameObject.tag == "terrain") {
			Debug.Log ("地面");
			//Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Zipper") {
			col.GetComponent<Collider>().SendMessage ("beFat");
			Destroy (this.gameObject);
		}


	}


}
