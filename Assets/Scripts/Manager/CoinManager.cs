using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : Singleton<CoinManager>
{
    public int Coins { get; set; }
    private bool initialized = false; // To check if the coins have been initialized

    private readonly string COINS_KEY = "MyGame_MyCoins_DontCheat";

    public TextMeshProUGUI CurrencyText;

    public Button cheatButton;

    
    private void Start()
    {
        PlayerPrefs.DeleteKey(COINS_KEY);
        LoadCoins();
        cheatButton.onClick.AddListener(GetMaxCoin);
        //cheatButton.onClick.AddListener(AttackManager.Instance.InfiniteSkill);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            GetMaxCoin();
        }
    }

    private void GetMaxCoin()
    {
        AddCoins(1000000000);
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
