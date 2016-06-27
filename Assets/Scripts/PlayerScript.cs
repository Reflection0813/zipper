using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript: MonoBehaviour {
	Animator animator;
	//playerIdentity
	private int playerHP = 3;
	public Image HPbar;public Image staminaBar;
	public Transform player_transform;
	private float v;private float h;private float run;
	private float stamina = 10;
	private int blinkCount = 0;private bool damagedBool = false;

	//food関連
	public GameObject[] foodPrefab;
	public Text[] foodLeftText;
	public int foodNum = 0;
	private float throwSpeed = 1000;
	private int[] foodLeft = new int[3];




	//parts関連
	private int gatheredParts = 0;
	private int clearNum = 4; //何個のパーツでクリアか


	void Start(){
		animator = gameObject.GetComponent<Animator> ();

		//foodの初期値
		foodLeft[0] = 10;foodLeft[1]=10;foodLeft[2]=10;
		for (var i = 0; i < 3; i++) {
			foodLeftText [i].text = foodLeft [i].ToString ();
		}
	}

	// ゲームの1フレームごとに呼ばれるメソッド
	void Update () {
		

		
		//Zで投げてXで食べてCで変える
		if (Input.GetKeyDown(KeyCode.Z)) {
			throwFood ();
		}

		if(Input.GetKeyDown(KeyCode.X)){
			//eat();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			changeFoodNum ();
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
		if (!damagedBool) {
			damagedBool = true;
			blinkCount = 0;
			StartCoroutine (Blink ());
			playerHP--;
			HPbar.fillAmount -= 0.33f;
			if (playerHP <= 0) {
				SceneManager.LoadScene ("GameOver");
			}
		}
	}
	//点滅
	IEnumerator Blink(){
		while (blinkCount < 30) {
			GameObject.Find ("Player/character").GetComponent<SkinnedMeshRenderer> ().enabled = !GameObject.Find ("Player/character").GetComponent<SkinnedMeshRenderer> ().enabled;
			blinkCount += 1;

			if (blinkCount == 29) {
				damagedBool = false;
			}
			yield return new WaitForSeconds (0.05f);

		}
		GameObject.Find ("Player/character").GetComponent<SkinnedMeshRenderer> ().enabled = true;
	}

	private void Sprinting(){
		if (Input.GetKey(KeyCode.LeftShift) && stamina > 0){
			run=10;
			//stamina -= 0.03f;
			staminaBar.fillAmount -= 0.003f;

		}
		else
		{
			stamina += 0.02f;
			staminaBar.fillAmount += 0.002f;
			run=0.0f;
		}
	}



	private void changeFoodNum(){
		
		foodNum += 1;
		if (foodNum >= 3) {
			foodNum = 0;
		}
		gameObject.GetComponent<UIScript> ().changeFoodUI (foodNum);
	}


	void throwFood(){
		

		if (foodLeft [foodNum] > 0) {
			
			Vector3 pos = gameObject.transform.position + transform.TransformDirection (Vector3.forward) + new Vector3 (0, 2.3f, 0);
			GameObject food = Instantiate (foodPrefab [foodNum], pos, Quaternion.identity) as GameObject;
			//	Rigidbody rigidbody = food.GetComponent<Rigidbody> ();

			Vector3 bom_speed = transform.TransformDirection (Vector3.forward) * 7;
			bom_speed += Vector3.up * 10;	
			food.GetComponent< Rigidbody > ().velocity = bom_speed;
			foodLeft [foodNum] -= 1;

			foodLeftText [foodNum].text = foodLeft [foodNum].ToString();
		}



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


