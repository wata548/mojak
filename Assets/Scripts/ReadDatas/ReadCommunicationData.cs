using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadInteractionData {

    [Serializable]
    public class Script {
        public string      situation;
        public string[]    script;
    }

    [Serializable]
    public class SpeakerAndScript {
        
        public string   speaker= "\0";
        public Script[] communication;
    }

    [Serializable]
    public class CommunicationDatas {

        public SpeakerAndScript[] communications;
    }

    public static CommunicationDatas communicationDatas;

    static void SetUp() {

        TextAsset jsonFile = Resources.Load<TextAsset>("communications");
        communicationDatas = JsonUtility.FromJson<CommunicationDatas>("{\"communications\":" + jsonFile.text + "}");
    }
}

