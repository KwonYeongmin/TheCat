using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicInteraction : Interaction
{
    public int CubicCount { get; private set; }

    Chapter3 Chapter;

    [Header("Cubic")]
    public GameObject Cubic;
    private GameObject[] Cubics = new GameObject[4];
    private GameObject[] Cubics_Glow = new GameObject[4];

    private void Start()
    {
        Chapter = GameObject.FindWithTag("Chapter").GetComponent<Chapter3>();
       

        for (int i = 0; i < Cubics.Length; i++)
        {
            Cubics[i] = Cubic.transform.GetChild(i).gameObject;
            Cubics_Glow[i] = Cubics[i].transform.GetChild(1).gameObject;
        }

        Reset();
    }

    private void Reset()
    {
        CubicCount = 0;

        for (int i = 0; i < Cubics.Length; i++)
        {
            Cubics[i].SetActive(false);
            Cubics_Glow[i].SetActive(false);
            
        }
    }

 

    public override void StartInteraction()
    {
        UIManager.Instance.CreateAtLocation(UIList.UI_DragNDrop_chapter3 ,Cubics[0].transform.position);

        for (int i = 0; i < Cubics.Length; i++)
        {


            Cubics[i].SetActive(true);
            Cubics_Glow[i].SetActive(true);
        }
    }

    public void AddCubic()
    {
        CubicCount++;
    }

    public override void EndInteraction()
    {
        Debug.Log("EndCubicInteraction");
    }



    public void Trigger(GameObject target)
    {
        CubicCount++;
        if(CubicCount==1)
            UIManager.Instance.Destroy_UI(3);

        target.GetComponent<Animator>().SetBool("click",true);
        Destroy(target, 2f);
    }
}
