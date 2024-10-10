using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommunicationBox : MonoBehaviour
{
    [SerializeField] TMP_Text communicationData;

    const int ALREADY_READ = -1;
    bool printing = false;
    float printingInterval = 0.1f;

    int currentIndex = ALREADY_READ;
    ReadCommunicationData.Scripts[] scripts;

    public void GetData(ReadCommunicationData.Scripts[] scripts) {

        this.scripts = scripts;
        currentIndex = 0;
    }

    public void ShowCommunication() {

        printing = true;
        StartCoroutine(ShowScript(printingInterval, scripts[currentIndex].script));


        IEnumerator ShowScript(float intervalTime, string singleScript, int index = 0) {

            if(index == 0) {

                communicationData.SetText(singleScript[index].ToString());
            }
         
            else {

                communicationData.SetText(communicationData.text + singleScript[index]);
            }

            yield return new WaitForSeconds(intervalTime);

            index++;

            if(!printing) {

                communicationData.SetText(singleScript);
            }

            else if (index < singleScript.Length) {

                StartCoroutine(ShowScript(intervalTime, singleScript, index));
            }

            else {

                printing = false;
            }
        }
    }

    private void Awake() {

        ReadCommunicationData.SetUp();
        scripts = ReadCommunicationData.datas.dialogs[0].communications[0].scripts;

        GetData(scripts);
        ShowCommunication();
    }

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space) && printing) {

            printing = false;
        }
    }
}
