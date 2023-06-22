using System;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public string Name;
    public int Life;
    public Vector LastPosition;
    public Vector LastEulerAngles;
    public Rotation LastRotation;
}
