using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalManager : MonoBehaviour
{
	public Sprite Blue;
	public Sprite Green;
	public Sprite Red;
	public Sprite Empty;

	public Image BlueRenderer;
	public Image GreenRenderer;
	public Image RedRenderer;

	public void Collected(Crystal.CrystalType type)
	{
		switch (type)
		{
			case Crystal.CrystalType.Green:
				GreenRenderer.sprite = Green;
				break;
			case Crystal.CrystalType.Blue:
				BlueRenderer.sprite = Blue;
				break;
			case Crystal.CrystalType.Red:
				RedRenderer.sprite = Red;
				break;
		}
	}
}
