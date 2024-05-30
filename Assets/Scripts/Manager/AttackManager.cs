using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : Singleton<AttackManager>
{
    [Header("General Setting")]
    [SerializeField] public int damage;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float acceleration = 0f;

    [SerializeField] public Vector3 scale;


    /*[Header("Horizontal Movement Settings")]
    [SerializeField] public bool canHorizontal;
    [SerializeField] public float horizontalSpeed = 5f;
    [SerializeField] public float horizontalDistance = 2f;
    */
    [Header("Horizontal Settings")]
    [SerializeField] public bool canHorinzontal;

    [Header("Pierce Settings")]
    public bool pierce;
    public float pierceSkillDuration;
    public float pierceCooldownDuration;

    [Header("Spread Attack Settings")]
    public bool canSpread;
    public float spreadSkillDuration;
    public float spreadCooldownDuration;
    public int numberOfProjectiles = 5;
    public float spreadAngle = 45f;

    [Header("AOE Settings")]
    public bool canAOE;
    public float aoeSkillDuration;
    public float aoeCooldownDuration;
    public float explosionRadius;
    public int explosionDamage;

    [Header("Freeze Settings")]
    public bool canFreeze;
    public float freezeSkillDuration;
    public float freezeCooldownDuration;
    public float freezeDuration = 1f;

    [Header("Settings")]
    public float bulletSpawnDistance = 1f;

    [Header("UIs")]
    public Button pierceButton;
    public Image pierceCooldownImage;

    public Button spreadButton;
    public Image spreadCooldownImage;

    public Button aoeButton;
    public Image aoeCooldownImage;

    public Button freezeButton;
    public Image freezeCooldownImage;



    // Start is called before the first frame update
    void Start()
    {
        pierceButton.onClick.AddListener(() => StartCoroutine(ActivateSkill("pierce")));
        spreadButton.onClick.AddListener(() => StartCoroutine(ActivateSkill("spread")));
        aoeButton.onClick.AddListener(() => StartCoroutine(ActivateSkill("aoe")));
        freezeButton.onClick.AddListener(() => StartCoroutine(ActivateSkill("freeze")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
    private IEnumerator ActivateSkill(string skill)
    {
        switch (skill)
        {
            case "pierce":
                pierce = true;
                pierceButton.interactable = false;
                StartCoroutine(Cooldown(pierceCooldownImage, pierceSkillDuration, pierceCooldownDuration));
                yield return new WaitForSeconds(pierceSkillDuration);
                pierce = false;
                yield return new WaitForSeconds(pierceCooldownDuration);
                pierceButton.interactable = true;
                break;

            case "spread":
                canSpread = true;
                spreadButton.interactable = false;
                StartCoroutine(Cooldown(spreadCooldownImage, spreadSkillDuration, spreadCooldownDuration));
                yield return new WaitForSeconds(spreadSkillDuration);
                canSpread = false;
                yield return new WaitForSeconds(spreadCooldownDuration);
                spreadButton.interactable = true;
                break;

            case "aoe":
                canAOE = true;
                aoeButton.interactable = false;
                StartCoroutine(Cooldown(aoeCooldownImage, aoeSkillDuration, aoeCooldownDuration));
                yield return new WaitForSeconds(aoeSkillDuration);
                canAOE = false;
                yield return new WaitForSeconds(aoeCooldownDuration);
                aoeButton.interactable = true;
                break;

            case "freeze":
                canFreeze = true;
                freezeButton.interactable = false;
                StartCoroutine(Cooldown(freezeCooldownImage, freezeSkillDuration, freezeCooldownDuration));
                yield return new WaitForSeconds(freezeSkillDuration);
                canFreeze = false;
                yield return new WaitForSeconds(freezeCooldownDuration);
                freezeButton.interactable = true;
                break;
        }
    }

    private IEnumerator Cooldown(Image cooldownImage, float skillDuration, float cooldownDuration)
    {
        // Activate the cooldown image
        cooldownImage.gameObject.SetActive(true);

        // Skill duration phase
        cooldownImage.fillAmount = 1;
        float elapsedTime = 0;

        while (elapsedTime < skillDuration)
        {
            cooldownImage.fillAmount = elapsedTime / skillDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Cooldown duration phase
        cooldownImage.fillAmount = 1;
        elapsedTime = 0;

        while (elapsedTime < cooldownDuration)
        {
            cooldownImage.fillAmount = 1 - (elapsedTime / cooldownDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Deactivate the cooldown image
        cooldownImage.gameObject.SetActive(false);
    }
}
