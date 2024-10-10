using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReadPeopleData : MonoBehaviour {

//=================================================================| Field

    [Serializable]
    private class PersonData {

        public string   name = "\0";
        public string   profile = "\0";
        public int      age = 0;
        public string[] appearence;
        public string   trait = "\0";
    }

    [Serializable]
    private class PersonArray {

        public PersonData[] person;
    }

    public class Person {

        public Sprite profile;
        public int age;
        public string[] appearence;
        public string trait;
    }

    private PersonArray people;
    public class PeopleDatas : Dictionary<String, Person> { }
    public PeopleDatas peopleDatas = new();

    public static ReadPeopleData Instance { get; private set; } = null;

//=================================================================| Method

    private void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/people");
        people = JsonUtility.FromJson<PersonArray>("{\"person\":" + jsonFile.text + "}");
    }

    private void TransToDictionary() {

        foreach (PersonData personData in people.person) {

            Person temp = new();

            temp.profile = Resources.Load<Sprite>(personData.profile);
            temp.age = personData.age;
            temp.appearence = personData.appearence;
            temp.trait = personData.trait;

            peopleDatas.Add(personData.name, temp);
        }
    }

    public  void Analysis() {

        AnalysisJson();

        TransToDictionary();
    }

    private void SetSingleTone() {

        if(Instance == null) {
            Instance = this;
        }
    }

//=================================================================| Logic

    private void Awake() {

        SetSingleTone();
        Analysis();
    }
}

