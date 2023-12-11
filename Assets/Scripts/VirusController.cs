using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 2.5f;
     Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
         rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(0.0f,move.y))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += (speed * horizontal * Time.deltaTime);
        position.y += (speed * vertical * Time.deltaTime);
        rb2d.MovePosition(position);
    }

}
