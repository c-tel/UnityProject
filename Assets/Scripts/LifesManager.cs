using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesManager : MonoBehaviour {
	
	public Sprite filled;
	public Sprite empty;
	
	// Use this for initialization
	public void SetLifes(int lifes)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).GetComponent<Image>().sprite = lifes > i ? filled : empty;
		}
	}
}
