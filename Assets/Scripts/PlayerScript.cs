using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript: MonoBehaviour {
	Animator animator;
	//playerIdentity
	private int playerHP = 10;
	public Image HPbar;public Image staminaBar;
	public Transform player_transform;
	private float v;private float h;private float run;
	private float stamina = 10;

	//food関連
	public GameObject food_prefab;
	private float throwSpeed = 1000;


	//parts関連
	private int gatheredParts = 0;
	private int clearNum = 4; //何個のパーツでクリアか


	void Start(){
		animator = gameObject.GetComponent<Animator> ();
	}

	// ゲームの1フレームごとに呼ばれるメソッド
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			Shot ();
		}

		v=Input.GetAxis("Vertical");
		h=Input.GetAxis("Horizontal");

		Sprinting ();


		if (gatheredParts == clearNum) {
			print ("sucess");
			SceneManager.LoadScene ("Clear");
		}
	}

	void FixedUpdate(){
		animator.SetFloat("Walk",v);
		animator.SetFloat("Turn",h);
		animator.SetFloat("Run",run);
	}


	void Damage(){
		playerHP--;
		HPbar.fillAmount -= 0.1f;
		if (playerHP <= 0) {
			SceneManager.LoadScene ("GameOver");
		}
	}

	private void Sprinting(){
		if (Input.GetKey(KeyCode.LeftShift) && stamina > 0){
			run=10;
			stamina -= 0.03f;
			staminaBar.fillAmount -= 0.003f;
		}
		else
		{
			stamina += 0.02f;
			staminaBar.fillAmount += 0.002f;
			run=0.0f;
		}
	}

	void Shot(){
		Vector3 pos = gameObject.transform.position + transform.TransformDirection(Vector3.forward) + new Vector3(0,2.3f,0);
		GameObject food = Instantiate (food_prefab, pos, Quaternion.identity) as GameObject;
	//	Rigidbody rigidbody = food.GetComponent<Rigidbody> ();

		Vector3 bom_speed = transform.TransformDirection(Vector3.forward)  * 7;
		bom_speed += Vector3.up * 10;	
		food.GetComponent< Rigidbody >().velocity = bom_speed;
	//	food.GetComponent< Rigidbody >().angularVelocity = Vector3.forward * 7;


	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//partsに触れたら回収
		if (hit.gameObject.tag == "Parts") {
			gatheredParts += 1;
			Destroy (hit.gameObject);
		}

		if (hit.gameObject.tag == "enemy") {
			Damage ();
		}
	}



}


