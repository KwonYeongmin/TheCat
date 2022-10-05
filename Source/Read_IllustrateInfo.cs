using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Read_IllustrateInfo 
{
    // Illust
    List<IllustInfo> Illust_Info=new List<IllustInfo>();

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public Read_IllustrateInfo() { Read_Illust_File(0); }

    public void ClearList() { Illust_Info.Clear(); } 

    public void Read_Illust_File(int Index)
    {
        ClearList(); 

         TextAsset data = Resources.Load("Illustrate_Info") as TextAsset;
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE); // header 나누기


        for (int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);

            bool value = false;
            bool.TryParse(values[2], out value);

            if (int.Parse(values[0]) == Index)
            {
                Illust_Info.Add(new IllustInfo(int.Parse(values[1]), value, int.Parse(values[3])));
            }
        }
    }


    // ============================== Illust_info

    public int Get_IllustrateCount()
    {
        return Illust_Info.Count;
    }

    public int Get_Illustrate_ImageIndex(int index)
    {
        return Illust_Info[index].Image_Index;
    }
    public bool Get_Illustrate_Text_bool(int index)
    {
        return Illust_Info[index].bIsTextActive;
    }

    public int Get_Illustrate_Text_Index(int index)
    {
        return Illust_Info[index].TextIndex;
    }
}
