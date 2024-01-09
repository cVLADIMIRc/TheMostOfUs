using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(player.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(player.health);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
