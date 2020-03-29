using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Technology : Command
{
    [SerializeField]
    protected float duration = 1f;

    [SerializeField]
    protected float range = 20;

}
