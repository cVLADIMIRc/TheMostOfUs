using UnityEngine;

public class Player : MonoBehaviour
{
	public float maxHealth;
	public float health;
	public float speed;
	public float jumpForce;
	public float damage;

	public bool isGrounded;
	private Rigidbody2D rigidbody2D;
	public Joystick joystick;

    public Transform firePoint;
    public Gun currentGun; // Assuming you have a Gun class defined.

    // Start ���������� ����� ������ ����������� �����
    void Start()
    {
        health = maxHealth;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update ���������� ���� ��� �� ����
    void Update()
    {
        if (joystick.Vertical > 0.5)
        {
            Jump();
        }
        die();

        if (currentGun != null)
        {
            currentGun.Shoot(); // �������� ����� Shoot � �������� ������
        }

        // �������� �������� ��� �����������
        float horizontalInput = Input.GetAxis("Horizontal");

        // ��������� ����������� �������� � �������� �������
        if (horizontalInput > 0)
        {
            // �������� ������
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            // �������� �����
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // FixedUpdate ���������� ������ ������������� ����
    void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(joystick.Horizontal * speed, rigidbody2D.velocity.y);
	}

    // ����� ��� ������
    public void Jump()
	{
		if (isGrounded && joystick.Vertical >= 0.5f)
		{
			isGrounded = false;
			rigidbody2D.velocity = Vector2.up * jumpForce;
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
            gameObject.SetActive(false);
            // Destroy(gameObject);
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