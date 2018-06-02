using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabbit rabit = collider.GetComponent<HeroRabbit> ();
		if(rabit != null) 
			LevelController.current.onRabbitDeath(rabit);
}
}
