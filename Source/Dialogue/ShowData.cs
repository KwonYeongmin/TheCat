using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowData : MonoBehaviour
{
    public ReadData DataReader;
    public TMP_Text Text;
   [HideInInspector] public int Index;// { get; private set; }
    protected Color TextColor;

    private void Awake()
    {
        DataReader = GameManager.Instance.DataReader;
        Index = 0;
    }

    public virtual void ShowText()
    {
       
    }

    public virtual void NextIndex()
    {
        Index++;
        
    }

    public virtual void  ToggleText(bool value)
    {
        Text.gameObject.SetActive(value);
    }

    protected void SetTextColor(string name)
    {
        if (name.Equals("Seonhwa") || name.Equals("Sunhwa") || name.Equals("Radio"))
            Text.color = new Color(1f, 1f, 1f);
        else if(name.Equals("Cat"))
            Text.color = new Color(1f, 0.9f, 0.6f);
        else if (name.Equals("Gogo"))
            Text.color = new Color(0.5f, 0.7f, 0.8f);
        else if (name.Equals("Sphinx"))
            Text.color = new Color(1f, 0.8f, 0.7f);
        else if (name.Equals("Nero"))
            Text.color = new Color(0.96f, 0.75f, 0.25f);
        else if (name.Equals("Parent"))
            Text.color = new Color(0.2f, 0.25f, 0.27f);
    }

}
