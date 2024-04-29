using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public int HealAmmount = 200;
    bool HasBeenPicked = false;
    public AudioSource source;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (HasBeenPicked)
            return;

        Debug.Log("on touche : " + other.gameObject.name);
        if (other.CompareTag("player") && other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.AddHealth(HealAmmount);
            GetComponent<Renderer>().enabled = false;
            other.GetComponent<Sniper>().setPowerFactor(4f);
            source.Play();

            Destroy(gameObject, 1f);
        }
    }
}
