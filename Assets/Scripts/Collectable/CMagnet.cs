using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CMagnet : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private float delay = 1f;

    // Returns the speed of the gameObject   
    public float Speed { get; set; }
    public Vector2 Direction { get; set; }


    private GameObject player;
    private Vector2 movement;
    private bool canMove;

    private Rigidbody2D rg2d;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rg2d = GetComponent<Rigidbody2D>();
        StartCoroutine(StartMagnet());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            canMove = true;
        }
    }*/

    private void MoveProjectile()
    {
        if (canMove)
        {
            Vector3 newDirection = (player.transform.position - transform.position).normalized;
            SetDirection(newDirection);

            movement = Direction * (speed) / 10;
            rg2d.MovePosition(rg2d.position + movement);

            speed += acceleration * Time.deltaTime;

        }


            /*Vector3 projectileAngle = player.transform.rotation.eulerAngles;
            Quaternion newRotation = thisTransform.rotation;
            Vector3 playerPosition = player.transform.position;

            // Set Rotation
            float angleToAdd = acceleration * Time.deltaTime;
            newRotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, projectileAngle.z + angleToAdd);

            // Apply acceleration
            speed += acceleration * Time.deltaTime;

            // Move
            Vector3 newPosition = thisTransform.position + newDirection * (speed * Time.deltaTime);
            thisTransform.SetPositionAndRotation(newPosition, newRotation);*/
        
    }

    public IEnumerator StartMagnet()
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
    public void SetDirection(Vector2 newDirection)
    {
        Direction = newDirection;
    }

}
