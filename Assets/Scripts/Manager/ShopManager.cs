using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [Header ("Speed")]
    public Button speedUpgradeButton;
    public TextMeshProUGUI speedDescription;
    public TextMeshProUGUI speedCostText;
    public int speedCost;
    public int speedMaxCost;

    [Header("Damage")]
    public Button damageUpgradeButton;
    public TextMeshProUGUI damageDescription;
    public TextMeshProUGUI damageCostText;
    public int damageCost;
    public int damageMaxCost;
    
    [Header("SpreadAngle")]
    public Button spreadAngleUpgradeButton;
    public TextMeshProUGUI spreadAngleDescription;
    public TextMeshProUGUI spreadAngleCostText;
    public int spreadAngleCost;
    public int spreadAngleMaxCost;
    
    [Header("SpreadProjectile")]
    public Button spreadProjectileUpgradeButton;
    public TextMeshProUGUI spreadProjectileDescription;
    public TextMeshProUGUI spreadProjectileCostText;
    public int spreadProjectileCost;
    public int spreadProjectileMaxCost;
    
    [Header("BlastRadius")]
    public Button blastUpgradeButton;
    public TextMeshProUGUI blastDescription;
    public TextMeshProUGUI blastCostText;
    public int blastCost;
    public int blastMaxCost;
    
    [Header("FreezeEnemyDuration")]
    public Button freezeEnemyDurationUpgradeButton;
    public TextMeshProUGUI freezeEnemyDurationDescription;
    public TextMeshProUGUI freezeEnemyDurationCostText;
    public int freezeEnemyDurationCost;
    public int freezeEnemyDurationMaxCost;

    // Start is called before the first frame update
    void Start()
    {
        speedUpgradeButton.onClick.AddListener(OnSpeedUpgrade);
        damageUpgradeButton.onClick.AddListener(OnDamageUpgrade);
        UpdateSpeedButton();
        UpdateDamageButton();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //---------------------------------------------------General Upgrades-------------------------------------------------------------
    #region Speed 
    public void OnSpeedUpgrade()
    {
        if (CoinManager.Instance.Coins >= speedCost)
        {
            if (!AttackManager.Instance.IncreaseSpeed(5))
            {
                return;
            }
            CoinManager.Instance.Coins -= speedCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (speedCost <  speedMaxCost)
        {
            speedCost = Mathf.RoundToInt(speedCost * 1.9f);

        }
        else
        {
            speedCost += speedCost/3;
        }
        UpdateSpeedButton();
    }

    private void UpdateSpeedButton()
    {
        if (AttackManager.Instance.speed >= 100)
        {
            speedDescription.text = "Speed : " + AttackManager.Instance.speed.ToString();
            speedCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.speed + 5;

        speedDescription.text = "Speed : " + AttackManager.Instance.speed.ToString() + " -> " + newStat.ToString();
        speedCostText.text = "Cost : " + speedCost.ToString();
    }
    #endregion

    #region Damage 

    public void OnDamageUpgrade()
    {
        if (CoinManager.Instance.Coins >= damageCost)
        {
            if (!AttackManager.Instance.IncreaseDamage(1))
            {
                return;
            }
            CoinManager.Instance.Coins -= damageCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (damageCost < damageMaxCost)
        {
            damageCost = Mathf.RoundToInt(damageCost * 2.5f);

        }
        else
        {
            damageCost += damageCost / 3;
        }
        UpdateDamageButton();
    }

    private void UpdateDamageButton()
    {
        if (AttackManager.Instance.damage >= 5)
        {
            damageDescription.text = "Damage : " + AttackManager.Instance.damage.ToString();
            damageCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.damage + 1;
        damageDescription.text = "Damage : " + AttackManager.Instance.damage.ToString() + " -> " + newStat.ToString();
        damageCostText.text = "Cost : " + damageCost.ToString();
    }
    #endregion

    //---------------------------------------------------Skill General Upgrade-------------------------------------------------------------
    #region SpreadAngle 

    #endregion

    #region SpreadProjectile

    #endregion

    #region BlastRadius

    #endregion

    #region FreezeEnemyDuration

    #endregion
    //---------------------------------------------------Skill Duration Upgrade-------------------------------------------------------------

}
