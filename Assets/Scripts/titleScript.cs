using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour {
	public Text title;

	// Use this for initialization
	void Start () {
		Sound.LoadBgm ("main", "main");
		Sound.PlayBgm ("main");
		title.enabled = false;
		Invoke ("titleFunc", 32);
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown (KeyCode.Space)) {
			titleFunc ();
		}
	}

	private void titleFunc(){
		if (title.enabled) {
			SceneManager.LoadScene ("MAIN");
		}

		title.enabled = true;


	}
}
