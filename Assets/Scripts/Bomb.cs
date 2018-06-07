using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		if (!rabbit.Enlarged ())
			rabbit.AnimatedDeath ();
		else
			rabbit.Reduce ();
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
