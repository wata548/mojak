using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class CommunicationBox : MonoBehaviour
{
//=====================================================================================| field

    public static CommunicationBox Instance { get; private set; } = null;

    public bool Printing { get; private set; } = false;

    private bool pressSkip = false;
//=====================================================================================| method

    public void ShowCommunication(TMP_Text scriptBox, float intervalTime, string script, int index = 0) {

        Printing = true;
        StartCoroutine(ShowScript(scriptBox, intervalTime, script, index));
    }

    private IEnumerator ShowScript(TMP_Text scriptBox, float intervalTime, string script, int index) {


        ShowOneChar();

        yield return new WaitForSeconds(intervalTime);

        index++;

        if (pressSkip) {

            scriptBox.SetText(script);
            pressSkip = false;
            Printing = false;
        }

        else {
            EndCheck();
        }

        void ShowOneChar() {

            if (index == 0) {

                scriptBox.SetText(script[index].ToString());
            }

            else {

                scriptBox.SetText(scriptBox.text + script[index]);
            }
        }

        void EndCheck() {

            if (index < script.Length) {

                StartCoroutine(ShowScript(scriptBox, intervalTime, script, index));
            }
            else {

                Printing = false;
            }
        }
    }

    public bool SkipCommunication() {
         
        if (Printing) {

            pressSkip = true;
            ;
            return true;
        }

        else {
            return false;
        }
    }

    private void SetSingleton() {

        if(Instance == null) {

            Instance = this;
        }
    }

    private void Awake() {

        /*
        */
        SetSingleton();
    }
}
