using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    float timeConstant = 3;
    public Animator animator;
    public Rigidbody2D rb;
    public Vector3 movement;


    private void Move() 
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        rb.velocity = new Vector2(movement.x * timeConstant, movement.y * timeConstant);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
