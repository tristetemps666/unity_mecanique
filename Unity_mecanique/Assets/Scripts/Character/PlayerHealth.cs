using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth, IDammagable
{
    public int initialHealth;

    public int health { get; private set; }

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    Slider healthBarDelta;

    float healthBarDeltaVelocity = 0f;

    private bool isInvincible = false;
    private float invisibleTimeAfterHited = 0.2f;

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
        if (isInvincible)
            return;
        health = Mathf.Max(health - reduceAmount, 0);
        UpdateHealthBar();

        InvinsibleForATime(invisibleTimeAfterHited);
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

    public void TakeDammage(int dammageAmmount, GameObject goHitPart)
    {
        Debug.Log("je prends des degats : " + dammageAmmount);
        ReduceHealth(dammageAmmount);

        if (IsDead())
        {
            GameManager.Instance.LoadLooseScene();
            // Debug.Break();
        }
        AudioManager.Instance.PlayPlayerHit();
    }

    void ResetInvisible()
    {
        isInvincible = false;
    }

    public void InvinsibleForATime(float time)
    {
        isInvincible = true;
        Invoke("ResetInvisible", time);
    }
}
