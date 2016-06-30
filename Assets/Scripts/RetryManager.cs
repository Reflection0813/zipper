using UnityEngine;
using System.Collections;

public class RetryManager : MonoBehaviour {

	void Update(){
		// スペースキーを押したら
		if (Input.GetKey (KeyCode.Space)) {
			Retry (); //リトライ
		}
	}

	// リトライメソッド
	public void Retry(){
		Application.LoadLevel("TITLE");
	}
}
