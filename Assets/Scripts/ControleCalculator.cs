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

    private const float   horizonInterval   = 63;
    private const float   verticalInterval  = 76;
    private const long    maximumRange      = 1000000000;
    private const KeyCode ERASE_KEY         = KeyCode.Backspace;
    private const KeyCode SUBMIT_KEY        = KeyCode.Return;

    private long number = 0;
    private bool active = false;

    private Vector2 appearPosition = new(650, 225);
    private Vector2 disappearPosition = new(955, 255);

    public ControleCalculator Instance { get; private set; }

//=======================================================================| Method

    private void SetUpCalculatorKeys() {

        for (int i = 0; i < 3; i++) {

            for (int j = 0; j < 3; j++) {

                int index = i * 3 + j + 1;

                CreateButton(new Vector3(horizonInterval * j, - verticalInterval * i), index);
            }
        }

        CreateButton(new Vector3(horizonInterval * 1, - verticalInterval * 3), 0);

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

        bool outRange = number >= maximumRange;

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

    private void ClickKeyboard() {

        for (char i = '0'; i <= '9'; i++) {

            if (Input.GetKeyDown((KeyCode)i)) {

                ClickButton(i - '0');
                ;
            }
        }

        if (Input.GetKeyDown(ERASE_KEY)) {

            EraseOneNumber();
        }

        if (Input.GetKeyDown(SUBMIT_KEY)) {

            SubmitNumber();
        }
    }


    private void TurnOff() {

        active = false;
        palete.SetActive(false);

    }

    private void TurnOn() {

        active = true;
        palete.SetActive(true);

    }

    IEnumerator AppearMovement(Vector2 startPosition, float increse = 5) {

        const float UI_TRANSFORM = 2.4f;

        yield return new WaitForSeconds(0.08f);

        startPosition.x -= increse;

        palete.transform.position = startPosition * UI_TRANSFORM;
        
        if(startPosition.x > appearPosition.x) {
            StartCoroutine(AppearMovement(startPosition, increse));
        }

        else {
            palete.transform.position = appearPosition * UI_TRANSFORM;
        }
    }
    
    private void SetSingletone() {

        if (Instance == null) {

            Instance = this;
        }
    }

//==========================================================================| Logic

    private void Awake() {

        SetSingletone();

        SetUpCalculatorKeys();

        TurnOff();
    }

    private void Start() {
    }

    private void Update() {

        ClickKeyboard();
    }
}
