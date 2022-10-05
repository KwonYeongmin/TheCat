using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TextData
{
    public int Index { get; private set; }
   public string Content { get; private set; }

    public TextData(int index, string content)
    {
       Index = index;
       Content = content;
    }
}


public class DialData 
{
    public int Index { get; private set; }
    public List<DialTextData> TextData { get; private set; }
    public DialData(int index, DialTextData textData)
    {
        TextData = new List<DialTextData>();
        Index = index;
        TextData.Add(textData);
    }
}

// chapter, Index, index, Name, 
    public class DialTextData : TextData
{
    public string Name { get; private set; }
    public int SHAnimIndex { get; private set; }
    public int CatAnimIndex { get; private set; }


    public DialTextData(int index, string name,string content, int SHanimIndex,int CatanimIndex) : base(index,content)
    {
        SHAnimIndex = SHanimIndex;
        CatAnimIndex = CatanimIndex;
        Name = name;
    }
}
 // chapter index, name, text
public class IllustData : TextData
{
    public string Name { get; private set; }
    
    public IllustData(int index, string name, string content) : base(index,content)
    {
        Name = name;
    }
}

// Chapter, index, Text
public class UIData : TextData
{
    public UIData(int index, string content) : base(index, content) { }
}

public class IllustInfo
{
   // public int Illust_Index { get; private set; }
    public int Image_Index { get; private set; }
    public bool bIsTextActive { get; private set; }
    public int TextIndex { get; private set; }

    //public IllustInfo(int illust_index,int image_index, bool bisTextactive, int textIndex)
    public IllustInfo(int image_index, bool bisTextactive, int textIndex)
   {
        Image_Index = image_index;
        bIsTextActive = bisTextactive;
        TextIndex = textIndex;
    }
}