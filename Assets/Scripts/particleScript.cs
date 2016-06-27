using UnityEngine;
using System.Collections;

public class particleScript : MonoBehaviour {
	public GameObject par;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = par.transform.position;
	}
}
