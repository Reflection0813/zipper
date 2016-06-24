using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	//AudioClip clip;

	private Transform target;
	NavMeshAgent agent;
	Animator animator;
	public int enemyHP = 3; // 敵の体力
	public GameObject Bomb; // 爆発のオブジェクト

	private GameObject player;
	private float agentToTargetDistance;
	private Vector3 pos;

	void Start(){
		//AudioClip clip = gameObject.GetComponent<AudioSource> ().clip;

		player = GameObject.Find ("Player");
		target = player.transform;
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
	}

	void Update(){
		agentToTargetDistance = Vector3.Distance(this.agent.transform.position,player.transform.position);
		if (agentToTargetDistance < 30f) {
			agent.SetDestination (target.position);
		} else {
			DoPatrol ();
		}//プレイヤーとの距離が一定以上でプレイヤーを追いかける

	}

	private void DoPatrol(){
		
		pos = new Vector3 (gameObject.transform.position.x, 0, gameObject.transform.position.z);
		agent.SetDestination (pos);
	}


	// Playerにダメージを与えられた時
	void Damage(){
		enemyHP--; //体力を1減らす。
		// 体力がゼロになったら
		if (enemyHP == 0) {
			if (Bomb) {
				// 爆発を起こす
				Instantiate (Bomb, transform.position, transform.rotation);
			}
			// 敵を倒した数を1増やす
			ScoreManager.instance.enemyCount++;


		}
	}

	// 物にさわった時に呼ばれる
	void OnCollisionEnter(Collision col) {
		

		if (col.gameObject.tag == "Zipper") {
			//gameObject.GetComponent<AudioSource> ().PlayOneShot (clip);
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "Player") {
			print ("Collision");
			col.collider.SendMessage ("Damage"); //ダメージを与えて
		}

		animator.SetTrigger ("attack");
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		print ("aa");
		animator.SetTrigger ("attack");
		if (hit.gameObject.tag == "Player") {
			hit.collider.SendMessage ("Damage"); //ダメージを与えて
		}
	}

	void OnTriggerEnter(Collider col){
		// もしPlayerにさわったら
		if (col.gameObject.tag == "Player") {
			col.SendMessage ("Damage"); //ダメージを与えて
		}

		animator.SetTrigger ("attack");

	}
}
