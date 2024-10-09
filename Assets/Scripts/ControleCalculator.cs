using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControleCalculator : MonoBehaviour
{
    [SerializeField] GameObject palete;
    [SerializeField] GameObject calculateKeyPrefab;
    [SerializeField] GameObject keyFolder;
    [SerializeField] TMP_Text   ShowNumber;
    [SerializeField] float      horizonInterval   = 50;
    [SerializeField] float      verticalInterval = 60;

    public ControleCalculator instance { get; private set; }

    void SetUpCalculatorKeys() {

        for (int i = 0; i < 3; i++) {

            for (int j = 0; j < 3; j++) {

                int index = i * 3 + j + 1;

                CreateButton(new Vector3(horizonInterval * j, - verticalInterval * i), index);
            }
        }

        CreateButton(new Vector3(horizonInterval * 1, - verticalInterval * 3), 0);

        palete.SetActive(false);

        void CreateButton(Vector3 location, int index) {

            GameObject newKey;
            RectTransform newKeyTrasform;

            newKey = Instantiate(calculateKeyPrefab, keyFolder.transform);

            newKeyTrasform = newKey.GetComponent<RectTransform>();
            newKeyTrasform.position = newKeyTrasform.position + location;

            newKey.GetComponentInChildren<TMP_Text>().SetText(index.ToString());

            var ButtonEvent = newKey.GetComponent<Button>();
            ButtonEvent.onClick.AddListener(() => ClickButton(index));

        }

    }

    public long number = 0;

    private void ClearNumber() {

        number = 0;
    }

    public void ClickButton(int userInput) {

        number = number * 10 + userInput;
        UpdateNumber();
    }

    public void EraseOneNumber() {

        number /= 10;
        UpdateNumber();
    }

    public void Submit() {

        ClearNumber();
        UpdateNumber();
    }

    private void UpdateNumber() {

        ShowNumber.SetText(number.ToString());
    }

    private void Awake() {

        if(instance == null) {

            instance = this;
        }

        SetUpCalculatorKeys();
    }
}
