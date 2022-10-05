using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    protected UIManager() { }


    List<GameObject> UIObjects;

    private void Awake()
    {
        UIObjects = new List<GameObject>();
    }



    public void CreateAtLocationToFront(GameObject uiObject, Vector3 location)
    {
        GameObject obj = Instantiate(uiObject, location, uiObject.transform.rotation);
        int sortingLayerID = SortingLayer.NameToID("ToFront");
        obj.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayerID;
        UIObjects.Add(obj);
        //Debug.Log(UIObjects.Count);
    }
    public void CreateAtLocation(GameObject uiObject, Vector3 location, float lifetime)
    {
        GameObject obj = Instantiate(uiObject, location, uiObject.transform.rotation);
       // Debug.Log(UIObjects.Count);
        Destroy(obj, lifetime);
    }

    public void CreateAtLocation_Parent(GameObject uiObject,Vector3 pos, Transform parent)
    {
        GameObject obj = Instantiate(uiObject, pos, Quaternion.identity);
        UIObjects.Add(obj);
        obj.transform.parent = parent;
    
        //Debug.Log(UIObjects.Count);
    }

    public void CreateAtLocation(GameObject uiObject, Vector3 location)
    {
        GameObject obj = Instantiate(uiObject, location, uiObject.transform.rotation);
        UIObjects.Add(obj);
       // Debug.Log(UIObjects.Count);
    }

    public void CreateAtCanvas(GameObject uiObject, Vector2 position)
    {
        GameObject obj 
            = Instantiate(uiObject, position, Quaternion.identity,GameObject.Find("Canvas").transform);

        UIObjects.Add(obj);
       // Debug.Log(UIObjects.Count);
    }

    public void Destroy_UI(int index)
    {
        Destroy(UIObjects[index]);
        Debug.Log("Count : " + UIObjects.Count);
        Debug.Log(UIObjects[index]);

    }

    public void ClearObjects()
    {
        UIObjects.Clear();
    }

}
