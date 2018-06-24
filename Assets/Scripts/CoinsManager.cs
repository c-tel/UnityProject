using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public Text text;
    
    public void SetCoins(int coins)
    {
        string coinStr = coins.ToString();
        while (coinStr.Length<3)
        {
            coinStr = "0" + coinStr;
        }
        text.text = coinStr;
    }
}
