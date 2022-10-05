using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class ReadData
{
    //Dial
    List<DialData> Dial_Data_Kor;
    List<DialData> Dial_Data_Eng;

    // UI
    List<UIData> UI_Data_Kor;
    List<UIData> UI_Data_Eng;

    //Illust
    List<IllustData> Illust_Data_Kor;
    List<IllustData> Illust_Data_Eng;

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    string[] File;

    //public int Chapter = 0;

    public ReadData() { Init(); }

    public void Init()
    {
        Dial_Data_Kor = new List<DialData>();
        Dial_Data_Eng = new List<DialData>();

        UI_Data_Kor = new List<UIData>();
        UI_Data_Eng = new List<UIData>();

        Illust_Data_Kor = new List<IllustData>();
        Illust_Data_Eng = new List<IllustData>();

        ReadNewChapterData();
    }

    public void ReadNewChapterData()
    {
        ResetData();

        ReadFile("DialogueText_verKor");
        ReadFile("DialogueText_verEng");
        ReadFile("IllustText_verKor");
        ReadFile("IllustText_verEng");
        ReadFile("UIText_verKor");
        ReadFile("UIText_verEng");
    }

    // ResetData
    public void ResetData()
    {
        Dial_Data_Kor.Clear();
        Dial_Data_Eng.Clear();
        UI_Data_Kor.Clear();
        UI_Data_Eng.Clear();
        Illust_Data_Kor.Clear();
        Illust_Data_Eng.Clear();
    }


    private void ReadFile(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return;

        var header = Regex.Split(lines[0], SPLIT_RE); // header 나누기


        for (int i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);

            if (int.Parse(values[0]) == GameManager.Instance.Chapter)
            // if(int.Parse(values[0])==Chapter)
            {
                if (file.Equals("DialogueText_verKor"))
                {
                    //  Debug.Log("DialValues[1]: " + int.Parse(values[1]));
                    Dial_Data_Kor.Add(new DialData(int.Parse(values[1]), new DialTextData(int.Parse(values[2]), values[3].ToString(),
                                                              values[4].ToString(), int.Parse(values[5]), int.Parse(values[6]))));
                }

                else if (file.Equals("DialogueText_verEng"))
                    Dial_Data_Eng.Add(new DialData(int.Parse(values[1]), new DialTextData(int.Parse(values[2]), values[3].ToString(),
                                                              values[4].ToString(), int.Parse(values[5]), int.Parse(values[6]))));

                //Chapter	UI	Index	Text
                else if (file.Equals("UIText_verKor"))
                    UI_Data_Kor.Add(new UIData(int.Parse(values[1]), values[2].ToString()));
                else if (file.Equals("UIText_verEng"))
                    UI_Data_Eng.Add(new UIData(int.Parse(values[1]), values[2].ToString()));

                else if (file.Equals("IllustText_verKor"))
                    Illust_Data_Kor.Add(new IllustData(int.Parse(values[1]), values[2].ToString(), values[3].ToString()));

                else
                    Illust_Data_Eng.Add(new IllustData(int.Parse(values[1]), values[2].ToString(), values[3].ToString()));
                // 
            }
        }
    }

   


    //================DialogueData
    // Name
    public string GetDialName(int dial, int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor[dial].TextData[index].Name.ToString();
        else
            return Dial_Data_Eng[dial].TextData[index].Name.ToString();
    }
    // Content
    public string GetDialContent(int dial, int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor[dial].TextData[index].Content.ToString();
        else
            return Dial_Data_Eng[dial].TextData[index].Content.ToString();
    }

    // DialIndex
    public int GetDialCount()
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor.Count;
        else
            return Dial_Data_Eng.Count;
    }
    // DialIndex
    public int GetDialIndexCount(int dial)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor[dial].TextData.Count;
        else
            return Dial_Data_Eng.Count;
    }
    // SHAnimIndex
    public int GetDialSHAnimIndex(int dial, int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor[dial].TextData[index].SHAnimIndex;
        else
            return Dial_Data_Eng[dial].TextData[index].SHAnimIndex;
    }
    // SHAnimIndex
    public int GetDialCatAnimIndex(int dial, int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Dial_Data_Kor[dial].TextData[index].CatAnimIndex;
        else
            return Dial_Data_Eng[dial].TextData[index].CatAnimIndex;
    }

    //================UIData
    public string GetUIContent(int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return UI_Data_Kor[index].Content.ToString();
        else
            return UI_Data_Eng[index].Content.ToString();
    }
    public int GetUICount()
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return UI_Data_Kor.Count;
        else
            return UI_Data_Kor.Count;
    }
    //================IllustData
    public int GetIllustCount()
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Illust_Data_Kor.Count;
        else
            return Illust_Data_Eng.Count;
    }
    public string GetIllustName(int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Illust_Data_Kor[index].Name.ToString();
        else
            return Illust_Data_Eng[index].Name.ToString();
    }

    public string GetIllustContent(int index)
    {
        if (GameManager.Instance.LanguageVersion == 0)
            return Illust_Data_Kor[index].Content.ToString();
        else
            return Illust_Data_Eng[index].Content.ToString();
    }

  
}


