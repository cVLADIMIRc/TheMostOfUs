using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float walkSpeed = 2f;
    public float runSpeed = 10f;
    public float jumpForce;
    public float damage;
    private bool hasGun = false;
    private Gun currentGun;

    private bool isGrounded;
    private bool isRunning;
    private new Rigidbody2D rigidbody2D; // »спользуем new, чтобы указать, что это нова€ переменна€
    private Collider2D playerCollider;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (hasGun && Input.GetKeyDown(KeyCode.Tab))
        {
            FireGun();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        Die();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 position = transform.position;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        position.x += Input.GetAxis("Horizontal") * currentSpeed * Time.fixedDeltaTime;
        transform.position = position;
    }

    void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void PickupGun(Gun gun)
    {
        hasGun = true;
        currentGun = gun;
        currentGun.transform.parent = transform;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.gameObject.SetActive(false);
    }

    void FireGun()
    {
        if (currentGun != null)
        {
            currentGun.gameObject.SetActive(true);
            currentGun.Fire(true);
            currentGun.PickupGun();
            currentGun.gameObject.SetActive(false);
        }
    }
}
