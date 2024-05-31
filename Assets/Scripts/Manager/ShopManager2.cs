using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager2 : MonoBehaviour
{
    [Header("PierceDuration")]
    public Button pierceDurationUpgradeButton;
    public TextMeshProUGUI pierceDurationDescription;
    public TextMeshProUGUI pierceDurationCostText;
    public int pierceDurationCost;
    public int pierceDurationMaxCost;

    [Header("PierceCooldown")]
    public Button pierceCooldownUpgradeButton;
    public TextMeshProUGUI pierceCooldownDescription;
    public TextMeshProUGUI pierceCooldownCostText;
    public int pierceCooldownCost;
    public int pierceCooldownMaxCost;

    [Header("SpreadDuration")]
    public Button spreadDurationUpgradeButton;
    public TextMeshProUGUI spreadDurationDescription;
    public TextMeshProUGUI spreadDurationCostText;
    public int spreadDurationCost;
    public int spreadDurationMaxCost;

    [Header("SpreadCooldown")]
    public Button spreadCooldownUpgradeButton;
    public TextMeshProUGUI spreadCooldownDescription;
    public TextMeshProUGUI spreadCooldownCostText;
    public int spreadCooldownCost;
    public int spreadCooldownMaxCost;

    [Header("BlastDuration")]
    public Button blastDurationUpgradeButton;
    public TextMeshProUGUI blastDurationDescription;
    public TextMeshProUGUI blastDurationCostText;
    public int blastDurationCost;
    public int blastDurationMaxCost;

    [Header("BlastCooldown")]
    public Button blastCooldownUpgradeButton;
    public TextMeshProUGUI blastCooldownDescription;
    public TextMeshProUGUI blastCooldownCostText;
    public int blastCooldownCost;
    public int blastCooldownMaxCost;

    [Header("FreezeDuration")]
    public Button freezeDurationUpgradeButton;
    public TextMeshProUGUI freezeDurationDescription;
    public TextMeshProUGUI freezeDurationCostText;
    public int freezeDurationCost;
    public int freezeDurationMaxCost;

    [Header("FreezeCooldown")]
    public Button freezeCooldownUpgradeButton;
    public TextMeshProUGUI freezeCooldownDescription;
    public TextMeshProUGUI freezeCooldownCostText;
    public int freezeCooldownCost;
    public int freezeCooldownMaxCost;

    // Start is called before the first frame update
    void Start()
    {
        pierceDurationUpgradeButton.onClick.AddListener(OnPierceDurationUpgrade);
        pierceCooldownUpgradeButton.onClick.AddListener(OnPierceCooldownUpgrade);
 
        spreadDurationUpgradeButton.onClick.AddListener(OnSpreadDurationUpgrade);
        spreadCooldownUpgradeButton.onClick.AddListener(OnSpreadCooldownUpgrade);

         blastDurationUpgradeButton.onClick.AddListener(OnBlastDurationUpgrade);
         blastCooldownUpgradeButton.onClick.AddListener(OnBlastCooldownUpgrade);

         freezeDurationUpgradeButton.onClick.AddListener(OnFreezeDurationUpgrade);
         freezeCooldownUpgradeButton.onClick.AddListener(OnFreezeCooldownUpgrade);

         UpdatePierceDurationButton();
         UpdatePierceCooldownButton();

         UpdateSpreadDurationButton();
         UpdateSpreadCooldownButton();

         UpdateBlastDurationButton();
         UpdateBlastCooldownButton();

         UpdateFreezeDurationButton();
         UpdateFreezeCooldownButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region PierceDuration
    public void OnPierceDurationUpgrade()
    {
        if (CoinManager.Instance.Coins >= pierceDurationCost)
        {
            if (!AttackManager.Instance.IncreasePierceDuration(0.5f))
            {
                return;
            }
            CoinManager.Instance.Coins -= pierceDurationCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (pierceDurationCost < pierceDurationMaxCost)
        {
            pierceDurationCost = Mathf.RoundToInt(pierceDurationCost * 1.5f);

        }
        else
        {
            pierceDurationCost += pierceDurationCost / 20;
        }
        UpdatePierceDurationButton();
    }

    private void UpdatePierceDurationButton()
    {
        if (AttackManager.Instance.pierceSkillDuration >= 6)
        {
            pierceDurationDescription.text = "Duration : " + AttackManager.Instance.pierceSkillDuration.ToString();
            pierceDurationCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.pierceSkillDuration + 0.5f;
        pierceDurationDescription.text = "Duration : " + AttackManager.Instance.pierceSkillDuration.ToString() + " -> " + newStat.ToString();
        pierceDurationCostText.text = "Cost : " + pierceDurationCost.ToString();
    }
    #endregion

    #region PierceCooldown
    public void OnPierceCooldownUpgrade()
    {
        if (CoinManager.Instance.Coins >= pierceCooldownCost)
        {
            if (!AttackManager.Instance.DecreasePierceCooldownDuration(1))
            {
                return;
            }
            CoinManager.Instance.Coins -= pierceCooldownCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (pierceCooldownCost < pierceCooldownMaxCost)
        {
            pierceCooldownCost = Mathf.RoundToInt(pierceCooldownCost * 1.8f);

        }
        else
        {
            pierceCooldownCost += pierceCooldownCost / 10;
        }
        UpdatePierceCooldownButton();
    }

    private void UpdatePierceCooldownButton()
    {
        if (AttackManager.Instance.pierceCooldownDuration <= 5)
        {
            pierceCooldownDescription.text = "Cooldown : " + AttackManager.Instance.pierceCooldownDuration.ToString();
            pierceCooldownCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.pierceCooldownDuration - 1;
        pierceCooldownDescription.text = "Cooldown : " + AttackManager.Instance.pierceCooldownDuration.ToString() + " -> " + newStat.ToString();
        pierceCooldownCostText.text = "Cost : " + pierceCooldownCost.ToString();
    }
    #endregion

    #region SpreadDuration

    public void OnSpreadDurationUpgrade()
    {
        if (CoinManager.Instance.Coins >= spreadDurationCost)
        {
            if (!AttackManager.Instance.IncreaseSpreadDuration(1f))
            {
                return;
            }
            CoinManager.Instance.Coins -= spreadDurationCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (spreadDurationCost < spreadDurationMaxCost)
        {
            spreadDurationCost = Mathf.RoundToInt(spreadDurationCost * 1.7f);

        }
        else
        {
            spreadDurationCost += spreadDurationCost / 15;
        }
        UpdateSpreadDurationButton();
    }

    private void UpdateSpreadDurationButton()
    {
        if (AttackManager.Instance.spreadSkillDuration >= 8)
        {
            spreadDurationDescription.text = "Duration : " + AttackManager.Instance.spreadSkillDuration.ToString();
            spreadDurationCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.spreadSkillDuration + 1f;
        spreadDurationDescription.text = "Duration : " + AttackManager.Instance.spreadSkillDuration.ToString() + " -> " + newStat.ToString();
        spreadDurationCostText.text = "Cost : " + spreadDurationCost.ToString();
    }
    #endregion

    #region SpreadCooldown
    public void OnSpreadCooldownUpgrade()
    {
        if (CoinManager.Instance.Coins >= spreadCooldownCost)
        {
            if (!AttackManager.Instance.DecreaseSpreadCooldownDuration(1))
            {
                return;
            }
            CoinManager.Instance.Coins -= spreadCooldownCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (spreadCooldownCost < spreadCooldownMaxCost)
        {
            spreadCooldownCost = Mathf.RoundToInt(spreadCooldownCost * 1.8f);

        }
        else
        {
            spreadCooldownCost += spreadCooldownCost / 10;
        }
        UpdateSpreadCooldownButton();
    }

    private void UpdateSpreadCooldownButton()
    {
        if (AttackManager.Instance.spreadCooldownDuration <= 3)
        {
            spreadCooldownDescription.text = "Cooldown : " + AttackManager.Instance.spreadCooldownDuration.ToString();
            spreadCooldownCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.spreadCooldownDuration - 1;
        spreadCooldownDescription.text = "Cooldown : " + AttackManager.Instance.spreadCooldownDuration.ToString() + " -> " + newStat.ToString();
        spreadCooldownCostText.text = "Cost : " + spreadCooldownCost.ToString();
    }
    #endregion

    #region BlastDuration
    public void OnBlastDurationUpgrade()
    {
        if (CoinManager.Instance.Coins >= blastDurationCost)
        {
            if (!AttackManager.Instance.IncreaseBlastDuration(0.5f))
            {
                return;
            }
            CoinManager.Instance.Coins -= blastDurationCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (blastDurationCost < blastDurationMaxCost)
        {
            blastDurationCost = Mathf.RoundToInt(blastDurationCost * 1.7f);

        }
        else
        {
            blastDurationCost += blastDurationCost / 15;
        }
        UpdateBlastDurationButton();
    }

    private void UpdateBlastDurationButton()
    {
        if (AttackManager.Instance.aoeSkillDuration >= 8)
        {
            blastDurationDescription.text = "Duration : " + AttackManager.Instance.aoeSkillDuration.ToString();
            blastDurationCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.aoeSkillDuration + 0.5f;
        blastDurationDescription.text = "Duration : " + AttackManager.Instance.aoeSkillDuration.ToString() + " -> " + newStat.ToString();
        blastDurationCostText.text = "Cost : " + blastDurationCost.ToString();
    }
    #endregion

    #region BlastCooldown
    public void OnBlastCooldownUpgrade()
    {
        if (CoinManager.Instance.Coins >= blastCooldownCost)
        {
            if (!AttackManager.Instance.DecreaseBlastCooldownDuration(2))
            {
                return;
            }
            CoinManager.Instance.Coins -= blastCooldownCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (blastCooldownCost < blastCooldownMaxCost)
        {
            blastCooldownCost = Mathf.RoundToInt(blastCooldownCost * 1.8f);

        }
        else
        {
            blastCooldownCost += blastCooldownCost / 10;
        }
        UpdateBlastCooldownButton();
    }

    private void UpdateBlastCooldownButton()
    {
        if (AttackManager.Instance.aoeCooldownDuration <= 6)
        {
            blastCooldownDescription.text = "Cooldown : " + AttackManager.Instance.aoeCooldownDuration.ToString();
            blastCooldownCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.aoeCooldownDuration - 2;
        blastCooldownDescription.text = "Cooldown : " + AttackManager.Instance.aoeCooldownDuration.ToString() + " -> " + newStat.ToString();
        blastCooldownCostText.text = "Cost : " + blastCooldownCost.ToString();
    }
    #endregion

    #region FreezeDuration
    public void OnFreezeDurationUpgrade()
    {
        if (CoinManager.Instance.Coins >= freezeDurationCost)
        {
            if (!AttackManager.Instance.IncreaseFreezeDuration(0.5f))
            {
                return;
            }
            CoinManager.Instance.Coins -= freezeDurationCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (freezeDurationCost < freezeDurationMaxCost)
        {
            freezeDurationCost = Mathf.RoundToInt(freezeDurationCost * 1.7f);

        }
        else
        {
            freezeDurationCost += freezeDurationCost / 15;
        }
        UpdateFreezeDurationButton();
    }

    private void UpdateFreezeDurationButton()
    {
        if (AttackManager.Instance.freezeSkillDuration >= 8)
        {
            freezeDurationDescription.text = "Duration : " + AttackManager.Instance.freezeSkillDuration.ToString();
            freezeDurationCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.freezeSkillDuration + 0.5f;
        freezeDurationDescription.text = "Duration : " + AttackManager.Instance.freezeSkillDuration.ToString() + " -> " + newStat.ToString();
        freezeDurationCostText.text = "Cost : " + freezeDurationCost.ToString();
    }
    #endregion

    #region FreezeCooldown
    public void OnFreezeCooldownUpgrade()
    {
        if (CoinManager.Instance.Coins >= freezeCooldownCost)
        {
            if (!AttackManager.Instance.DecreaseFreezeCooldownDuration(2))
            {
                return;
            }
            CoinManager.Instance.Coins -= freezeCooldownCost;
        }
        else
        {
            Debug.Log("Not Enought Money");
            return;
        }

        if (freezeCooldownCost < freezeCooldownMaxCost)
        {
            freezeCooldownCost = Mathf.RoundToInt(freezeCooldownCost * 1.8f);

        }
        else
        {
            freezeCooldownCost += freezeCooldownCost / 10;
        }
        UpdateFreezeCooldownButton();
    }

    private void UpdateFreezeCooldownButton()
    {
        if (AttackManager.Instance.freezeCooldownDuration <= 6)
        {
            freezeCooldownDescription.text = "Cooldown : " + AttackManager.Instance.freezeCooldownDuration.ToString();
            freezeCooldownCostText.text = "Maxed";
            return;
        }
        float newStat = AttackManager.Instance.freezeCooldownDuration - 2;
        freezeCooldownDescription.text = "Cooldown : " + AttackManager.Instance.freezeCooldownDuration.ToString() + " -> " + newStat.ToString();
        freezeCooldownCostText.text = "Cost : " + freezeCooldownCost.ToString();
    }
    #endregion
}
