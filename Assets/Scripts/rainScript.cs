using UnityEngine;
using System.Collections;

public class rainScript : MonoBehaviour {
	public GameObject RainPrefab;
	private bool rainBool = false;

	void OnTriggerEnter(Collider col){
		rainBool = !rainBool;
		RainPrefab.SetActive (rainBool);
	}
}
