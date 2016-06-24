using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript: MonoBehaviour {
	private int playerHP = 10;
	public Transform player_transform;
	public Image HPbar;
	public GameObject food_prefab;
	private float throwSpeed = 1000;


	// ゲームの1フレームごとに呼ばれるメソッド
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			Shot ();
		}

	}

	// ダメージを与えられた時に行いたい命令を書く
	void Damage(){
		playerHP--;
		HPbar.fillAmount -= 0.1f;
		if (playerHP <= 0) {
			SceneManager.LoadScene ("GameOver");
		}
	}

	void Shot(){
		Vector3 pos = gameObject.transform.position + transform.TransformDirection(Vector3.forward);
		GameObject food = Instantiate (food_prefab, pos, Quaternion.identity) as GameObject;
	//	Rigidbody rigidbody = food.GetComponent<Rigidbody> ();

		Vector3 bom_speed = transform.TransformDirection(Vector3.forward)  * 7;
		bom_speed += Vector3.up * 10;	
		food.GetComponent< Rigidbody >().velocity = bom_speed;
		food.GetComponent< Rigidbody >().angularVelocity = Vector3.forward * 7;


	}
	}


