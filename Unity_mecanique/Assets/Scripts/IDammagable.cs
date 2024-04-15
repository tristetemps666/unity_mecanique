using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDammagable
{
    // Start is called before the first frame update

    // goHitPart can be used to check where we have hit(it can be used for critical dammage for exemple)
#nullable enable
    void TakeDammage(int dammageAmount, GameObject? goHitPart = null);
}
