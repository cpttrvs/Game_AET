using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Command : MonoBehaviour, ICloneable
{
    protected Drone drone = null;

    private bool isInitialised = false;

    public event Action<Command> OnPrepare;
    public event Action<Command> OnLaunch;
    public event Action<Command> OnEnd;

    public void Prepare(Drone d)
    {
        if (isInitialised) return;

        SetDrone(d);

        Initialise();

        isInitialised = true;

        OnPrepare?.Invoke(this);
    }

    public void Launch()
    {
        if (!isInitialised) return;

        OnLaunch?.Invoke(this);

        StartCoroutine(Play());
    }

    public virtual void End()
    {
        OnEnd?.Invoke(this);
    }

    protected abstract IEnumerator Play();
    protected abstract void Initialise();

    public void SetDrone(Drone d) { drone = d; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
