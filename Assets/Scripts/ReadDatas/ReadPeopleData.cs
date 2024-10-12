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

        public Sprite Profile { get; private set; }
        public int Age { get; private set; }
        public string[] Appearence { get; private set; }
        public string Trait { get; private set; }

        public Person(Sprite profile, int age, string[] appearence, string trait) {

            Profile = profile;
            Age = age;
            Appearence = appearence;
            Trait = trait;

        }
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


            Sprite profile = Resources.Load<Sprite>(personData.profile);
            int age = personData.age;
            string[] appearence = personData.appearence;
            string trait = personData.trait;

            Person temp = new(profile, age, appearence, trait);

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

