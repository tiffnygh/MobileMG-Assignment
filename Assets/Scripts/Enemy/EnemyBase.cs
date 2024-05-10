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

    protected Vector2 Direction;

    protected Vector2 movement;
    protected Vector2 forwardMovement;
    protected Vector2 horizontalMovement;
    protected Vector2 zigzagMovemnet;
    protected Vector2 spiralMovement;

    [Header("General Settings")]
    [SerializeField] protected bool canForward;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float acceleration = 0.03f;


    [Header("Horizontal Movement Settings")]
    [SerializeField] protected bool canHorizontal;
    [SerializeField] protected float horizontalSpeed = 5f;
    [SerializeField] protected float horizontalDistance = 2f;

    [Header("ZigZag Movement Settings")]
    [SerializeField] protected bool canZigZag;
    [SerializeField] protected float zigzagChangeTime = 2.0f;
    [SerializeField] protected float nextChangeTime = 0;

    [Header("Spiral Movement Settings")]
    [SerializeField] protected bool canSpiral;
    [SerializeField] protected bool repeatSpiral;
    protected bool negativeSpiral;
    [SerializeField] protected float spiralAngle = 0f; // Current angle of the spiral movement
    [SerializeField] protected float spiralSpeed = 30f; // Speed at which the spiral rotates, degrees per second
    [SerializeField] protected float spiralRadius = 1f; // Initial radius of the spiral from the player
    [SerializeField] protected float spiralExpansionRate = 0.1f; // Rate at which the spiral radius increases per second

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
        FixPositionRotation();
    }

    private void FixPositionRotation()
    {

        Direction = (transform.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    public void StartSpiral()
    {
        if (repeatSpiral)
        {
            CheckSpiral();
        }

        // Calculate the initial position offset from the player to the enemy
        Vector2 startPositionOffset = transform.position - playerTransform.position;

        // Calculate the initial angle from this offset
        spiralAngle = Mathf.Atan2(startPositionOffset.y, startPositionOffset.x) * Mathf.Rad2Deg;

        // Ensure the angle is in the range [0, 360)
        if (spiralAngle < 0) spiralAngle += 360f;

        // Optionally set an initial radius based on the current distance to the player
        spiralRadius = startPositionOffset.magnitude;
    }

    public void CheckSpiral()
    {
        if (spiralRadius >= 5.0)
        {
            negativeSpiral = true;
        }
        else if (spiralRadius <= 0.5)
        {
            negativeSpiral = false;
        }
    }

    public void DisableEnemy()
    {
        canMove = false;
        spriteRenderer.enabled = false;  // If we donft disable the spriteRenderer, the bullet will fall down before disappear
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
