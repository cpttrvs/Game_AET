using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Coordinate
{
    NORTH, SOUTH, WEST, EAST,
    NORTHWEST, NORTHEAST,
    SOUTHWEST, SOUTHEAST,
    CENTER
}

public static class CoordinateUtil
{
    private static float delta = 0.25f;

    public static Coordinate GetCoordinate(Vector3 start, Vector3 end)
    {

        float dx = end.x - start.x;
        float dz = end.z - start.z;


        if (Mathf.Abs(dx) > Mathf.Abs(dz))
        {
            if (Mathf.Abs(dz / dx) <= tan_Pi_div_8)
            {
                return dx > 0 ? Coordinate.EAST : Coordinate.WEST;
            }

            else if (dx > 0)
            {
                return dz > 0 ? Coordinate.NORTHEAST : Coordinate.SOUTHEAST;
            }
            else
            {
                return dz > 0 ? Coordinate.NORTHWEST : Coordinate.SOUTHWEST;
            }
        }

        else if (Mathf.Abs(dz) > 0)
        {
            if (Mathf.Abs(dx / dz) <= tan_Pi_div_8)
            {
                return dz > 0 ? Coordinate.NORTH : Coordinate.SOUTH;
            }
            else if (dz > 0)
            {
                return dx > 0 ? Coordinate.NORTHEAST : Coordinate.NORTHWEST;
            }
            else
            {
                return dx > 0 ? Coordinate.SOUTHEAST : Coordinate.SOUTHWEST;
            }
        }
        else
        {
            return Coordinate.CENTER;
        }


    }

    static readonly float tan_Pi_div_8 = Mathf.Sqrt(2f) - 1f;
}
