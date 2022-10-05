using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool bIsEndInteraction { get; private set; }

    private void Start()
    {
        bIsEndInteraction = false;
    }

    public virtual void StartInteraction()
    {

    }

    public virtual void EndInteraction()
    {
        bIsEndInteraction = true;
    }
}
