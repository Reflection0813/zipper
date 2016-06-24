using UnityEngine;
using System.Collections;

public class zipperScript : MonoBehaviour {
	static Vector3 pos;
	public GameObject player;
	//public GameObject group;

	NavMeshAgent agent;
	private bool tamed;
	private float agentToTargetDistance;
	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody> ();
		gameObject.GetComponent<Animation> ().Play ("idle");
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		//rigidbody.AddForce (10, 0, 0);
		if (tamed){
			agent.SetDestination (player.transform.position);
		}//tameされたらプレイヤーに懐く
		/*
		agentToTargetDistance = Vector3.Distance(this.agent.transform.position,group.transform.position);
		if (agentToTargetDistance < 30f) {
			agent.SetDestination (group.transform.position);
		} else {
			//DoPatrol ();
		}//プレイヤーとの距離が一定以上でプレイヤーを追いかける
	*/
	}


	private void DoPatrol(){
		//gameObject.GetComponent<Animation>().Play("idle");
		var x = Random.Range(-50.0f, 50.0f);
		var z = Random.Range(-50.0f, 50.0f);
		pos = new Vector3 (x, 0, z);
		agent.SetDestination (pos);
	}

	private void beFat(){
		
		gameObject.GetComponent<Animation>().Play("attack");
		//gameObject.transform.localScale += new Vector3 (1, 1, 1);
		tamed = true;
		//gameObject.GetComponent<Animation> ().Play ("idle");
	}
}
