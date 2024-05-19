using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] protected int damage;

    [Header("General Movement Settings")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    [Header("Horizontal Movement Settings")]
    [SerializeField] public bool canHorizontal;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float horizontalDistance = 2f;

    [Header("Spiral Movement Settings")]
    [SerializeField] protected bool canSpiral;
    [SerializeField] protected bool repeatSpiral;
    protected bool negativeSpiral;
    private Vector3 initialMousePosition;
    [SerializeField] protected float spiralAngle = 0f; // Current angle of the spiral movement
    [SerializeField] protected float spiralSpeed = 30f; // Speed at which the spiral rotates, degrees per second
    [SerializeField] protected float spiralRadius = 1f; // Initial radius of the spiral from the player
    [SerializeField] protected float spiralExpansionRate = 0.1f; // Rate at which the spiral radius increases per second

    private Transform playerTransform;

    // Returns the direction of this projectile    
    public Vector3 Direction { get; set; }

    public Vector3 SpawnPosition { get; set; }

    // Returns the speed of the projectile    
    public float Speed { get; set; }

    public int Damage { get; set; }



    // Internal
    private CharacterAttack characterAttack;
    private Rigidbody2D myRigidbody2D;
    private Collider2D myCollider2D;
    private SpriteRenderer spriteRenderer;

    private ReturnToPool returnToPool;

    private Health enemyHealth;

    private Vector2 movement;
    protected Vector2 forwardMovement;
    protected Vector2 horizontalMovement;
    protected Vector2 spiralMovement;
    private float horizontalAmount;
    private bool canMove;

    private float elapsedTime = 0; // Time elapsed for oscillation calculation


    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Damage = damage;
        Speed = speed;

        characterAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
        returnToPool = GetComponent<ReturnToPool>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveProjectile();
            FixRotation();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject.layer != 9) //PlayerProjectile layer
        {
            enemyHealth = collision.GetComponent<Health>();
            enemyHealth.TakeDamage(Damage);
            //SoundManager.Instance.PlaySound(SoundManager.Instance.ImpactClip, 0.1f);
        }
    }

    //-----------------------------------------------------MOVEMENT CALCULATION SECTION-----------------------------------------------------------
    public virtual void MoveProjectile()
    {
        //Forward Movement
        forwardMovement = Direction * (Speed / 10f) * Time.fixedDeltaTime;

        //Horizontal Movement
        //horizontalAmount = horizontalSpeed * Time.fixedDeltaTime;
        //horizontalMovement = Direction.normalized * Mathf.Sin(horizontalAmount) * horizontalDistance;
        if (canHorizontal)
        {
            elapsedTime += Time.fixedDeltaTime;
            float horizontalAmount = Mathf.Sin(elapsedTime * horizontalSpeed) * horizontalDistance;
            Vector2 rightVector = new Vector2(-Direction.y, Direction.x).normalized; // Perpendicular to the main direction
            horizontalMovement = rightVector * horizontalAmount;
        }
        else
        {
            horizontalMovement = new Vector2(0, 0);
        }

        if (canSpiral)
        {
            StartSpiral();
            // Update the spiral angle and radius
            spiralAngle += spiralSpeed * Time.deltaTime;
            if (negativeSpiral)
            {
                spiralRadius -= spiralExpansionRate * Time.deltaTime;
            }
            else
            {
                spiralRadius += spiralExpansionRate * Time.deltaTime;
            }

            // Calculate the new position in the spiral
            Vector2 spiralOffset = new Vector2(Mathf.Cos(spiralAngle * Mathf.Deg2Rad), Mathf.Sin(spiralAngle * Mathf.Deg2Rad)) * spiralRadius;
            Vector2 nextPosition = (Vector2)playerTransform.position + spiralOffset;

            spiralMovement = nextPosition - (Vector2)transform.position;

            // Normalize and scale the movement by the current speed and deltaTime
            spiralMovement = spiralMovement.normalized * (Speed * Time.deltaTime);
        }
        else
        {
            spiralMovement = new Vector2(0, 0);
        }


        //Sum of movement 
        movement = forwardMovement + horizontalMovement + spiralMovement;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;

    }

    public void StartSpiral()
    {
        if (repeatSpiral)
        {
            CheckSpiral();
        }

        Vector2 startPositionOffset = transform.position - playerTransform.position;

        spiralAngle = Mathf.Atan2(startPositionOffset.y, startPositionOffset.x) * Mathf.Rad2Deg;

        if (spiralAngle < 0) spiralAngle += 360f;

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

    //-----------------------------------------------------FIX ROTATION SECTION-----------------------------------------------------------
    private void SetPositionAndRotation()
    {
        transform.position = Direction;
        transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
    }

    private void FixRotation()
    {

        Direction = (transform.position - playerTransform.position).normalized;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    //------------------------------------------------------ENABLE & DISABLE SECTION------------------------------------------------------------
    public virtual void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;  // If we donft disable the spriteRenderer, the bullet will fall down before disappear
        myCollider2D.enabled = false;
    }

    public virtual void EnableProjectile()
    {
        if (canSpiral)
        {
            SetPositionAndRotation();
        }
        canMove = true;
        spriteRenderer.enabled = true;
        myCollider2D.enabled = true;

        this.gameObject.SetActive(true);
    }   

}
