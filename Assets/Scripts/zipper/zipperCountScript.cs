using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class zipperCountScript : MonoBehaviour {
	public Image serializedZipper;
	private int numberOfTamedZipper;

	public void zipperCount(int i){
		numberOfTamedZipper += i;
		serializedZipper.fillAmount = (float)(0.33f * numberOfTamedZipper);
	}
}
