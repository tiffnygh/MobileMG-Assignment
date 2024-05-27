using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Barrier Health")]
    [SerializeField] private Image topHealthBar;
    [SerializeField] private Image bottomHealthBar;
    [SerializeField] private Image leftHealthBar;
    [SerializeField] private Image rightHealthBar;

    [SerializeField] private TextMeshProUGUI topCurrentHealthTMP;
    [SerializeField] private TextMeshProUGUI bottomCurrentHealthTMP;
    [SerializeField] private TextMeshProUGUI leftCurrentHealthTMP;
    [SerializeField] private TextMeshProUGUI rightCurrentHealthTMP;


    private int barrierMaxHealth;
    private int topBarrierHealth;
    private int bottomBarrierHealth;
    private int leftBarrierHealth;
    private int rightBarrierHealth;




    private void Update()
    {
        InternalUpdate();
    }

    public void UpdateBarrierHealth(int maxHealth, int topHealth, int bottomHealth, int leftHealth, int rightHealth)
    {
        barrierMaxHealth = maxHealth;
        topBarrierHealth = topHealth;   
        bottomBarrierHealth = bottomHealth;
        leftBarrierHealth = leftHealth;
        rightBarrierHealth = rightHealth;
    }


    private void InternalUpdate()
    {
        topHealthBar.fillAmount = Mathf.Lerp(topHealthBar.fillAmount, (float)topBarrierHealth/barrierMaxHealth, 10f * Time.deltaTime);        
        bottomHealthBar.fillAmount = Mathf.Lerp(bottomHealthBar.fillAmount, (float)bottomBarrierHealth /barrierMaxHealth, 10f * Time.deltaTime);
        leftHealthBar.fillAmount = Mathf.Lerp(leftHealthBar.fillAmount, (float)leftBarrierHealth /barrierMaxHealth, 10f * Time.deltaTime);
        rightHealthBar.fillAmount = Mathf.Lerp(rightHealthBar.fillAmount, (float)rightBarrierHealth /barrierMaxHealth, 10f * Time.deltaTime);
        Debug.Log(topHealthBar.fillAmount);
        Debug.Log(topBarrierHealth);
        Debug.Log(barrierMaxHealth);


        topCurrentHealthTMP.text = topBarrierHealth.ToString() + "/" + barrierMaxHealth.ToString();
        bottomCurrentHealthTMP.text = bottomBarrierHealth.ToString() + "/" + barrierMaxHealth.ToString();
        leftCurrentHealthTMP.text = leftBarrierHealth.ToString() + "/" + barrierMaxHealth.ToString();
        rightCurrentHealthTMP.text = rightBarrierHealth.ToString() + "/" + barrierMaxHealth.ToString();

        // Update Coins
        //coinsTMP.text = CoinManager.Instance.Coins.ToString();
    }
    /*
    private void OnBossDead()
    {
        StartCoroutine(MyLibrary.FadeCanvasGroup(bossHealthBarPanel.GetComponent<CanvasGroup>(), 1f, 0f, () =>
        {
            bossHealthBarPanel.SetActive(false);
        }));
    }

    private void OnEventResponse(GameEvent.EventType obj)
    {
        switch (obj)
        {
            case GameEvent.EventType.BossFight:
                StartCoroutine(BossFight());
                break;
        }
    }

    private void OnEnable()
    {
        GameEvent.OnEventFired += OnEventResponse;
        Health.OnBossDead += OnBossDead;
    }

    private void OnDisable()
    {
        GameEvent.OnEventFired -= OnEventResponse;
        Health.OnBossDead -= OnBossDead;
    }*/

}



