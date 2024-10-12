using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControleCommunicationSystem : MonoBehaviour
{
//==================================================================| field

    [SerializeField] UnityEngine.UI.Image profile;
    [SerializeField] TMP_Text             scriptIndicate;
    [SerializeField] GameObject           palete;

    private const int       ALREADY_READ         = -1;
    private const float     PRINTING_INTERVAL    = 0.0625f;
    private const KeyCode   SKIP_KEY             = KeyCode.Space;

    private int     currentIndex                 = ALREADY_READ;
    public bool     Active { get; private set; } = false;

    private ReadCommunicationData.Script[] scriptData;

    public static ControleCommunicationSystem Instance { get; private set; } = null;

//==================================================================| Method


    private void SetSingletone() {

        if (Instance == null) {

            Instance = this;
        }
    }

    public void SetDialog(ReadCommunicationData.Communication data) {

        CustomerProfile.GetActors(data.Actors);
        scriptData = data.Scripts;
        currentIndex = 0;

        TurnOn();
    }

    public void StartCommunication() {

        if(!Active || currentIndex == ALREADY_READ) {
            TurnOff();
            return;
        }

        MovementSkipButton.Instance.RestartMovement();

        CommunicationBox.Instance.ShowCommunication(scriptIndicate, PRINTING_INTERVAL, scriptData[currentIndex].SingleScript);
        CustomerProfile.SetProfile(profile, scriptData[currentIndex].Actor);

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

        Active = true;
        palete.SetActive(true);
    }

    public void TurnOff() {

        Active = false;
        palete.SetActive(false);
    }
   
//=================================================================| Logic

    private void Awake() {

        if (Active) {
            TurnOn();
        }
        else {
            TurnOff();
        }

        SetSingletone();
    }

    private void Update() {
        
        if(Active && Input.GetKeyDown(KeyCode.Space)) {

            SkipScript();
        }

    }
}
