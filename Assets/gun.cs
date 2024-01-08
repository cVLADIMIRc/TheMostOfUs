using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // Сделаем метод public, чтобы его можно было вызывать из других классов
    public void Fire()
    {
        // Реализуйте здесь логику стрельбы
        Debug.Log("Pew pew! Gun fired!");
    }

    // Метод для подбора оружия игроком
    public void Pickup()
    {
        // Дополнительные действия при поднятии оружия
        gameObject.SetActive(false);
    }

    // Метод, вызываемый при столкновении с другим коллайдером
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Подбор оружия игроком
            other.GetComponent<Player>().PickupGun(this);
            Pickup(); // Вызываем метод Pickup при подборе оружия
        }
    }
}
