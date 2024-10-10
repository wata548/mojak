using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndicateDay : MonoBehaviour {

//==================================================================| Field

    [SerializeField] TMP_Text indicateDay;

    public static IndicateDay Instance { get; private set; } = null;

//==================================================================| Method

    public void SetDay(int day) {
        indicateDay.SetText($"{day} Day");
    }

    private void SetSingletone() {

        if(Instance) {
            Instance = this;
        }
    }

//==================================================================| Logic

    private void Awake() {

        SetSingletone();
    }

}
