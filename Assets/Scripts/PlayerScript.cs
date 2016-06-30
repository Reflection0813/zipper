using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript: MonoBehaviour {
	Animator animator;
	tutorialScript tutorialScript;

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
	public GameObject curedEffect;




	//parts関連
	private int gatheredParts = 0;
	private int clearNum = 4; //何個のパーツでクリアか


	//tutorialText

	void Start(){
		tutorialScript = GameObject.Find ("GameManager").GetComponent<tutorialScript> ();
		tutorialScript.changeTutorialText ("散らばった飛行船のパーツを集めよう！",5);

		Sound.LoadSe ("throw", "throw");Sound.LoadSe ("select", "select");Sound.LoadSe ("dash_1", "dash_1");
		Sound.LoadSe ("damaged", "player_damaged");Sound.LoadSe ("death", "player_death");
		Sound.LoadSe ("get", "get");Sound.LoadSe ("getfood", "getfood");
		Sound.LoadSe ("eat", "player_eat");
	
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
			Sound.PlaySe ("throw");
			throwFood ();
		}

		if (foodLeft [foodNum] >= 10) {
			if (Input.GetKeyDown (KeyCode.X)) {
				Sound.PlaySe ("eat");
				eatFood ();
			}
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			Sound.PlaySe ("select");
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
				Sound.PlaySe ("death");
				SceneManager.LoadScene ("GameOver");
			} else {
				Sound.PlaySe ("damaged");
			}
		}
	}
	//点滅
	IEnumerator Blink(){
		tutorialScript.changeTutorialText ("ダメージを受けた！！",2);
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
			run=1;
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


	//Cを押したらfoodChange
	private void changeFoodNum(){
		
		foodNum += 1;
		if (foodNum >= 3) {
			foodNum = 0;
		}
		gameObject.GetComponent<UIScript> ().changeFoodUI (foodNum);
	}
	void eatFood(){
		Instantiate (curedEffect,gameObject.transform.position,Quaternion.identity);
		if (playerHP <= 2) {
			playerHP += 1;
		}
		HPbar.fillAmount += 0.33f;

		foodLeft [foodNum] -=  10;
		gameObject.GetComponent<UIScript> ().changeFoodTX (foodNum, foodLeft [foodNum]);
	}

	void getFood(string tagname){
		Sound.PlaySe ("getfood");
		
		int tmp_foodNum = 0;
		switch (tagname) {
		case "item1":
			tmp_foodNum = 0;
			int i = Random.Range (0, 2);
			if (i == 0) {
				tutorialScript.changeTutorialText ("Zキーでドーナツをzipperに投げると仲間になる！", 2);
			} else {
				tutorialScript.changeTutorialText ("Xキーでドーナツを食べて回復できる！", 2);
			}

			break;
		case "item2":
			tmp_foodNum = 1;
			tutorialScript.changeTutorialText ("Zキーでハンバーガーをzipperに投げると回復する！",2);
			break;

		}
		int foodPoint = 5;
		foodLeft [tmp_foodNum] += foodPoint;
		gameObject.GetComponent<UIScript> ().changeFoodTX (tmp_foodNum, foodLeft [tmp_foodNum]);
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
			tutorialScript.changeTutorialText ("パーツを手に入れた！！",3);
			Sound.PlaySe ("get");
			Destroy (hit.gameObject);
			gatheredParts += 1;
			print ("gatherd = " + gatheredParts);

			hit.gameObject.GetComponent<partsScript> ().changePartsUIColor ();
		}

		if (hit.gameObject.tag == "enemy" || hit.gameObject.tag == "giant") {
			Damage ();
		}

		//食べ物に触れたら回収
		if (hit.gameObject.tag == "item1" || hit.gameObject.tag == "item2") {
			getFood (hit.gameObject.tag);
			Destroy (hit.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "ship") {
			tutorialScript.changeTutorialText ("愛車が壊れてしまった… 早くパーツを探して修理しよう",3);
		}
	}





}


