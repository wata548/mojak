using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndicatePrice : MonoBehaviour
{

//===================================================================================| Field
    
    const float REPEAT_COUNT = 10;
    const float MAX_COLOR    = 1;
    const float APPEAR_TIME  = 2;
    const float CHANGE_TIME  = 0.3f;

    [SerializeField] Image priceBox;
    [SerializeField] TMP_Text price;

    public bool Active { get; private set; } = false;
    public static IndicatePrice Instance { get; private set; } = null;

//====================================================================================| Method

    private void SetSingleton() {

        if(Instance == null) {
            Instance = this;
        }
    }

    private IEnumerator AppearPrice(Image priceBox, TMP_Text price, float targetTime, bool type = true, float index = 0) {

        Active = true;

        yield return new WaitForSeconds(targetTime / REPEAT_COUNT);

        Color targetBoxColor = AppearColor(priceBox.color, type);
        Color targetColor = AppearColor(price.color, type);
        priceBox.color = targetBoxColor;
        price.color = targetColor;

        index++;
        if(index < 10) {

            StartCoroutine(AppearPrice(priceBox, price, targetTime, type, index));
        }

        //Do perfactly appearing state proces and begin to disappear
        else if(type) {
            yield return new WaitForSeconds(APPEAR_TIME - 2 * CHANGE_TIME);

            StartCoroutine(AppearPrice(priceBox, price, targetTime, !type, 0));
        }

        else {
            Active = false;
        }

        Color AppearColor(Color color, bool type) {

            int checkAppear = type ? 1 : -1;

            color.a += checkAppear * MAX_COLOR / REPEAT_COUNT;

            return color;
        }
    }

    public void AppearAndDisappear(string priceInfo) {
        price.SetText(priceInfo);

        StartCoroutine(AppearPrice(priceBox, price, CHANGE_TIME));
    }

//=====================================================================================| Logic 

    private void Awake() {

        SetSingleton();
    }
}
