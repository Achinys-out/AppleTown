using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Character animation:")]
    public Animator animator;
    [Space]
    [Header("Character attributes:")]
    public Vector2 movement;
    public float speed = 7.0f;
    private int health = 6;
    public float movementSpeed;
    private float lastDamagedTime;
    private float invincibilityLength = 0;
    private Vector2 initPosition;
    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public GameObject healthStatus;

    private void Awake()
    {
        initPosition = transform.position;
        lastDamagedTime = Time.time;
        healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");
    }
    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        movementSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 0.0f);
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile") {
            healthDown();
        }
    }

    public void healthDown() {

        if (health > 0)
        {

            if (lastDamagedTime + invincibilityLength < Time.time)
            {
                lastDamagedTime = Time.time;
                health -= 1;
                Debug.Log("-1 HEALTH");
                // damage animation anbd sound
                healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");
            }
        }
        else {

            // player is dead, do respawn
            transform.position = initPosition;
            health = 6;
            healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");

        }
        
    }
    public void healthUp()
    {
        if (health < 6) {
            health += 1;
            healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");
        }
        

    }


}
