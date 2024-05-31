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
        /*
         blastDurationUpgradeButton.onClick.AddListener(OnBlastDurationUpgrade);
         blastCooldownUpgradeButton.onClick.AddListener(OnBlastCooldownUpgrade);
         freezeDurationUpgradeButton.onClick.AddListener(OnFreezeDurationUpgrade);
         freezeCooldownUpgradeButton.onClick.AddListener(OnFreezeCooldownUpgrade);
         */
        UpdatePierceDurationButton();
        UpdatePierceCooldownButton();

        UpdateSpreadDurationButton();
        UpdateSpreadCooldownButton();
        /*
        UpdateBlastDurationButton();
        UpdateBlastCooldownButton();
        UpdateFreezeDurationButton();
        UpdateFreezeCooldownButton();
        */
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
            spreadDurationCost = Mathf.RoundToInt(spreadDurationCost * 1.5f);

        }
        else
        {
            spreadDurationCost += spreadDurationCost / 20;
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

    #endregion

    #region BlastCooldown

    #endregion

    #region FreezeDuration

    #endregion

    #region FreezeCooldown

    #endregion
}
