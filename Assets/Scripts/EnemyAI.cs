using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	GameObject player;
	Rigidbody rigidBody;

	float randomForce = 10f;
	float towardsPlayerForce = 20f;

	//float tempTime;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rigidBody = GetComponent<Rigidbody>();
		InvokeRepeating("ApplyRandomForce",1f,1f);
		InvokeRepeating("ApplyForceTowardsPlayer",1f,5f);
	}
	
	// Update is called once per frame
	void Update () {
		//tempTime += Time.deltaTime;
		transform.LookAt(player.transform);
		// if(Random.value>0.9)
		// {
		// 	rigidBody.AddForce(new Vector3(10f,0f,0f));
		// }
	}

	void ApplyRandomForce()
	{
		rigidBody.AddForce(new Vector3(Random.Range(-randomForce,randomForce),Random.Range(-randomForce,randomForce),Random.Range(-randomForce,randomForce)));
	}
	void ApplyForceTowardsPlayer()
	{
		Vector3 aim = player.transform.position - transform.position;
		//rigidBody.AddRelativeForce(new Vector3(0f,0f,10f));
		rigidBody.AddRelativeForce(towardsPlayerForce*aim);
	}
}
