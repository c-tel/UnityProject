using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	public static LevelController current;
	Vector3 startingPosition;
	public CoinsManager coins;
	public LifesManager lifes;
	public FruitManager fruits;
	public CrystalManager CrystalsManager;
	private int coinsNum;
	private int fruitsNum;
	private int lifesNum = 3;

	private List<Crystal.CrystalType> _crystals = new List<Crystal.CrystalType>();
	
	void Awake () {
		current = this;
		if (coins != null)
		{
			fruits.SetFruits(0, Fruit.NumFruits);
			lifes.SetLifes(lifesNum);
			coins.SetCoins(0);			
		}
		
	}
	
	public void setStartPosition(Vector3 pos) {
		startingPosition = pos;
	}

	public void AddCoin()
	{
		coins.SetCoins(++coinsNum);
	}
	public void AddFruit()
	{
		fruits.SetFruits(++fruitsNum, Fruit.NumFruits);
	}

	public void CollectedCrystal(Crystal.CrystalType type)
	{
		if(_crystals.Contains(type))
			return;
		_crystals.Add(type);
		CrystalsManager.Collected(type);

		if (_crystals.Count >= 3)
		{
			
		}
	}
	
	public void onRabbitDeath(HeroRabbit rabit) {
		if (lifes == null)
		{
			rabit.transform.position = startingPosition;
			return;			
		}
		
		if (lifesNum > 1)
		{
			lifes.SetLifes(--lifesNum);
			rabit.transform.position = startingPosition;
		}
		else
		{
			SceneManager.LoadScene ("LevelChooser");

		}
	}
}
