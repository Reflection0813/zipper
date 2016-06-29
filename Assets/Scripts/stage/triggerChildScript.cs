using UnityEngine;
using System.Collections;

public class triggerChildScript : MonoBehaviour {
	GameObject parent;

	void Start () {
		
		parent = gameObject.transform.parent.gameObject;
	}

	void OnTriggerEnter(Collider collider)
	{
		parent.SendMessage("RedirectedOnTriggerEnter", collider);
	}

}
