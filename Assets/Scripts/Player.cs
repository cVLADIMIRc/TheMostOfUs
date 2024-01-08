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

    // Start ���������� ����� ������ ����������� �����
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update ���������� ���� ��� �� ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        die();

        if (currentGun != null)
        {
            currentGun.Shoot(); // �������� ����� Shoot � �������� ������
        }
    }

    // FixedUpdate ���������� ������ ������������� ����
    private void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed;

        transform.position = position;
    }

    // ����� ��� ������
    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // ���������� ������������ � ������
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Gun") // ���������, ��� ����������� � �������
        {
            Gun gunToPickup = collision.gameObject.GetComponent<Gun>();
            if (gunToPickup != null)
            {
                PickupGun(gunToPickup); // �������� ����� ��� �������� ������
                Destroy(collision.gameObject); // ���������� ������ �� �����
            }
        }
    }

    // ����� ��� �������� ������� ������
    public void die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // ����� ��� �������� ������
    public void PickupGun(Gun gunToPickup)
    {
        Debug.Log("Picking up gun: " + gunToPickup.name);
        currentGun = gunToPickup;
        currentGun.transform.parent = firePoint;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.identity;
    }
}
