using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlledLight : TimeControlled
{
    public override void OnTimeUpdate()
    {
        float rotationAngle = TimeController.GameTime * 2; 
        transform.rotation = Quaternion.Euler(rotationAngle, 0, 0);
    }
}
