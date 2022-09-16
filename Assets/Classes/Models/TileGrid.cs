using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileGrid
{
    readonly public Vector3 offsetVector = new Vector3(0.5f, 0.5f, 0);
    public static Vector3 Translate(int positionX, int positionY) {
        return new Vector3(positionX, positionY, 0) - offsetVector;
    }

    
}
