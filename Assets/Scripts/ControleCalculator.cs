using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControleCalculator : MonoBehaviour
{

//=======================================================================| Field

    [SerializeField] GameObject palete;
    [SerializeField] GameObject calculateKeyPrefab;
    [SerializeField] GameObject keyFolder;
    [SerializeField] TMP_Text   ShowNumber;
    [SerializeField] float      horizonInterval     = 50;
    [SerializeField] float      verticalInterval    = 60;

    private  bool    active         = false;
    readonly long    maximumRange   = 1000000000000000;
    readonly KeyCode ERASE_KEY      = KeyCode.Backspace;
    readonly KeyCode SUBMIT_KEY     = KeyCode.Return;

    public long number = 0;

//=======================================================================| Method

    public ControleCalculator instance { get; private set; }

    private void SetUpCalculatorKeys() {

        for (int i = 0; i < 3; i++) {

            for (int j = 0; j < 3; j++) {

                int index = i * 3 + j + 1;

                CreateButton(new Vector3(horizonInterval * j, - verticalInterval * i), index);
            }
        }

        CreateButton(new Vector3(horizonInterval * 1, - verticalInterval * 3), 0);

        TurnOff();

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

    public void ClickButton(int userInput) {

        bool outRange = number > maximumRange;

        if (!active || outRange) {

            return;
        }

        number = number * 10 + userInput;
        UpdateNumber();
    }

    public void EraseOneNumber() {

        if (!active) {

            return;
        }

        number /= 10;
        UpdateNumber();
    }

    public void SubmitNumber() {

        if (!active) {

            return;
        }

        ClearNumber();
        UpdateNumber();
    }

    private void ClearNumber() {

        number = 0;
    }

    private void UpdateNumber() {

        ShowNumber.SetText(number.ToString());
    }

    private void TurnOff() {

        active = false;
        palete.SetActive(false);
    }

    private void TurnOn() {

        active = true;
        palete.SetActive(true);
    }

    //==========================================================================| Logic

    private void Awake() {

        if(instance == null) {

            instance = this;
        }

        SetUpCalculatorKeys();
    }

    private void Update() {

        for(char i = '0'; i <= '9'; i++) {

            if (Input.GetKeyDown((KeyCode)i)) {

                ClickButton(i - '0');
                ;
            }
        }

        if(Input.GetKeyDown(ERASE_KEY)) {

            EraseOneNumber();
        }

        if(Input.GetKeyDown(SUBMIT_KEY)) {

            SubmitNumber();
        }
    }
}
