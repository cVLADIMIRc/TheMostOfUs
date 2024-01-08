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
    private Transform player; // Ссылка на объект игрока
    private float direction = 1f; // Направление движения врага

    public LayerMask playerLayer; // Слой, на котором находится игрок
    public float detectionRange = 5f; // Расстояние, на котором враг замечает игрока

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        Die();
        Move(); // Вызываем метод движения
    }

    private void FixedUpdate()
    {
        // Проверяем, виден ли игрок
        bool isPlayerDetected = IsPlayerDetected();

        // Если игрок обнаружен, определяем направление движения к игроку
        if (isPlayerDetected)
        {
            direction = Mathf.Sign(player.position.x - transform.position.x);
        }
    }

    bool IsPlayerDetected()
    {
        // Проверяем, виден ли игрок в пределах обнаружения
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, playerLayer);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Move()
    {
        // Производим движение в сторону, указанную в переменной direction
        Vector2 position = transform.position;
        position.x += speed * direction * Time.deltaTime;
        transform.position = position;

        // Поворот взгляда в сторону движения
        if (direction > 0f)
        {
            // Вправо
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0f)
        {
            // Влево
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
