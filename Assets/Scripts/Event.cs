using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new event",  menuName = "GDD/event")]
public class Event : ScriptableObject
{
    public delegate void TriggeredDelegate(Vector3 p_position);

    public event TriggeredDelegate onTriggered;
    public event TriggeredDelegate offTriggered;

    public void Raise(Vector3 p_pos)
    {
        onTriggered?.Invoke(p_pos);
    }
    public void Exit(Vector3 p_pos)
    {
        offTriggered?.Invoke(p_pos);
    }
}