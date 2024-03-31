using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test/Data",fileName ="PlayerDataSO")]
public class PlayerData : ScriptableObject
{
    public float MovementSpeed=2;
    public float RunSpeed = 5;
}
