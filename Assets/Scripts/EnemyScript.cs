using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	Animator animator;
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

		player = GameObject.Find ("Player");
		target = player.transform;
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	void Update(){
		//PlayerとZIpperまでの距離をもとめる
		agentToTargetDistance = Vector3.Distance(this.agent.transform.position,player.transform.position);
		agentToZipperDistance = Vector3.Distance(this.agent.transform.position,player.transform.position);

		decideTargetId ();

		//zipperが近くにいてtameであれば追いかける
		if (agentToZipperDistance < 30f && zippers[id].GetComponent<zipperScript>().checkIsTamed()) {
			agent.SetDestination (zippers [id].transform.position);
		} else if (agentToTargetDistance < 30f) {
			agent.SetDestination (player.transform.position);
		}
			//DoPatrol ();
		//プレイヤーとの距離が一定以下でプレイヤーを追いかける

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
		
		if (col.gameObject.tag == "Zipper") {
			col.gameObject.SendMessage ("Damage");
		}

		animator.SetTrigger ("attack");
	}

	//private void 
}
