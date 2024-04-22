using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goLinked;

    public IDammagable dammagable;

    void Start()
    {
        if (goLinked.TryGetComponent<IDammagable>(out IDammagable Dammagable))
        {
            dammagable = Dammagable;
        }
        else
        {
            Debug.Log(
                "le weak point : " + name + " n'est pas lié à un objet enfant de Idammagable"
            );
        }
    }
}
