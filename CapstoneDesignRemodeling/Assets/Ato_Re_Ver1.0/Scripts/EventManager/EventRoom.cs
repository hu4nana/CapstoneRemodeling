using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventRoom : MonoBehaviour
{
    abstract public void EventStartTrigger();
    abstract public void Event();
    abstract public void EventEndTrigger();
}
