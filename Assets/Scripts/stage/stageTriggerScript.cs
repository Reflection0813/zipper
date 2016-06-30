using UnityEngine;
using System.Collections;

public class stageTriggerScript : MonoBehaviour {
	public GameObject RainPrefab;
	private bool forestBool = false;
	private bool desertBool = false;
	private bool snowBool = false;
	tutorialScript tutorialScript;

	// Use this for initialization
	void Start () {
		Sound.LoadBgm ("forest", "theme_forest");
		Sound.LoadBgm ("desert", "theme_desert");
		Sound.LoadBgm ("snow", "theme_snow");

		tutorialScript = GameObject.Find ("GameManager").GetComponent<tutorialScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RedirectedOnTriggerEnter(Collider col){
		print ("enter" + col.gameObject.tag);
		if (col.gameObject.tag == "Player") {
			if (gameObject.transform.parent.tag == "forest") {
				tutorialScript.changeTutorialText ("きのこの森", 3);
				forestBool = !forestBool;
				print (forestBool);
				RainPrefab.SetActive (forestBool);
				if (forestBool) {
					Sound.PlayBgm ("forest");
				} else {
					Sound.PlayBgm ("main");
				}


			}

			if (gameObject.transform.parent.tag == "Desert") {
				desertBool = !desertBool;
				tutorialScript.changeTutorialText ("砂漠", 3);
				if (desertBool) {
					
					Sound.PlayBgm ("desert");
				} else {
					Sound.PlayBgm ("main");
				}

			}

			if (gameObject.transform.parent.tag == "Snow") {
				snowBool = !snowBool;
				tutorialScript.changeTutorialText ("スノーマウンテン", 3);
				if (snowBool) {
					Sound.PlayBgm ("snow");
				} else {
					Sound.PlayBgm ("main");
				}

			}

		}
	}
}
