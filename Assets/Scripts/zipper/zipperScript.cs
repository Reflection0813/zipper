using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class zipperScript : MonoBehaviour {
	//Zipper identity
	private float HP = 10;
	private bool beDead;


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

	//food関連
	public GameObject energyBall;

	//attack関連
	public GameObject attackEffect;
	private int attackPower = 1;

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		player_follower = GameObject.Find ("Player/player_follower");

		rigidbody = gameObject.GetComponent<Rigidbody> ();
		gameObject.GetComponent<Animation> ().Play ("idle");
		agent = gameObject.GetComponent<NavMeshAgent> ();
		zipperCountScript = GameObject.Find ("ZIPPER").GetComponent<zipperCountScript> ();

		enemies = GameObject.FindGameObjectsWithTag ("enemy");
	}
	
	// Update is called once per frame
	void Update () {
		
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
		HP--;
		if (HP < 1 && !beDead) {
			beDead = true;
			Destroy (gameObject,2);
			zipperCountScript.zipperCount (-1);
			gameObject.GetComponent<Animation>().Play("deth");

		}
	}


	private void TargetDecide(){
		
		switch (state) {
		case State.free:
			//agent.SetDestination ();
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
	/*

	private void DoPatrol(){
		//gameObject.GetComponent<Animation>().Play("idle");
		var x = Random.Range(-50.0f, 50.0f);
		var z = Random.Range(-50.0f, 50.0f);
		pos = new Vector3 (x, 0, z);
		agent.SetDestination (pos);
	}
*/
	public void eatFood(int foodNum){
		switch (foodNum) {
		case 0:
			if (!isTamed) {
				zipperCountScript.zipperCount (1);
			}
			isTamed = true;
			gameObject.GetComponent<Animation> ().Play ("attack");
			state = State.tamed;
			break;

		case 1://ハンバーガー
			gameObject.GetComponent<Animation> ().Play ("attack");
			HP += 5;
			break;

		case 2://dragon fruit
			gameObject.GetComponent<Animation> ().Play ("attack");
			Invoke ("fire", 2);
			break;
		}


	}

	private void fire(){
		GameObject fire = Instantiate (energyBall, gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity) as GameObject;
		//fire.transform.position = 
	}


	//敵を攻撃
	void OnCollisionEnter(Collision col){
		
		//キノコを食べるアクション
		if (col.gameObject.tag == "enemy" && !beDead) {
			print ("EAT!!");
			gameObject.GetComponent<Animation>().Play("attack");
			GameObject attackEffect2 = Instantiate (attackEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
			col.gameObject.GetComponent<EnemyScript> ().Damage (attackPower);
			//Destroy (enemies[id].gameObject);


		}
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
