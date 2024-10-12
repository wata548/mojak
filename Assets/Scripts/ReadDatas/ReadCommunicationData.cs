using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ReadCommunicationData;

public class ReadCommunicationData : MonoBehaviour {

    //=================================================================================| field

    //Get Json
    [Serializable]
    private class Scripts {

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



    //Translate Dictionary
    public class Communication {

        public string[] Actors { get; private set; }
        public Script[] Scripts { get; private set; }

        public Communication(string[] actors, Script[] scripts) {

            Actors = actors;
            Scripts = scripts;
        }
    }

    public class Script {

        public int Actor { get; private set; }
        public string SingleScript { get; private set; }

        public Script(int actor, string singleScript) {
            Actor = actor;
            SingleScript = singleScript;

        }
    }

    public class SituationData : Dictionary<string, Communication> { }

    public class AllDialog : Dictionary<string, SituationData> { }

    public AllDialog allDatas { get; private set; } = new();


    public static ReadCommunicationData Instance { get; private set; } = null;

    //=================================================================================| Method
    private Dialogs datas = null;

    private void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/communications");
        datas = JsonUtility.FromJson<Dialogs>("{\"dialogs\":" + jsonFile.text + "}");
    }

    private void TransToDictionary() {


        foreach(var speaker in datas.dialogs) {

            allDatas.Add(speaker.speaker, new());

            foreach(var actorAndScripts in speaker.communications) {

                Script[] copyScript = SetMoreSurcurityScript(actorAndScripts.scripts);

                Communication dialog = new(actorAndScripts.actors, copyScript);

                allDatas[speaker.speaker].Add(actorAndScripts.situation, dialog);
            }
        }

        Script[] SetMoreSurcurityScript(Scripts[] scripts) {

            Script[] copyScript = new Script[scripts.Length];
            int index = 0;

            foreach (var script in scripts) {

                Script copy = new(script.actor, script.script);
                copyScript[index++] = copy;
            }

            return copyScript;
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

        //test code start
        StartCoroutine(wait());


    }
}

