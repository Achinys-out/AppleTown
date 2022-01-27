using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float radius; // also used as stopDistance
    public int speed;
    public bool range;
    public int damage;
    public float retreatDistance;
    public float startTimeShots;
    public Vector2 movement;
    private float timeBetweenShots;
    private bool alreadyInRange = false;
    private float floatTolerance = 0.25f;
    private float DISTANCE_TO_STOP = 12f;
    private Transform target;
    public GameObject projectile;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        timeBetweenShots = startTimeShots;
    }

    void Update()
    {
        var tmpDistance = Vector2.Distance(target.position, transform.position);

        if (tmpDistance < DISTANCE_TO_STOP &&  (tmpDistance < radius || alreadyInRange || !range)) {

            alreadyInRange = true;

            if (tmpDistance > radius || !range)
            {
                animator.speed = 1;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (tmpDistance < radius + floatTolerance && tmpDistance > retreatDistance - floatTolerance)
            {
                transform.position = this.transform.position;
                animator.speed = 0;
            }
            else if (tmpDistance < retreatDistance)
            {
                animator.speed = 1;
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }

            if (range) {
                if (timeBetweenShots <= 0)
                {
                    GameObject tmp = Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBetweenShots = startTimeShots;
                    Destroy(tmp, 3.5f);
                }
                else timeBetweenShots -= Time.deltaTime;
            }
            
            if (tmpDistance < radius) Animate();

        }


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") && Vector2.Distance(collision.transform.position, transform.position) < 2f)
        {
            Destroy(collision);
            Destroy(gameObject);
        }

    }
    private void Animate()
    {
        Vector3 posA = transform.position;
        Vector3 posB = target.position;
        Vector2 movement = (posB - posA).normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }
}
