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

    void UpdateHealthBar(int? delta = null)
    {
        if (initialHealth == 0)
        {
            Debug.Log("error, initialHealth = 0 ");
            return;
        }
        healthBar.value = 1f * health / initialHealth;

        if (healthBarDammageText != null && delta != null)
        {
            healthBarDammageText.gameObject.SetActive(true);
            healthBarDammageText.text = delta.ToString();
            StartCoroutine(FadeOutDammageAmmount());
        }
    }

    public bool IsDead()
    {
        return health == 0;
    }

    void ChangeMaterialOnHit()
    {
        gameObject.layer = LayerMask.NameToLayer("ennemiHit");
        Invoke("ResetLayer", 0.1f);
    }

    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("ennemi");
    }

    private IEnumerator FadeOutDammageAmmount()
    {
        float t = 1f;
        yield return new WaitForSeconds(2f);

        while (t > 0)
        {
            t -= Time.deltaTime;
            healthBarDammageText.alpha = Mathf.Lerp(0f, 1f, t);
            Debug.Log(t);
            yield return null;
        }
        healthBarDammageText.text = "";
        healthBarDammageText.gameObject.SetActive(true);
    }
}
