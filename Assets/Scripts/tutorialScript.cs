using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorialScript : MonoBehaviour {
	public Text tutorial;

	void Start(){
		Sound.LoadBgm ("main", "main");
		Sound.PlayBgm ("main");
	}

	public void changeTutorialText(string present,int second){
		tutorial.text = present;
		Invoke ("deleteText",second);
	}

	private void deleteText(){
		tutorial.text = "";
	}
}
