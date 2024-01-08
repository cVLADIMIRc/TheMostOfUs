using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // ������� ����� public, ����� ��� ����� ���� �������� �� ������ �������
    public void Fire(bool isPlayer)
    {
        if (isPlayer)
        {
            // ���������� ����� ������ ��������
            Debug.Log("Pew pew! Gun fired!");
        }
    }


    // ����� ��� ������� ������ �������
    public void PickupGun()
    {
        // �������������� �������� ��� �������� ������
        gameObject.SetActive(false);
    }

    // �����, ���������� ��� ������������ � ������ �����������
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ������ ������ �������
            other.GetComponent<Player>().PickupGun(this);
            Destroy(gameObject);
        }
    }
}