using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile
{
    public override bool IsWalkable()
    {
        return false;
    }
}
