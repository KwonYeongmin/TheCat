using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowUIData : ShowData
{
    public int Index_;

    private void Start()
    {
        if (DataReader != null) ShowUIText(Index_);
        else Debug.Log("DataReaderNull");
    }
    public override void ShowText()
    {
        Text.text = DataReader.GetUIContent(Index).ToString();
    }
    public override void NextIndex()
    {
        if (DataReader.GetUICount() - 1 <= Index) return;
        base.NextIndex();
        ShowText();
    }

    public void ShowUIText(int index)
    {
        Text.text = DataReader.GetUIContent(index).ToString();
    }

    public override void ToggleText(bool value)
    {
        base.ToggleText(value);
    }

}
