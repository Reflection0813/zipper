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
		if (col.gameObject.tag == "Zipper") {
			print ("aaaa");
			col.collider.SendMessage ("beFat");
			//col.SendMessage ("beFat"); //ダメージを与えて
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "terrain") {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Zipper") {
			col.SendMessage ("beFat");
			Destroy (this.gameObject,1);
		}

	}
}
