using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float speed;
    public float jumpForce;
    public float damage;

    public bool isGrounded;
    private Rigidbody2D rigidbody2D;

    public Transform firePoint;
    public Gun currentGun; // Assuming you have a Gun class defined.

    // Start вызывается перед первым обновлением кадра
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update вызывается один раз за кадр
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        die();

        if (currentGun != null)
        {
            currentGun.Shoot(); // Вызываем метод Shoot у текущего оружия
        }

        // Получаем значение оси горизонтали
        float horizontalInput = Input.GetAxis("Horizontal");

        // Проверяем направление движения и изменяем масштаб
        if (horizontalInput > 0)
        {
            // Движение вправо
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            // Движение влево
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // FixedUpdate вызывается каждый фиксированный кадр
    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed;

        transform.position = position;
    }

    // Метод для прыжка
    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Обработчик столкновения с землей
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Gun") // Проверяем, что столкнулись с оружием
        {
            Gun gunToPickup = collision.gameObject.GetComponent<Gun>();
            if (gunToPickup != null)
            {
                PickupGun(gunToPickup); // Вызываем метод для поднятия оружия
                Destroy(collision.gameObject); // Уничтожаем оружие на земле
            }
        }
    }

    // Метод для проверки условия смерти
    public void die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Метод для поднятия оружия
    public void PickupGun(Gun gunToPickup)
    {
        Debug.Log("Picking up gun: " + gunToPickup.name);
        currentGun = gunToPickup;
        currentGun.transform.parent = firePoint;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}
