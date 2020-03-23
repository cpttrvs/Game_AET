using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Command : MonoBehaviour
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

        Play();

        OnEnd?.Invoke(this);
    }

    protected abstract void Play();
    protected abstract void Initialise();

    public void SetDrone(Drone d) { drone = d; }
}
