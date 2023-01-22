using System;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Serializable] public class HitEvent : UnityEvent<int> { }
    public HitEvent OnHit = new HitEvent();


    public void InvokeHitEvent(int score)
    {
        OnHit.Invoke(score);
    }
    
}
