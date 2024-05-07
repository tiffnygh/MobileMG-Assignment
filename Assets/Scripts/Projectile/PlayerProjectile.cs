using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private int damage;

    [Header("General Movement Settings")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    [Header("Horizontal Movement Settings")]
    [SerializeField] bool canHorizontal;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float horizontalDistance = 2f;

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

    private Health enemyHealth;

    private Vector2 movement;
    private Vector2 forwardMovement;
    private Vector2 horizontalMovement;
    private float horizontalAmount;
    private bool canMove;

    private float elapsedTime = 0; // Time elapsed for oscillation calculation


    private void Awake()
    {
        Damage = damage;
        Speed = speed;

        characterAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAttack>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveProjectile();
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

    // Moves this projectile  
    public void MoveProjectile()
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


        //Sum of movement 
        movement = forwardMovement + horizontalMovement;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;

    }

    private void SetPositionAndRotation()
    {
        transform.position = SpawnPosition;
        transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
    }


    public void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;  // If we donÅft disable the spriteRenderer, the bullet will fall down before disappear
        myCollider2D.enabled = false;
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        myCollider2D.enabled = true;
        Direction = characterAttack.direction;
        SpawnPosition = characterAttack.ProjectileSpawnPosition;

        SetPositionAndRotation();
        this.gameObject.SetActive(true);


    }

}
