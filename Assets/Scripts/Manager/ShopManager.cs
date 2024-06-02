using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [Header("Speed")]
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
        spreadAngleUpgradeButton.onClick.AddListener(OnSpreadAngleUpgrade);
        spreadProjectileUpgradeButton.onClick.AddListener(OnSpreadProjectileUpgrade);
        blastUpgradeButton.onClick.AddListener(OnBlastUpgrade);
        freezeEnemyDurationUpgradeButton.onClick.AddListener(OnFreezeUpgrade);

        UpdateSpeedButton();
        UpdateDamageButton();
        UpdateSpreadAngleButton();
        UpdateSpreadProjectileButton();
        UpdateBlastButton();
        UpdateFreezeButton();

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

        if (speedCost < speedMaxCost)
        {
            speedCost = Mathf.RoundToInt(speedCost * 1.9f);

        }
        else
        {
            speedCost += speedCost / 3;
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
            damageCost += damageCost / 2;
        }
        UpdateDamageButton();
    }

    private void UpdateDamageButton()
    {
        if (AttackManager.Instance.damage >= 6)
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
    public void OnSpreadAngleUpgrade()
    {
        if (CoinManager.Instance.Coins >= spreadAngleCost)
        {
            if (!AttackManager.Instance.IncreaseSpreadAngle(15))
            {
                return;
            }
            CoinManager.Instance.Coins -= spreadAngleCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (spreadAngleCost < spreadAngleMaxCost)
        {
            spreadAngleCost = Mathf.RoundToInt(spreadAngleCost * 1.5f);

        }
        else
        {
            spreadAngleCost += spreadAngleCost / 25;
        }
        UpdateSpreadAngleButton();
    }

    private void UpdateSpreadAngleButton()
    {
        if (AttackManager.Instance.spreadAngle >= 360)
        {
            spreadAngleDescription.text = "Angle : " + AttackManager.Instance.spreadAngle.ToString();
            spreadAngleCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.spreadAngle + 15;
        spreadAngleDescription.text = "Angle : " + AttackManager.Instance.spreadAngle.ToString() + " -> " + newStat.ToString();
        spreadAngleCostText.text = "Cost : " + spreadAngleCost.ToString();
    }
    #endregion

    #region SpreadProjectile
    public void OnSpreadProjectileUpgrade()
    {
        if (CoinManager.Instance.Coins >= spreadProjectileCost)
        {
            if (!AttackManager.Instance.IncreaseSpreadProjectile(1))
            {
                return;
            }
            CoinManager.Instance.Coins -= spreadProjectileCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (spreadProjectileCost < spreadProjectileMaxCost)
        {
            spreadProjectileCost = Mathf.RoundToInt(spreadProjectileCost * 1.5f);

        }
        else
        {
            spreadProjectileCost += spreadProjectileCost / 20;
        }
        UpdateSpreadProjectileButton();
    }

    private void UpdateSpreadProjectileButton()
    {
        if (AttackManager.Instance.numberOfProjectiles >= 32)
        {
            spreadProjectileDescription.text = "Proj. no : " + AttackManager.Instance.numberOfProjectiles.ToString();
            spreadProjectileCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.numberOfProjectiles + 1;
        spreadProjectileDescription.text = "Proj. no : " + AttackManager.Instance.numberOfProjectiles.ToString() + " -> " + newStat.ToString();
        spreadProjectileCostText.text = "Cost : " + spreadProjectileCost.ToString();
    }
    #endregion

    #region BlastRadius
    public void OnBlastUpgrade()
    {
        if (CoinManager.Instance.Coins >= blastCost)
        {
            if (!AttackManager.Instance.IncreaseBlastRadius(0.1f))
            {
                return;
            }
            CoinManager.Instance.Coins -= blastCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (blastCost < blastMaxCost)
        {
            blastCost = Mathf.RoundToInt(blastCost * 1.8f);

        }
        else
        {
            blastCost += blastCost / 5;
        }
        UpdateBlastButton();
    }

    private void UpdateBlastButton()
    {
        if (AttackManager.Instance.explosionRadius >= 1.5)
        {
            blastDescription.text = "Radius : " + AttackManager.Instance.explosionRadius.ToString();
            blastCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.explosionRadius + 0.1f;
        blastDescription.text = "Radius : " + AttackManager.Instance.explosionRadius.ToString("F1") + " -> " + newStat.ToString("F1");
        blastCostText.text = "Cost : " + blastCost.ToString();
    }
    #endregion

    #region FreezeEnemyDuration
    public void OnFreezeUpgrade()
    {
        if (CoinManager.Instance.Coins >= freezeEnemyDurationCost)
        {
            if (!AttackManager.Instance.IncreaseFreezeEnemyDuration(0.5f))
            {
                return;
            }
            CoinManager.Instance.Coins -= freezeEnemyDurationCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (freezeEnemyDurationCost < freezeEnemyDurationMaxCost)
        {
            freezeEnemyDurationCost = Mathf.RoundToInt(freezeEnemyDurationCost * 2f);

        }
        else
        {
            freezeEnemyDurationCost += freezeEnemyDurationCost / 5;
        }
        UpdateFreezeButton();
    }

    private void UpdateFreezeButton()
    {
        if (AttackManager.Instance.freezeDuration >= 5)
        {
            freezeEnemyDurationDescription.text = "Freeze Duration : " + AttackManager.Instance.freezeDuration.ToString();
            freezeEnemyDurationCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.freezeDuration + 0.5f;
        freezeEnemyDurationDescription.text = "Freeze Duration : " + AttackManager.Instance.freezeDuration.ToString("F1") + " -> " + newStat.ToString("F1");
        freezeEnemyDurationCostText.text = "Cost : " + freezeEnemyDurationCost.ToString();
        #endregion

    }
}
