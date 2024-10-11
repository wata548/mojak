using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ReadCommunicationData;

public class ReadCommunicationData: MonoBehaviour {

    //=================================================================================| field

    //Get Json
    [Serializable]
    public class Scripts {

        public int      actor;
        public string   script;
    }

    [Serializable]
    private class ActorAndScripts {

        public string       situation;
        public string[]     actors;
        public Scripts[]    scripts;
    }

    [Serializable]
    private class TargetAndDialogs {
        
        public string           speaker = "\0";
        public ActorAndScripts[] communications;
    }

    [Serializable]
    private class Dialogs {

        public TargetAndDialogs[] dialogs;
    }

    private Dialogs datas = null;


    //Translate Dictionary
    public class Communication {

        public string[] actors;
        public Scripts[] scripts;
    }

    public class SituationData : Dictionary<string, Communication> { }

    public class AllDialog : Dictionary<string, SituationData> { }

    public AllDialog allDatas = new();


    public static ReadCommunicationData Instance { get; private set; } = null;

    //=================================================================================| Method

    private void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/communications");
        datas = JsonUtility.FromJson<Dialogs>("{\"dialogs\":" + jsonFile.text + "}");
    }

    private void TransToDictionary() {


        foreach(var speaker in datas.dialogs) {

            allDatas.Add(speaker.speaker, new());

            foreach(var situation in speaker.communications) {

                Communication temp = new();

                temp.actors = situation.actors;
                temp.scripts = situation.scripts;

                allDatas[speaker.speaker].Add(situation.situation, temp);
            }
        }
    }

    public void Analysis() {

        AnalysisJson();
        TransToDictionary();
    }

    private void SetSingleTone() {

        if (Instance == null) {
            Instance = this;
        }
    }

//==================================================================================| Logic

    //test Code
    IEnumerator wait() {

        yield return new WaitForSeconds(0.1f);

        ReadCommunicationData.Communication data = allDatas["단소 할아버지"]["firstCommunication"];
        ControleCommunicationSystem.Instance.SetDialog(data);
        ControleCommunicationSystem.Instance.StartCommunication();
    }

    private void Awake() {

        SetSingleTone();
        Analysis();

        StartCoroutine(wait());


    }
}

