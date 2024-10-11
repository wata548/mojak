using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class CommunicationBox : MonoBehaviour
{
//=====================================================================================| field

    private static ReadCommunicationData.Scripts[] scripts;

    public static CommunicationBox Instance { get; private set; } = null;

    private static bool printing = false;
//=====================================================================================| method

    public void ShowCommunication(TMP_Text dialogBox, float intervalTime, string script, int index = 0) {

        printing = true;
        StartCoroutine(ShowScript(dialogBox, intervalTime, script, index));
    }

    private IEnumerator ShowScript(TMP_Text dialogBox, float intervalTime, string script, int index) {


        ShowOneChar();

        yield return new WaitForSeconds(intervalTime);

        index++;

        if (!printing) {
            dialogBox.SetText(script);
        }

        else {
            EndCheck();
        }

        void ShowOneChar() {

            if (index == 0) {

                dialogBox.SetText(script[index].ToString());
            }

            else {

                dialogBox.SetText(dialogBox.text + script[index]);
            }
        }

        void EndCheck() {

            if (index < script.Length) {

                StartCoroutine(ShowScript(dialogBox, intervalTime, script, index));
            }
            else {

                printing = false;
            }
        }
    }

    public bool SkipCommunication() {

        if (printing) {

            printing = false;
            return true;
        }

        else {
            return false;
        }
    }

    private void SetSingletone() {

        if(Instance == null) {

            Instance = this;
        }
    }

    private void Awake() {

        SetSingletone();
    }
}
