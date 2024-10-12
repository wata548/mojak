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

    private void SetSingleton() {

        if(Instance == null) {
            Instance = this;
        }
    }

//==================================================================| Logic

    private void Awake() {

        SetSingleton();
    }

}
