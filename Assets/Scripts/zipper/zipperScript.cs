using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class zipperScript : MonoBehaviour {
	//Zipper identity
	private float HP = 3;
	private bool beDead;


	//Script
	zipperCountScript zipperCountScript;

	//Agent関係
	private float INF = 100000;
	public GameObject player;
	public GameObject player_follower;
	public GameObject[] enemies;
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

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
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

			print("id = " + id);
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
	private void beFat(){
		if (!isTamed) {
			zipperCountScript.zipperCount (1);
		}
		isTamed = true;
		gameObject.GetComponent<Animation>().Play("attack");
		state = State.tamed;
	}



	void OnCollisionEnter(Collision col){

		//キノコを食べるアクション
		if (col.collider.tag == "enemy" && !beDead) {
			print ("EAT!!");
			gameObject.GetComponent<Animation>().Play("attack");
			Destroy (enemies[id].gameObject);

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
