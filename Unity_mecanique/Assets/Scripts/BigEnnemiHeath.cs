using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigEnnemiHeath : MonoBehaviour, IHealth
{
    public int initialHealth;

    public int health { get; private set; }

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    Slider healthBarDelta;

    float healthBarDeltaVelocity = 0f;

    void Start()
    {
        health = initialHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        healthBarDelta.value = Mathf.SmoothDamp(
            healthBarDelta.value,
            healthBar.value,
            ref healthBarDeltaVelocity,
            0.3f
        );
    }

    public void ReduceHealth(int reduceAmount)
    {
        health = Mathf.Max(health - reduceAmount, 0);
        UpdateHealthBar();
    }

    public void AddHealth(int AddAmount)
    {
        health += AddAmount;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (initialHealth == 0)
        {
            Debug.Log("error, initialHealth = 0 ");
            return;
        }
        healthBar.value = 1f * health / initialHealth;
    }

    public bool IsDead()
    {
        return health == 0;
    }
}
