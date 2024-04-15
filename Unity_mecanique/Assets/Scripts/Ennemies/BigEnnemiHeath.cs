using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// this class is more general for Ennemi Health
public class BigEnnemiHeath : MonoBehaviour, IHealth
{
    public int initialHealth;

    public int health { get; private set; }

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    Slider healthBarDelta;

    [SerializeField]
    TextMeshProUGUI healthBarDammageText;

    float healthBarDeltaVelocity = 0f;

    float delta = 0f;

    float delayFadeDammageAmount = 2f;
    bool dammageAmountCanFade = false;

    // this is used to reset the child layers after we have hit
    private Dictionary<GameObject, int> childLayers = new Dictionary<GameObject, int>();

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

        // handle dammage ammount fade
        HandleAlphaDammageAmount();
    }

    public void ReduceHealth(int reduceAmount)
    {
        health = Mathf.Max(health - reduceAmount, 0);
        if (IsDead())
        {
            Destroy(gameObject);
        }
        ChangeMaterialOnHit();
        UpdateHealthBar(reduceAmount);
    }

    public void AddHealth(int AddAmount)
    {
        health += AddAmount;
        UpdateHealthBar(AddAmount);
    }

    void UpdateHealthBar(int? deltaAmmount = null)
    {
        if (initialHealth == 0)
        {
            Debug.Log("error, initialHealth = 0 ");
            return;
        }
        healthBar.value = 1f * health / initialHealth;

        if (healthBarDammageText != null && deltaAmmount.HasValue)
        {
            delta += deltaAmmount.Value;
            // healthBarDammageText.gameObject.SetActive(true);
            // healthBarDammageText.text = delta.ToString();
            // healthBarDammageText.alpha = 1f;
            // delayFadeDammageAmount = 2f;
            // dammageAmountCanFade = true;
            // Debug.Log("on reaffiche");
            displayDammageAmount();
        }
    }

    public bool IsDead()
    {
        return health == 0;
    }

    void ChangeMaterialOnHit()
    {
        foreach (Renderer child in GetComponentsInChildren<Renderer>())
        {
            GameObject go = child.gameObject;
            childLayers.TryAdd(go, go.layer);
            go.layer = LayerMask.NameToLayer("ennemiHit");
        }
        // gameObject.layer = LayerMask.NameToLayer("ennemiHit");
        Invoke("ResetLayer", 0.1f);
    }

    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("ennemi");

        foreach (Renderer child in GetComponentsInChildren<Renderer>())
        {
            GameObject go = child.gameObject;

            if (childLayers.TryGetValue(go, out int childLayer))
            {
                go.layer = childLayer;
            }
        }
    }

    void displayDammageAmount()
    {
        healthBarDammageText.gameObject.SetActive(true);
        healthBarDammageText.text = delta.ToString();
        healthBarDammageText.alpha = 1f;
        // the alpha will be 1 if is delayFadeDammageAmount > 1
        delayFadeDammageAmount = 2f;
        dammageAmountCanFade = true;
        Debug.Log("on reaffiche");
    }

    void HandleAlphaDammageAmount()
    {
        delayFadeDammageAmount = Mathf.Max(delayFadeDammageAmount - Time.deltaTime, 0);
        // if (delayFadeDammageAmount <= 1f)
        // {
        if (healthBarDammageText != null)
        {
            healthBarDammageText.alpha = Mathf.Lerp(0f, 1f, delayFadeDammageAmount);
            if (delayFadeDammageAmount < 0.05)
                delta = 0;
        }
        // }
    }
}
