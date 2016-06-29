using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class zipperScript : MonoBehaviour {
	//Zipper identity
	private float HP = 10;
	private bool beDead;
	private Image HPBar;



	//Script
	zipperCountScript zipperCountScript;
	public PlayerScript playerScript;

	//Agent関係
	private float INF = 100000;
	private GameObject player_follower;
	private GameObject[] enemies;
	private float agentToEnemyDistance;
	private float minDistanceToTarget;
	private int id;
	NavMeshAgent agent;
	private enum State
	{
		free,
		tamed,
		attack
	};
	private State state;
	private bool isTamed;

	private Vector3 roamPosition;
	private float changeTargetSqrDistance = 40f;

	//food関連
	public GameObject energyBall;
	public GameObject curedEffect;

	//attack関連
	public GameObject attackEffect;
	public int attackPower = 1;

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		player_follower = GameObject.Find ("Player/player_follower");

		//LoadSound
		Sound.LoadSe ("tame", "zipper_tame");
		Sound.LoadSe ("attack", "zipper_attack");
		Sound.LoadSe ("down", "zipper_down");
		Sound.LoadSe ("cured", "zipper_cured");

		rigidbody = gameObject.GetComponent<Rigidbody> ();
		gameObject.GetComponent<Animation> ().Play ("idle");
		agent = gameObject.GetComponent<NavMeshAgent> ();
		roamPosition = GetRandomPositionOnLevel();
		zipperCountScript = GameObject.Find ("ZIPPER").GetComponent<zipperCountScript> ();

		enemies = GameObject.FindGameObjectsWithTag ("enemy");

	}
	
	// Update is called once per frame
	void Update () {

		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - roamPosition);
		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
			roamPosition = GetRandomPositionOnLevel();
		}

		
		TargetDecide ();
		decideTargetId ();

	}

	public bool checkIsTamed(){
		if (isTamed) {
			return true;
		}else{
			return false;
		}
	}

	public void Damage(){
		if (isTamed) {
			HP--;
			HPBar.fillAmount -= 0.1f;//damage処理はzipperCountでやるほうが面倒そうなのでこっちでやる

			//death処理
			if (HP < 1 && !beDead) {
				beDead = true;
				Sound.PlaySe ("down");
				Destroy (gameObject, 2);
				zipperCountScript.zipperCount (-1);
				gameObject.GetComponent<Animation> ().Play ("deth");

			}
		}
	}


	private void TargetDecide(){
		
		switch (state) {
		case State.free:
			if (gameObject.transform.parent.gameObject.tag != "black") {
				agent.SetDestination (roamPosition);
			}
			break;
		case State.tamed:
			agent.SetDestination (player_follower.transform.position);
			break;
		case State.attack:
			decideTargetId ();

			if (enemies.Length != 0){
				if(enemies [id] != null) {
					agent.SetDestination (enemies [id].transform.position);
				}
			}
			break;
		}

	}

	public void eatFood(int foodNum){
		switch (foodNum) {
		case 0:
			if (!isTamed) {
				Sound.PlaySe ("tame");
				zipperCountScript.zipperCount (1);
				GameObject.Find ("Canvas/zipperHP/" + zipperCountScript.numberOfTamedZipper).SetActive (true);
				print (zipperCountScript.numberOfTamedZipper);
				HPBar = GameObject.Find ("Canvas/zipperHP/" + zipperCountScript.numberOfTamedZipper).GetComponent<Image> ();

			}
			isTamed = true;
			gameObject.GetComponent<Animation> ().Play ("attack");Invoke("beIdle",2f);
			state = State.tamed;
			break;

		case 1://ハンバーガー
			Sound.PlaySe ("cured");
			Instantiate (curedEffect, gameObject.transform.position + new Vector3 (0, 2, 0), Quaternion.identity);
			gameObject.GetComponent<Animation> ().Play ("attack");
			Invoke ("beIdle", 2);
			HP += 5;
			if (isTamed) {
				HPBar.fillAmount += 5;
			}
			break;

		case 2://dragon fruit
			gameObject.GetComponent<Animation> ().Play ("attack");Invoke("beIdle",2);
			Invoke ("fire", 2);
			break;
		}


	}

	//idle状態へ戻る
	private void beIdle(){
		gameObject.GetComponent<Animation> ().Play ("idle");
	}

	private void fire(){
		Instantiate (energyBall, gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity);
		//fire.transform.position = 
	}





	//敵を攻撃
	void OnCollisionEnter(Collision col){
		
		//キノコを食べるアクション
		if (col.gameObject.tag == "enemy" && !beDead) {
			Sound.PlaySe ("attack");
			gameObject.GetComponent<Animation>().Play("attack");Invoke("beIdle",2);
			 Instantiate (attackEffect, gameObject.transform.position, Quaternion.identity);
			col.gameObject.GetComponent<EnemyScript> ().Damage (attackPower);
			//Destroy (enemies[id].gameObject);


		}
	}

	private Vector3 GetRandomPositionOnLevel(){
		float levelSize = 30f;
		return new Vector3(gameObject.transform.position.x + Random.Range(-levelSize,levelSize), 0,gameObject.transform.position.z + Random.Range(-levelSize,levelSize));
	
	}



	//周りのエネミーから近い奴のidを出す
	private void decideTargetId(){
			minDistanceToTarget = INF;

			enemies = GameObject.FindGameObjectsWithTag ("enemy");
		for (int i = 0; i < enemies.Length; i++) {
			agentToEnemyDistance = Vector3.Distance (this.agent.transform.position, enemies [i].transform.position);
			if (agentToEnemyDistance < minDistanceToTarget) {
				id = i;
				minDistanceToTarget = agentToEnemyDistance;
			}
		}

		//近くにいたらattack,いなかったらtamed
		if (minDistanceToTarget < 30f && isTamed) {
			state = State.attack;
		} else if (isTamed) {
			state = State.tamed;

		}

	}





}
