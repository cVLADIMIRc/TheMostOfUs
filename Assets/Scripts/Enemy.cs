using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float jumpForce;
    public float damage;

    public bool isGrounded;
    private Rigidbody2D rigidbody2D;
    private Transform player; // ������ �� ������ ������
    private float direction = 1f; // ����������� �������� �����

    public LayerMask playerLayer; // ����, �� ������� ��������� �����
    public float detectionRange = 5f; // ����������, �� ������� ���� �������� ������

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        Die();
        Move(); // �������� ����� ��������
    }

    private void FixedUpdate()
    {
        // ���������, ����� �� �����
        bool isPlayerDetected = IsPlayerDetected();

        // ���� ����� ���������, ���������� ����������� �������� � ������
        if (isPlayerDetected)
        {
            direction = Mathf.Sign(player.position.x - transform.position.x);
        }
    }

    bool IsPlayerDetected()
    {
        // ���������, ����� �� ����� � �������� �����������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, playerLayer);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Move()
    {
        // ���������� �������� � �������, ��������� � ���������� direction
        Vector2 position = transform.position;
        position.x += speed * direction * Time.deltaTime;
        transform.position = position;

        // ������� ������� � ������� ��������
        if (direction > 0f)
        {
            // ������
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0f)
        {
            // �����
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hitPlayer()
    {
        GameObject.FindObjectOfType<Player>().health -= damage;
        Debug.Log("hitPlayer");
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
}
