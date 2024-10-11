using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControleCommunicationSystem : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image profile;
    [SerializeField] TMP_Text             scriptIndicate;
    [SerializeField] GameObject           palete;

    private const int ALREADY_READ          = -1;
    private const float PRINTING_INTERVAL   = 0.0625f;
    private const KeyCode SKIP_KEY          = KeyCode.Space;

    private int     currentIndex = ALREADY_READ;
    private bool    active = true;

    private ReadCommunicationData.Scripts[] scriptData;

    public static ControleCommunicationSystem Instance { get; private set; } = null;

    private void SetSingletone() {

        if (Instance == null) {

            Instance = this;
        }
    }

    public void SetDialogData(ReadCommunicationData.Communication data) {

        CustomerProfile.GetActors(data.actors);
        scriptData = data.scripts;
        currentIndex = 0;

        TurnOn();
    }

    public void StartCommunication() {

        if(!active || currentIndex == ALREADY_READ) {
            TurnOff();
            return;
        }


        CommunicationBox.Instance.ShowCommunication(scriptIndicate, PRINTING_INTERVAL, scriptData[currentIndex].script);
        profile.sprite = CustomerProfile.SetProfile(scriptData[currentIndex].actor);

        currentIndex++;

        if(currentIndex == scriptData.Length) {
            currentIndex = ALREADY_READ;
        }
    }

    public void SkipScript() {
        bool skip = CommunicationBox.Instance.SkipCommunication();

        if (!skip) {

            StartCommunication();
        }
    }

    public void TurnOn() {

    }

    public void TurnOff() {

    }

    private void Awake() {

        SetSingletone();
    }

    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Space)) {

            SkipScript();
        }
    }
}
