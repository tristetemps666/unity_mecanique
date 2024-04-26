using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private IAttack attack;

    void Start()
    {
        attack = GetComponentInParent<IAttack>();
    }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider other)
    {
        // IDammagable dammagable = null;
        Debug.Log(other.transform.name + "touchééééééééé");

        if (other.transform.gameObject.TryGetComponent(out IDammagable Outdammagable))
        {
            if (Outdammagable == null)
                Debug.Log("out dammage est null dans le lazer shot");
            // Outdammagable = Outdammagable;
            Outdammagable.TakeDammage(attack.Dammages);
            Debug.Log(other.transform.name + "touchééééééééé AVEC DES DEGATS ????");
        }
    }
}
