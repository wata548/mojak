using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControleCommunicationBox : MonoBehaviour
{
//=====================================================================================| field
    [SerializeField] TMP_Text   communicationData;
    [SerializeField] GameObject palete;

    private const int       ALREADY_READ = -1;
    private const float     PRINTING_INTERVAL = 0.0625f;
    private const KeyCode   SKIP_KEY = KeyCode.Space;

    private bool printing = false;
    private bool active = false;
    private int  currentIndex = ALREADY_READ;
    private ReadCommunicationData.Scripts[] scripts;

    public static ControleCommunicationBox Instance { get; private set; } = null;

//=====================================================================================| method

    public void GetData(ReadCommunicationData.Scripts[] scripts) {

        this.scripts = scripts;

        currentIndex = 0;
    }

    public void ShowCommunication() {

        if(!active) {
            return;
        }

        if(currentIndex == ALREADY_READ) {
            return;
        }

        printing = true;
        CustomerProfile.Instance.SetProfile(scripts[currentIndex].actor);
        StartCoroutine(ShowScript(PRINTING_INTERVAL, scripts[currentIndex].script));
        currentIndex++;

        if(currentIndex >= scripts.Length) {
            currentIndex = ALREADY_READ;
        }

        
    }

    private IEnumerator ShowScript(float intervalTime, string singleScript, int index = 0) {

        ShowOneChar();

        yield return new WaitForSeconds(intervalTime);

        index++;

        if (!printing) {
            communicationData.SetText(singleScript);
        }

        else {
            EndCheck();
        }

        void ShowOneChar() {

            if (index == 0) {

                communicationData.SetText(singleScript[index].ToString());
            }

            else {

                communicationData.SetText(communicationData.text + singleScript[index]);
            }
        }

        void EndCheck() {

            if (index < singleScript.Length) {

                StartCoroutine(ShowScript(intervalTime, singleScript, index));
            }

            else {

                printing = false;
            }
        }
    }

    public void SkipCommunication() {

        if(!active) {
            return;
        }

        if (printing) {
            printing = false;
        }

        else {
            ShowCommunication();
        }
    }

    private void SkipByKeyboard() {

        if (Input.GetKeyDown(SKIP_KEY)) {
            SkipCommunication();
        }
    }


    public void TurnOn() {

        palete.SetActive(true);
        active = true;
    }

    public void TurnOff() {

        palete.SetActive(false);
        active = false;
    }


    public void SetSingletone() {

        if(Instance == null) {

            Instance = this;
        }
    }
//======================================================================================| Logic

    private void Awake() {

        SetSingletone();
        TurnOn();
    }

    private void Update() {

        SkipByKeyboard();
    }
}
