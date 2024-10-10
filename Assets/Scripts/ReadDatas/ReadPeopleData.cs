using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReadPeopleData : MonoBehaviour{

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

        public Image profile;
        public int age;
        public string[] appearence;
        public string trait;
    }

    private PersonArray people;
    public class PeopleDatas : Dictionary<String, Person> { }
    PeopleDatas peopleDatas = new();

//=================================================================| Method

    void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/people");
        people = JsonUtility.FromJson<PersonArray>("{\"people\":" + jsonFile.text + "}");
    }

    void TransToDictionary() {

        Person temp = new();
        foreach (PersonData personData in people.person) {

            temp.profile = Resources.Load<Image>(personData.profile);
            temp.age = personData.age;
            temp.appearence = personData.appearence;
            temp.trait = personData.trait;

            peopleDatas.Add(personData.name, temp);
        }
    }

    public void Analysis() {

        AnalysisJson();

        TransToDictionary();
    }

//=================================================================| Logic

    private void Awake() {
        Analysis();
    }
}

