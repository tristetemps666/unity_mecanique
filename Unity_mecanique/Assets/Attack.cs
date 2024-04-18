using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update

    public delegate void AttackEvent();
    public event AttackEvent OnAttackFinished;
    public event AttackEvent OnAttackStart;

    void Start()
    {
        OnAttackStart += func;
        OnAttackFinished += func;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Rotation());
    }

    void func()
    {
        Debug.Log("il se passe un truc");
    }

    IEnumerator Rotation()
    {
        OnAttackStart();

        float rotateAmount = 0f;
        while (rotateAmount <= 360f)
        {
            rotateAmount += Time.deltaTime * 10f;
            gameObject.transform.Rotate(0, 0, Time.deltaTime * 10f);
            Debug.Log(rotateAmount);
            yield return null;
        }

        OnAttackFinished();
    }
}
