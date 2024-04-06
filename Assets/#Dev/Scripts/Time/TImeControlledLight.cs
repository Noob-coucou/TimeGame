using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlledLight : TimeControlled
{
    private Quaternion currentRotation;
    protected override void Start()
    {
        currentRotation = new Quaternion(-10f, 180f, 0f, 0f);
        //Debug.Log(currentRotation);
    }
    public override void OnTimeUpdate()
    {
        float rotationAngle = TimeController.GameTime * 0.5f;
        transform.rotation = Quaternion.Euler(rotationAngle+currentRotation.x, currentRotation.y,currentRotation.z);
    }
}
