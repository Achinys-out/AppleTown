using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Vector2 target;
    private bool rotating = true;
    private float vectorExtend = 15.0f;

    

    private void Start()
    {
        player = PlayerManager.instance.player;
        target = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector3 normVector = (player.transform.position - transform.position).normalized;
        target = transform.position + normVector * vectorExtend;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        
        var dist = Vector2.Distance(transform.position, target);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(rotating) transform.Rotate(0, 0, 360 * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y) {
            //player.healthDown(1);
            rotating = false;
            Destroy(gameObject, 2.0f);
        }
        
    }


}
