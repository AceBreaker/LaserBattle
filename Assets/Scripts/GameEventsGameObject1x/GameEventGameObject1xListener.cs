using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObject1xUnityEvent : UnityEvent<GameObject> { }

public class GameEventGameObject1xListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventGameObject1x Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public GameObject1xUnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject go)
    {
        Response.Invoke(go);
    }
}