using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Attack attack;

    void Start()
    {
        attack = GetComponentInParent<Attack>();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        IDammagable dammagable = null;

        if (other.transform.gameObject.TryGetComponent(out IDammagable Outdammagable))
        {
            dammagable = Outdammagable;
            dammagable.TakeDammage(attack.Dammages);
            Debug.Log(other.transform.name + "touchééééééééé AVEC DES DEGATS ????");
        }
        Debug.Log(other.transform.name + "touchééééééééé");
    }
}
