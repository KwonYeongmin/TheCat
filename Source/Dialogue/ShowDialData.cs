using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialData : ShowData
{
   // public int DialIndex { get; private set; }
    [Header("Panel")]
    public GameObject[] Panel;

    [Header("DialAnimation")]
    public GameObject[] SHAnim;
    public GameObject[] CatAnim;
    int SHindex = 0;
    int Catindex = 0;

   

    public override void ShowText()
    {
        SetAnim();
        SetTextColor(DataReader.GetDialName(Index, 0).ToString());
        Text.text = DataReader.GetDialContent(Index, 0);
    }

    public void NextDialIndex()
    {
        if (DataReader.GetDialCount()-1 <= Index) return;
        Index++;
        ShowText();
    }

    public override void ToggleText(bool value)
    {
       // base.ToggleText(value);
        for(int i=0;i<2;i++)  Panel[i].SetActive(value);
    }

    private void ToggleAnim(int curIndex,int PrevIndex,GameObject[] Object)
    {
        //int Previndex = curIndex;
        if (Index > 0)
            Object[PrevIndex].SetActive(false);

        Object[curIndex].SetActive(true);
        Debug.Log(Object[curIndex].name);
    }

    void SetAnim()
    {
        Color activeColor = new Color(1f, 1f, 1f);
        Color inactiveColor = new Color(0.5f, 0.5f, 0.5f);

        
        SHindex = DataReader.GetDialSHAnimIndex(Index, 0);
        Catindex = DataReader.GetDialCatAnimIndex(Index, 0);

        if (Index - 1 > 0)
        {
            
            if (DataReader.GetDialSHAnimIndex(Index, 0) != DataReader.GetDialSHAnimIndex(Index-1, 0))
                SHAnim[DataReader.GetDialSHAnimIndex(Index - 1, 0)].SetActive(false);

            if (DataReader.GetDialCatAnimIndex(Index, 0) != DataReader.GetDialCatAnimIndex(Index - 1, 0))
                CatAnim[DataReader.GetDialCatAnimIndex(Index - 1, 0)].SetActive(false);

        }

        if (DataReader.GetDialName(Index, 0).Equals("Seonhwa") ||
           DataReader.GetDialName(Index, 0).Equals("Sunhwa"))
        {
            if (SHAnim[SHindex].GetComponent<Image>())
                SHAnim[SHindex].GetComponent<Image>().color = activeColor;
            if (CatAnim[Catindex].GetComponent<Image>())
                CatAnim[Catindex].GetComponent<Image>().color = inactiveColor;
        }
        else
        {
            if (SHAnim[SHindex].GetComponent<Image>())
                SHAnim[SHindex].GetComponent<Image>().color = inactiveColor;
            if (CatAnim[Catindex].GetComponent<Image>())
                CatAnim[Catindex].GetComponent<Image>().color = activeColor;
        }

            SHAnim[SHindex].SetActive(true);
        CatAnim[Catindex].SetActive(true);
    }

    public void InActiveAnim()
    {
        for (int i = 0; i < SHAnim.Length; i++)
            SHAnim[i].SetActive(false);
        for (int i = 0; i < CatAnim.Length; i++)
            CatAnim[i].SetActive(false);
    }


    private void SetAnimColor()
    {
        Color activColor = new Color(1f, 1f, 1f);
        Color inactivColor = new Color(0.5f, 0.5f, 0.5f);
        
        // 현재 애니메이션 인덱스 설정
        SHindex = DataReader.GetDialSHAnimIndex(Index, 0);
        Catindex = DataReader.GetDialCatAnimIndex(Index, 0);

        // 이전 애니메이션 꺼주기, 현재 애니메이션 켜주기
        if (Index - 1 > 0)
        {
            ToggleAnim(SHindex, DataReader.GetDialSHAnimIndex(Index - 1, 0), SHAnim);
            ToggleAnim(Catindex, DataReader.GetDialCatAnimIndex(Index - 1, 0), CatAnim);
        }

        if (DataReader.GetDialName(Index, 0).Equals("Seonhwa") ||
            DataReader.GetDialName(Index, 0).Equals("Sunhwa"))
        {
            if (SHAnim[Catindex].GetComponent<Image>())
                SHAnim[SHindex].GetComponent<Image>().color = activColor;
           if(CatAnim[Catindex].GetComponent<Image>())
                CatAnim[Catindex].GetComponent<Image>().color = inactivColor;
        }
        else
        {
            if (SHAnim[Catindex].GetComponent<Image>()) SHAnim[SHindex].GetComponent<Image>().color = inactivColor;
            if (CatAnim[Catindex].GetComponent<Image>()) CatAnim[Catindex].GetComponent<Image>().color = activColor;
        }
        
    }
}
