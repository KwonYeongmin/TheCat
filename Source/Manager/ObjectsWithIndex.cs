using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsWithIndex : MonoBehaviour
{
    public int Index = 0;
    public float Lifetime;
    public bool bIsDestroySelf;


  

    private void Start()
    {
        if (bIsDestroySelf) DestroyObject();
      
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject, Lifetime);
    }


 
}
