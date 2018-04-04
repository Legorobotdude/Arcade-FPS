using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : HealthManager {

	GameObject player;
	Rigidbody rigidBody;
	Material[] m_Material;

	[SerializeField]float randomForce = 10f;
	[SerializeField]float towardsPlayerForce = 40f;

	[SerializeField]float randomForceTiming = 1f;
	[SerializeField]float forceTowardsPlayerTiming = 1f;

	//float tempTime;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rigidBody = GetComponent<Rigidbody>();
		InvokeRepeating("ApplyRandomForce",1f,randomForceTiming);
		InvokeRepeating("ApplyForceTowardsPlayer",1f,forceTowardsPlayerTiming);
		m_Material = GetComponent<Renderer>().materials;
	}
	
	// Update is called once per frame
	void Update () {
		//tempTime += Time.deltaTime;
		//transform.LookAt(player.transform);
		// if(Random.value>0.9)
		// {
		// 	rigidBody.AddForce(new Vector3(10f,0f,0f));
		// }
		if (!isDead)
		{

			foreach (Material mat in m_Material)
			{
				//mat.SetColor("_EmissionColor", Color.yellow* Mathf.LinearToGammaSpace(5));
				mat.SetColor("_EmissionColor", Color.red*5*currentHealth/maxHealth);
				//Debug.Log("Current health" + currentHealth);
				//Debug.Log("Max Health" + maxHealth);
				//Debug.Log(currentHealth/maxHealth);
			}
		}
	}

	void ApplyRandomForce()
	{
		rigidBody.AddForce(new Vector3(Random.Range(-randomForce,randomForce),Random.Range(-randomForce,randomForce),Random.Range(-randomForce,randomForce)));
	}
	void ApplyForceTowardsPlayer()
	{
		Vector3 aim = player.transform.position - transform.position;
		aim = aim/aim.magnitude;
		//Debug.Log(aim);
		//rigidBody.AddRelativeForce(new Vector3(0f,0f,10f));
		rigidBody.AddForce(towardsPlayerForce*aim);
	}

	override protected void Die()
	{
		Debug.Log("Die new");
		foreach (Material mat in m_Material)
		{
			//mat.SetColor("_EmissionColor", Color.yellow* Mathf.LinearToGammaSpace(5));
			mat.SetColor("_EmissionColor", Color.yellow*5);
		}
		//m_Material.SetColor ("_EmissionColor", Color.yellow);
	}
}
