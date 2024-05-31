using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int Coins { get; set; }
    private bool initialized = false; // To check if the coins have been initialized

    private readonly string COINS_KEY = "MyGame_MyCoins_DontCheat";

    public TextMeshProUGUI CurrencyText;

    
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        LoadCoins();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddCoins(1000000000);
        }
    }

    private void LoadCoins()
    {
        if (!initialized)
        {
            Coins = PlayerPrefs.GetInt(COINS_KEY, 0);
            initialized = true;
        }
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt(COINS_KEY, Coins);
    }

}
