using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBase : MonoBehaviour
{
    protected Transform playerTransform;

    protected Rigidbody2D myRigidbody2D;
    protected Collider2D myCollider2D;
    protected SpriteRenderer spriteRenderer;

    protected Vector3 Direction { get; set; }

    protected Vector2 movement;
    protected Vector2 forwardMovement;
    protected Vector2 horizontalMovement;

    [Header("General Movement Settings")]
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float acceleration = 0;

    protected bool canMove;

    protected virtual void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FixPositionRotation();
    }

    protected virtual void Start()
    {
        FixPositionRotation();
    }

    private void Update()
    {
        
    }

    private void FixPositionRotation()
    {

        Direction = (transform.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void DisableEnemy()
    {
        canMove = false;
        spriteRenderer.enabled = false;  // If we donÅft disable the spriteRenderer, the bullet will fall down before disappear
        myCollider2D.enabled = false;
    }

    public void EnableEnemy()
    {
        FixPositionRotation();
        canMove = true;
        spriteRenderer.enabled = true;
        myCollider2D.enabled = true;
        /*
        Direction = characterAttack.direction;
        SpawnPosition = characterAttack.ProjectileSpawnPosition;

        SetPositionAndRotation();
        this.gameObject.SetActive(true);*/


    }
}
