using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void ReduceHealth(int reduceAmount);
    public void AddHealth(int AddAmount);

    public bool IsDead();
}
