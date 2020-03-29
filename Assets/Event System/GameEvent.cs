using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<EventListener> eventListeners = new List<EventListener>();

    public void Raise()
    {
        Debug.Log(this.name + " Raised");
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void Register(EventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void DeRegister(EventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
