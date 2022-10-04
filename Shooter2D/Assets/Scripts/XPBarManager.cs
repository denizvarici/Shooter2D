using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxXP(int maxInGameXpAmount)
    {
        slider.value = 0;
        slider.maxValue = maxInGameXpAmount;
    }

    public void SetXP(int inGameXpAmount)
    {
        slider.value = inGameXpAmount;
    }
}
