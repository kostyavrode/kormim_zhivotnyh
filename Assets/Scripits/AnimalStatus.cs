using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimalStatus : MonoBehaviour
{
    public Image[] statusImages;
    private void Start()
    {
        for (int i=0;i<statusImages.Length;i++)
        {
            statusImages[i].fillAmount = (float)Mathf.Clamp(PlayerPrefs.GetInt("F"+i)*1,0,10)/10;
            Debug.Log((float)Mathf.Clamp(PlayerPrefs.GetInt("F" + i) * 1, 0, 10) / 10);
        }
    }
}
