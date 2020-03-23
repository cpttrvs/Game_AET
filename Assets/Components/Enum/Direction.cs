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
            return Vector3.forward;
        if (d == Direction.BACKWARD)
            return Vector3.back;
        if (d == Direction.LEFT)
            return Vector3.left;
        if (d == Direction.RIGHT)
            return Vector3.right;

        return Vector3.zero;
    }

    public static float ToAngle(Direction d)
    {
        if (d == Direction.FORWARD)
            return 0f;
        if (d == Direction.BACKWARD)
            return 180f;
        if (d == Direction.LEFT)
            return -90f;
        if (d == Direction.RIGHT)
            return 90f;

        return 0f;
    }
}
