using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public bool isDead = false;
    public float maxHealth = 100f;
    protected float currentHealth;

    void Awake() {
        currentHealth = maxHealth;
    }

	// Use this for initialization
	// void Start () {
	// 	currentHealth = maxHealth;
	// }
	
	// Update is called once per frame
	// void Update () {
		
	// }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f || !isDead)
            Die();
    }

    virtual protected void Die()
    {
        isDead = true; 
        Destroy(this.gameObject,1f);
    }
}
