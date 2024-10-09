using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicateDay : MonoBehaviour
{
    [SerializeField] TMP_Text indicateDay;

    public void SetDay(int day) {
        indicateDay.SetText($"{day} Day");
    }

}
