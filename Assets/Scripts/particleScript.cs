using UnityEngine;
using System.Collections;

public class particleScript : MonoBehaviour {
	public GameObject par;
	public GameObject parts;
	private bool partsBool;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (par != null) {
			gameObject.transform.position = par.transform.position;
		} else if(!partsBool){
			partsBool = true;
			Instantiate (parts, gameObject.transform.position,Quaternion.identity);
			Destroy (gameObject);
		}

	}
}
