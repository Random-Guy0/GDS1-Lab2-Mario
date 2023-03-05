using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{


    public static bool DotTest(this Transform t1, Transform t2, Vector2 testDirection)
    {
        Vector2 dirction = t1.position - t2.position;
        return Vector2.Dot(dirction.normalized, testDirection) > 0.25f;
    }
}
