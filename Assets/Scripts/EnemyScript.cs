using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	Animator animator;
	//enemyIdentity
	public int enemyHP;

	//Script
	zipperScript zipperScript;

	//Gameobject
	private GameObject player;
	public GameObject[] zippers;

	//agent関係
	NavMeshAgent agent;
	private Transform target;
	private int id;
	private float agentToTargetDistance;
	private float agentToZipperDistance;
	private float minDistanceToTarget;
	private float INF = 10000000;


	void Start(){
		//AudioClip clip = gameObject.GetComponent<AudioSource> ().clip;
		Sound.LoadSe("attack", "attack2");

		switch (gameObject.transform.parent.gameObject.tag) {
		case "sizeS":
			enemyHP = 1;
			break;
		case "sizeM":
			enemyHP = 5;
			break;
		case "sizeL":
			enemyHP = 20;
			break;
		
		}


		player = GameObject.Find ("Player");
		target = player.transform;
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	void Update(){
		//enemyから、PlayerとZIpperまでの距離をもとめる
		agentToTargetDistance = Vector3.Distance(this.agent.transform.position,player.transform.position);

		decideTargetId ();
		//zipperが近くにいてtameであれば追いかける
		//一番近いzipperとの距離　…minDistance
		if (minDistanceToTarget < 30f && zippers[id].GetComponent<zipperScript>().checkIsTamed()) {
			
			agent.SetDestination (zippers [id].transform.position);
		} else if (agentToTargetDistance < 30f) {
			agent.SetDestination (player.transform.position);
		}

		//プレイヤーとの距離が一定以下でプレイヤーを追いかける

	}

	public void Damage(int damage){
		enemyHP -= damage;
		if (enemyHP <= 0) {
			Destroy (gameObject);
		}

	}



	//周りのzipperから近い奴のidを出す
	private void decideTargetId(){
		minDistanceToTarget = INF;

		zippers = GameObject.FindGameObjectsWithTag ("Zipper");
		for (int i = 0; i < zippers.Length; i++) {
			agentToZipperDistance = Vector3.Distance (this.agent.transform.position, zippers [i].transform.position);
		
			if (agentToZipperDistance < minDistanceToTarget) {
				id = i;
				minDistanceToTarget = agentToZipperDistance;
			}
	}
	}


	// 物にさわった時に呼ばれる
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != "terrain") {
			if (col.gameObject.tag == "Zipper") {
				Sound.PlaySe ("attack");
				col.gameObject.SendMessage ("Damage");
			}

			animator.SetTrigger ("attack");
		}
	}

	//private void 
}
