using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown frameRateDropdown;

    
    private int[] limits = {30,60};
    private int currentLimit;
    private void Start()
    {
        currentLimit = PlayerPrefs.GetInt("fpslimit");
        
        Application.targetFrameRate = (int)limits[currentLimit];
        frameRateDropdown.value = currentLimit;
    }


    public void SetLimit(int limitIndex)
    {
        Application.targetFrameRate = (int)limits[limitIndex];
        currentLimit = limitIndex;
        PlayerPrefs.SetInt("fpslimit", limitIndex);
    }
}
