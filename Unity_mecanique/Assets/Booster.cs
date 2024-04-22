using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public int HealAmmount = 200;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on touche : " + other.gameObject.name);
        if (other.CompareTag("player"))
        {
            other.GetComponent<PlayerHealth>().AddHealth(HealAmmount);
            other.GetComponent<Sniper>().setPowerFactor(4f);
            Destroy(gameObject);
        }
    }
}
