using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllustrateController : Interaction
{


    public int Illust_Index { get; private set; }
    public int index { get; private set; }

    Chapter3 chapter;
    [Header("Illust")]
    List< GameObject> IllustImages = new List<GameObject>();
    public GameObject[] Illust;

    [Header("ReadFile")]
    private Read_IllustrateInfo Illust_Info;
    public ShowIllustData showData;

    private void Awake()
    {
        Illust_Info = new Read_IllustrateInfo();
        chapter = GameObject.FindWithTag("Chapter").GetComponent<Chapter3>();


         for(int j=0;j<Illust.Length;j++)
            SetIllustImage(Illust[j]);
           
    }

    void Start()
    {
        Reset();
        StartInteraction();
        
    }

    private void SetIllustImage(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
           IllustImages.Add(obj.transform.GetChild(i).gameObject);
    }


    private void Reset()
    {
        Illust_Index = 0;
        index = 0;

        for (int i = 0; i < Illust.Length; i++)
            Illust[i].SetActive(true);

        for (int i = 0; i < IllustImages.Count; i++)
            IllustImages[i].SetActive(false);
    }


    public void DownIllustrateBTN()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_DialDown);

        if (Illust_Info.Get_IllustrateCount() - 1 <= index)
            EndInteraction();
        else
        {
            index++;
            ShowStory();
        }
    }

   
    private void ShowStory()
    {
        // text
        showData.Index = Illust_Info.Get_Illustrate_Text_Index(index);
        showData.Text.gameObject.SetActive(Illust_Info.Get_Illustrate_Text_bool(index));
        showData.ShowText();
        
        //Imgs
        if (index != 0 && Illust_Index>3) IllustImages[Illust_Info.Get_Illustrate_ImageIndex(index - 1)].SetActive(false);
        IllustImages[Illust_Info.Get_Illustrate_ImageIndex(index)].SetActive(true);
      
    }

    public override void StartInteraction()
    {
        // panel 켜기, 버튼 켜기
       
        showData.ToggleText(true);
     //   Debug.Log("Illust_Index : " + Illust_Index);
        // 파일 읽기
        Illust_Info.Read_Illust_File(Illust_Index);

        //text, image 보여주기
        ShowStory();

        
    }

    public override void EndInteraction()
    {
        // panel 끄기, 버튼 끄기
           
        chapter.Active_FadePanel();

        // 다음 일러스트 인덱스 추가
        Illust_Index++;
        index = 0;
    }

    public void InactiveImages()
    {
        showData.ToggleText(false);
        Illust[Illust_Index-1].SetActive(false);
    }


}
