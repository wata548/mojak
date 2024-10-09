using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadCommunicationData {

    [Serializable]
    public class Scripts {

        public int      actor;
        public string   scirpt;
    }

    [Serializable]
    public class Communications {

        public string       situation;
        public string[]     actors;
        public Scripts[]    scripts;
    }

    [Serializable]
    public class TargetAndDialogs {
        
        public string           speaker = "\0";
        public Communications[] communications;
    }

    [Serializable]
    public class Dialogs {

        public TargetAndDialogs[] dialogs;
    }

    public static Dialogs datas;

    public static void SetUp() {

        TextAsset jsonFile = Resources.Load<TextAsset>("communications");
        datas = JsonUtility.FromJson<Dialogs>("{\"communications\":" + jsonFile.text + "}");
    }
}

