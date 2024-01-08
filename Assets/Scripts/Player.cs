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
	public Joystick joystick;

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(joystick.Vertical > 0.5)
		{
			Jump();
		}
		die();
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(joystick.Horizontal * speed, rigidbody2D.velocity.y);
	}
	public void Jump()
	{
		if (isGrounded && joystick.Vertical >= 0.5f)
		{
			isGrounded = false;
			rigidbody2D.velocity = Vector2.up * jumpForce;
		}
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}
	public void die()
	{
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}