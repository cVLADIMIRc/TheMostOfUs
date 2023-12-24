using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public float health;
    public float speed;
    public float jumpForce;
    public float damage;

    public bool isGrounded;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		die();
	}
	private void FixedUpdate()
	{
		Vector2 position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed;
        //position.y += Input.GetAxis("Vertical");

        transform.position = position;  
	}
    public void Jump()
    {
        if (isGrounded) 
        {
            isGrounded = false;
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
	}
    public void die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
