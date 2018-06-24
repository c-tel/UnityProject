using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{
	public Text text;
	public void SetFruits(int fruits, int maxFruits)
	{
		text.text = String.Format("{0}/{1}", fruits, maxFruits);
	}

}
