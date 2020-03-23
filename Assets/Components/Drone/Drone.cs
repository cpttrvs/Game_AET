using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drone : MonoBehaviour
{
    private List<Command> commands = null;

    private int commandsPrepared = 0;
    private int currentIndex = 0;

    public event Action<Drone> OnPrepare;
    public event Action<Drone> OnPlay;
    public event Action<Drone> OnEnd;

    public void Prepare()
    {
        if(commands != null)
        {
            commandsPrepared = 0;

            foreach(Command c in commands)
            {
                c.OnPrepare += Command_OnPrepare;
                c.Prepare(this);
            }
        }
    }

    private void Command_OnPrepare(Command c)
    {
        c.OnPrepare -= Command_OnPrepare;
        commandsPrepared++;

        if(commandsPrepared == commands.Count)
        {
            Debug.Log("Drone has prepared (" + commandsPrepared + ")");
            OnPrepare?.Invoke(this);
        }
    }

    public void Play()
    {
        Debug.Log("Drone has started");

        OnPlay?.Invoke(this);

        if (commands != null)
        {
            currentIndex = 0;

            LaunchCommand(currentIndex);
        } else
        {
            Debug.LogWarning("Drone has no command");

            OnEnd?.Invoke(this);
        }
    }

    private void LaunchCommand(int index)
    {
        if (commands != null && index < commands.Count && index >= 0)
        {
            Command c = commands[index];

            LaunchCommand(c);
        }
    }

    private void LaunchCommand(Command c)
    {
        c.OnLaunch += Command_OnLaunch;

        c.Launch();
    }

    private void Command_OnLaunch(Command c)
    {
        c.OnLaunch -= Command_OnLaunch;

        c.OnEnd += Command_OnEnd;
    }

    private void Command_OnEnd(Command c)
    {
        c.OnEnd -= Command_OnEnd;

        currentIndex++;

        if(currentIndex == commands.Count)
        {
            Debug.Log("Drone has finished (" + currentIndex + ")");

            OnEnd?.Invoke(this);
        } else
        {
            LaunchCommand(currentIndex);
        }
    }

    public void AddCommand(Command a)
    {
        if (commands == null) commands = new List<Command>();

        commands.Add(a);
    }

    public List<Command> GetCommands() { return commands; }
}
