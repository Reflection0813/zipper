using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class zipperCountScript : MonoBehaviour {
	public Image[] serializedZipper;
	private Image HPbar;
	public int numberOfTamedZipper;


	public void zipperCount(int i){
		if (numberOfTamedZipper < 10) {
			numberOfTamedZipper += i;
		}
		if (0 < numberOfTamedZipper && numberOfTamedZipper <= 3) {
			serializedZipper[0].fillAmount = (float)(0.33f * numberOfTamedZipper);
			serializedZipper [1].fillAmount = 0;//4から3になったときに4を消す
		}else if( 3 < numberOfTamedZipper && numberOfTamedZipper <= 6){
			serializedZipper[1].fillAmount = (float)(0.33f * (numberOfTamedZipper - 3));
			serializedZipper [2].fillAmount = 0;//7から6になったときに7を消す
		}else if( 7 < numberOfTamedZipper && numberOfTamedZipper <= 9){
			serializedZipper[2].fillAmount = (float)(0.33f * (numberOfTamedZipper - 6));
		}
	}
}
