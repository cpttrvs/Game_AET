using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    FORWARD, BACKWARD, LEFT, RIGHT
}

public static class DirectionUtil
{
    public static Vector3 ToVector(Direction d)
    {
        if (d == Direction.FORWARD)
            return new Vector3(0, 0, 1);
        if (d == Direction.BACKWARD)
            return new Vector3(0, 0, -1);
        if (d == Direction.LEFT)
            return new Vector3(-1, 0, 0);
        if (d == Direction.RIGHT)
            return new Vector3(1, 0, 0);

        return Vector3.zero;
    }
}
