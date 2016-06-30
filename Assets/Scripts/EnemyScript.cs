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
	private Vector3 roamBase;
	private Vector3 roamPosition;
	private float changeTargetSqrDistance = 20f;

	void Start(){
		roamBase = gameObject.transform.position;
		roamPosition = GetRandomPositionOnLevel();

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

		if (gameObject.tag == "giant") {
			GetComponent<Animation> ().Play ("walk");
		}


		player = GameObject.Find ("Player");
		target = player.transform;
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	void Update(){

		// 目標地点との距離が小さければ、次のランダムな目標地点を設定する
		float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - roamPosition);
		if (sqrDistanceToTarget < changeTargetSqrDistance)
		{
			roamPosition = GetRandomPositionOnLevel();
		}

		//enemyから、PlayerとZIpperまでの距離をもとめる
		agentToTargetDistance = Vector3.Distance(this.agent.transform.position,player.transform.position);

		decideTargetId ();
		//zipperが近くにいてtameであれば追いかける
		//一番近いzipperとの距離　…minDistance
		//zipperもplayerもいなければroamする
		//giantは何も追いかけない
		if (gameObject.tag != "giant") {
			if (minDistanceToTarget < 30f && zippers [id].GetComponent<zipperScript> ().checkIsTamed ()) {
			
				agent.SetDestination (zippers [id].transform.position);
			} else if (agentToTargetDistance < 30f) {
				agent.SetDestination (player.transform.position);
			} else if (gameObject.transform.parent.gameObject.transform.parent.gameObject.tag == "Grass") {
				agent.SetDestination (roamPosition);
			}
		} else {
			//巨人は必ずroam
			agent.SetDestination (roamPosition);
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
			if (col.gameObject.tag == "Zipper" && col.gameObject.GetComponent<zipperScript>().checkIsTamed()) {
				//Sound.PlaySe ("attack");
				col.gameObject.SendMessage ("Damage");
			}

			//animator.SetTrigger ("attack");
		}
	}

	private Vector3 GetRandomPositionOnLevel(){
		float levelSize = 30f;
		return new Vector3(roamBase.x + Random.Range(-levelSize,levelSize), 0,roamBase.z + Random.Range(-levelSize,levelSize));

	}

	//private void 
}
