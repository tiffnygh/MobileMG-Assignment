using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] public int damage = 0;
    [SerializeField] public int point = 0;



    [Header("Settings")]
    [SerializeField] private bool destroyObject;

    private Character character;
    private Collider2D myCollider2D;
    private SpriteRenderer spriteRenderer;
    private DropItem dropItem;

    private EnemyMovement enemyMovement;

    private bool isPlayer;

    // Controls the current health of the object    
    public float CurrentHealth { get; set; }

    private void Awake()
    {
        character = GetComponent<Character>();
        myCollider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        dropItem = GetComponent<DropItem>();


        CurrentHealth = initialHealth;

        if (character != null)
        {
            isPlayer = character.CharacterType == Character.CharacterTypes.Player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= damage;
        DamageColorFeedback();

        //UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield, isPlayer);
        //UpdateCharacterHealth();

        if (CurrentHealth <= 0)
        {
            Die();
            ScoreManager.Instance.AddScore(point);
            SoundManager.Instance.PlaySound(SoundManager.Instance.EnemyDeadClip, 1f);
        }

    }

    public void Revive()
    {
        if (character != null)
        {
            spriteRenderer.color = Color.white;

            myCollider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            enemyMovement.enabled = true;
            CurrentHealth = initialHealth;

        }
    }
    public void Die()
    {
        if (character != null)
        {
            myCollider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            enemyMovement.enabled = false;
        }

        if (character.CharacterType == Character.CharacterTypes.AI && dropItem.canDropItem)
        {
            if (dropItem.dropMultipleItem)
            {
                dropItem.DropMultipleItem();
            }
        }
        /*
        if (bossBaseShot != null)
        {
            OnBossDead?.Invoke();
        }*/
         


        if (destroyObject)
        {
            DestroyObject();
        }


    }

    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth); //Logic if full HP, cannot add anymore
        //UpdateCharacterHealth();
    }

    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void DamageColorFeedback()
    {
        StartCoroutine(ChangeColorForASecond(Color.red, 0.05f, 0f));
    }

    public void FreezeColorFeedback()
    {
        StartCoroutine(ChangeColorForASecond(Color.cyan, AttackManager.Instance.freezeDuration, 0.06f));
    }

    IEnumerator ChangeColorForASecond(Color newColor, float second, float freezeDelay)
    {
        yield return new WaitForSeconds(freezeDelay);

        // Change the sprite renderer color to the new color
        spriteRenderer.color = newColor;

        // Wait for 1 second
        yield return new WaitForSeconds(second);

        // Change the sprite renderer color back to the original color
        spriteRenderer.color = Color.white;
    }

}
