using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DropItem : MonoBehaviour
{
    [Header("Reward Position")]
    [SerializeField] public bool canDropItem;
    [SerializeField] public bool dropMultipleItem;


    [SerializeField] private float xRandomPosition = 2f;
    [SerializeField] private float yRandomPosition = 2f;
    [SerializeField] private float spreadForce = 2f;


    [SerializeField] private GameObject[] rewards;
    [SerializeField] private int RewardMinAmount;
    [SerializeField] private int RewardMaxAmount;


    private Vector3 rewardRandomPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropOneItem()
    {
        InstantiateAndSpreadReward(SelectOneReward());
    }

    private GameObject SelectOneReward()
    {
        int randomRewardIndex = Random.Range(0, rewards.Length);
        for (int i = 0; i < rewards.Length; i++)
        {
            return rewards[randomRewardIndex];
        }

        return null;
    }
    public void DropMultipleItem()
    {
        GameObject[] selectedRewards = SelectMultipleReward();
        foreach (GameObject reward in selectedRewards)
        {
            InstantiateAndSpreadReward(reward);
        }
    }

    private GameObject[] SelectMultipleReward()
    {
        int RewardAmount = Random.Range(RewardMinAmount, RewardMaxAmount); ;
        GameObject[] selectedRewards = new GameObject[RewardAmount];

        int randomRewardIndex = Random.Range(0, rewards.Length);
        for (int i = 0; i < RewardAmount; i++)
        {
            selectedRewards[i] = rewards[randomRewardIndex];
        }

        return selectedRewards;
    }

    private void InstantiateAndSpreadReward(GameObject rewardPrefab)
    {
        
        GameObject rewardInstance = Instantiate(rewardPrefab, transform.position, Quaternion.identity);

        // Add Rigidbody component if not already present
        Rigidbody2D rb = rewardInstance.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = rewardInstance.AddComponent<Rigidbody2D>();
        }

        // Apply force to spread the coins around
        Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(forceDirection * spreadForce, ForceMode2D.Impulse);
    }
}
