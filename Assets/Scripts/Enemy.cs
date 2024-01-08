using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health;
    public float walkSpeed = 0.2f;
    public float runSpeed = 5f;
    public float jumpForce;
    public float damage;

    public bool isGrounded;
    private Rigidbody2D rigidbody2D;
    private bool isRunning = false;

    // Reference to the player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        die();

        // Check if the player is in sight
        if (CanSeePlayer())
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
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
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            hitPlayer();
        }
    }

    public void die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hitPlayer()
    {
        player.GetComponent<Player>().health -= damage;
        Debug.Log("hitPlayer");
    }

    // Check if the player is in sight
    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            return true;
        }

        return false;
    }

    // Move towards the player
    private void MoveTowardsPlayer()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rigidbody2D.velocity = new Vector2(direction.x * currentSpeed, rigidbody2D.velocity.y);
    }
}
