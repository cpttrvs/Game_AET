using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Drone drone = null;

    public List<Command> commands = null;

    private void Start()
    {
        foreach (Command c in commands)
        {
            Command clone = (Command) c.Clone();

            drone.AddCommand(clone);
        }

        drone.OnPrepare += Drone_OnPrepare;
        drone.OnPlay += Drone_OnPlay;
        drone.OnEnd += Drone_OnEnd;


        drone.Prepare();
    }

    private void Drone_OnPrepare(Drone d)
    {
        drone.Play();
    }

    private void Drone_OnPlay(Drone d)
    {

    }

    private void Drone_OnEnd(Drone d)
    {

    }

}
