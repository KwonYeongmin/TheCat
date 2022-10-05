using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartButton : MonoBehaviour
{
    private Image HeartImg;
    private void Start()
    {
        HeartImg = this.GetComponent<Image>();
    }
    public void PressButton()
    {
        HeartImg.color = new Color(1, 0, 0);
    }
}
