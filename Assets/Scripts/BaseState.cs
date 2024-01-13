using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerController playerController;
    public virtual void Construct()
    {
    }
    public virtual void Destruct() { }
    public virtual void Transition() { }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public virtual Vector3 ProcessMotion()
    {
        return Vector3.zero;
    }
}
