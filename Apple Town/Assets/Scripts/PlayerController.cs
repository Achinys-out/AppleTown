using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Vector2 movement;
    public float speed;
    private int health = 4;
    public float movementSpeed;
    private float lastDamagedTime;
    private float invincibilityLength = 0;
    private Vector2 initPosition;
    public Rigidbody2D rb;
    public GameObject healthStatus;
    public GameObject projectile;
    public AudioSource audioSource;
    private float shootLastPressed;
    public KeyCode interactKey;
    public AudioSource ouchSound;
    public AudioSource hitSound;


    private void Awake()
    {
        initPosition = transform.position;
        lastDamagedTime = Time.time;
        healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");

    }
    private Vector2 Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        movementSpeed = Mathf.Clamp(movement.magnitude, 0.0f, 0.0f) * Time.deltaTime;
        
        
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        if (rb.velocity.magnitude < 0.2f) audioSource.Play(0);
        return movement;
    }

    void Update()
    {
        Vector2 movement = Move();
        if (Input.GetKey(interactKey))
        {
            if (shootLastPressed + 0.35f + Random.Range(-0.1f, 0.2f) < Time.time) { 
                Attack(movement);
                shootLastPressed = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<MainMenu>().OpenMenu();
        }

    }

    private void Attack(Vector2 movement) {
        Vector2 shootingDirection = Vector2.down;
        if (Input.GetKey("a") || Input.GetKey("left")) shootingDirection = Vector2.left;
        if (Input.GetKey("s") || Input.GetKey("down")) shootingDirection = Vector2.down;
        if (Input.GetKey("d") || Input.GetKey("right")) shootingDirection = Vector2.right;
        if (Input.GetKey("w") || Input.GetKey("up")) shootingDirection = Vector2.up;


        //if(movement.x != 0 && movement.y != 0) shootingDirection = movement;
        // shootingDirection.Normalize();
        if (movement.x == 0 && movement.y == 0) movement = Vector2.down;

        GameObject tmpProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 newVelocity = movement * 10;
        tmpProjectile.GetComponent<Rigidbody2D>().velocity = newVelocity;
        Destroy(tmpProjectile, 1.5f);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag) {
            case "Projectile": {
                    healthDown(); 
                    break;
                } 
            case "FullAppleHealth": case "HalfAppleHealth": {
                    if (health < 6) {
                        other.GetComponent<SpriteRenderer>().enabled = false;
                        Destroy(other.gameObject, .4f);
                        if (other.gameObject.tag == "HalfAppleHealth") healthUp(1);
                        else healthUp(2);
                        other.GetComponent<AudioSource>().Play(0);
                    }
                    break;
                }
            case "Enemy":
                {
                    other.gameObject.GetComponent<AudioSource>().Play(0);
                    break;
                }
            default: break;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy") collision.gameObject.GetComponent<AudioSource>().Stop();
    }

    public void healthDown() {

        ouchSound.Play(0);
        hitSound.Play(0);
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
            var gm = gameObject.GetComponent<InitEnemies>();
            gm.resetEnemies();

        }
        
    }
    public void healthUp(int amount)
    {
        if (health < 6) {
            health += amount;
            if (health > 6) health = 6;
            healthStatus.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Health/" + health + "_HEALTH");
        }
        

    }


}
