using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Fence") || collision.CompareTag("House") || collision.CompareTag("Tree") || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy") && Vector2.Distance(collision.transform.position, transform.position) < 2f) {
                Destroy(collision);
                Destroy(gameObject);
            }
            
        }
        
    }
}
