using UnityEngine;
using System.Collections;

public class switchManagerScript : MonoBehaviour {
	private int count = 0;
	public GameObject[] floors;

	// Use this for initialization
	void Start () {
		Sound.LoadSe ("incorrect", "incorrect");
		Sound.LoadSe ("correct", "correct");
		Sound.LoadSe ("small_correct", "correct2");
	}
	
	public void nazotoki(string tagname){
		print (tagname);
		if (count == 0) {
			if (tagname == "zone_r") {
				count += 1;
				print (count);
				Sound.PlaySe("small_correct");

			}else{
				Sound.PlaySe ("incorrect");
				count = 0;
			}
		}else if (count == 1) {
			if (tagname == "zone_g") {
				count += 1;
				Sound.PlaySe("small_correct");
				print (count);
			} else {
				Sound.PlaySe ("incorrect");
				count = 0;
			}
		}else if (count == 2) {
			if (tagname == "zone_b") {
				Sound.PlaySe ("correct");
				print (count);
				for (var i = 0; i < 5; i++) {
					floors [i].GetComponent<Animator> ().SetBool ("switch", true);
				}
			} else {
				Sound.PlaySe ("incorrect");
				count = 0;
			}
		}



	}
}
