using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIllustData : ShowData
{
    public Image Panel;
    public bool bIsEnd { get; private set; }
    private void Start()
    {
        ShowText();
        bIsEnd = false;
    }
    public override void ShowText()
    {
        SetTextColor(DataReader.GetIllustName(Index));
        Text.text = DataReader.GetIllustContent(Index).ToString();
    }
    public override void NextIndex()
    {
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_Arrowbutton);
        if (DataReader.GetIllustCount() - 1 <= Index)
        {
            ToggleText(false);
            bIsEnd = true;
        }
        else
        {
            base.NextIndex();
            ShowText();
        }
        
    }

    public override void ToggleText(bool value) 
    {
        Panel.gameObject.SetActive(value);
    }

}
