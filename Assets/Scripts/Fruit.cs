using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit: Collectable
{

	private static int numFruits;

	public static int NumFruits
	{
		get { return numFruits; }
	}

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		LevelController.current.AddFruit();
		Destroy (gameObject);
		
	}

	private void Awake()
	{
		numFruits = FindObjectsOfType<Fruit>().Length;

	}
}