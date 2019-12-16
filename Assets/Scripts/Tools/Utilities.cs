using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {


    public static float getAngleDegBetween(float y, float x)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }

}
