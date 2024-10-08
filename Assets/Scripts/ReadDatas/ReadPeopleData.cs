using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadPeopleData {

    [Serializable]
    public class TextData {

        public string   name = "\0";
        public int      age = 0;
        public string[] appearence;
        public string   trait = "\0";
    }

    [Serializable]
    public class PeopleDataArray {

        public TextData[] people;
    }

    public static PeopleDataArray people;

    public static void SetUp() {

        TextAsset jsonFile = Resources.Load<TextAsset>("people");
        people = JsonUtility.FromJson<PeopleDataArray>("{\"people\":" + jsonFile.text + "}");
    }

    private void Awake() {
        SetUp();
    }

}

