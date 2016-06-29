using UnityEngine;
using System.Collections;

public class switchScript : MonoBehaviour {
	public GameObject[] floors;


	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			for (var i = 0; i < 5; i++) {
				floors [i].GetComponent<Animator> ().SetBool ("switch", true);
			}
		}
	}
}
