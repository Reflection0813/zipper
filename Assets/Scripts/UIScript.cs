using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {
	public Image[] foodUI;
	public Text[] foodTX;

	// Use this for initialization
	public void changeFoodUI(int foodNum){
		switch (foodNum) {
		case 0:
			foodUI [0].color = new Color (255, 255, 255);foodUI [1].color = new Color (0, 0, 0);foodUI [2].color = new Color (0, 0, 0);
			break;
		case 1:
			foodUI [1].color = new Color (255, 255, 255);foodUI [2].color = new Color (0, 0, 0);foodUI [0].color = new Color (0, 0, 0);
			break;
		case 2:
			foodUI [2].color = new Color (255, 255, 255);foodUI [0].color = new Color (0, 0, 0);foodUI [1].color = new Color (0, 0, 0);
			break;
			
			
		}

	}

	public void changeFoodTX(int foodNum,int foodLeft){
		foodTX [foodNum].text = foodLeft.ToString ();

	}
}
