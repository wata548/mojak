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
        public string   defaultImagePath = "\0";
        public string   profile = "\0";
        public string[] image;
        public int      age = 0;
        public string[] appearence;
        public string   trait = "\0";
    }

    [Serializable]
    private class PersonArray {

        public PersonData[] person;
    }

    public class Person {

        public Sprite   Profile     { get; private set; }
        public Sprite[] Images      { get; private set; }
        public int      Age         { get; private set; }
        public string[] Appearence  { get; private set; }
        public string   Trait       { get; private set; }

        public Person(Sprite profile, Sprite[] image, int age, string[] appearence, string trait) {

            Profile     = profile;
            Images      = image;
            Age         = age;
            Appearence  = appearence;
            Trait       = trait;

        }
    }

    public class PeopleDatas : Dictionary<String, Person> { }
    public PeopleDatas peopleDatas = new();

    public static ReadPeopleData Instance { get; private set; } = null;

//=================================================================| Method
    private PersonArray people;

    private void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/people");
        people = JsonUtility.FromJson<PersonArray>("{\"person\":" + jsonFile.text + "}");
    }

    private void TransToDictionary() {

        foreach (PersonData personData in people.person) {

            Sprite      profile     = SetProfile(personData);
            Sprite[]    images      = SetPersonImage(personData);
            int         age         = personData.age;
            string[]    appearence  = personData.appearence;
            string      trait       = personData.trait;

            Person temp = new(profile, images, age, appearence, trait);

            peopleDatas.Add(personData.name, temp);
        }

        Sprite[] SetPersonImage(PersonData personData) {

            Sprite[] images = new Sprite[personData.image.Length];
            int index = 0;

            foreach (string image in personData.image) {

                string imageRoot = personData.defaultImagePath + image;
                images[index++] = Resources.Load<Sprite>(imageRoot);
            }

            return images;
        }

        Sprite SetProfile(PersonData personData) {

            string root = personData.defaultImagePath + personData.profile;
            Sprite profile = Resources.Load<Sprite>(root);

            return profile;
        }
    }

    public  void Analysis() {

        AnalysisJson();

        TransToDictionary();
    }

    private void SetSingleton() {

        if(Instance == null) {
            Instance = this;
        }
    }

//=================================================================| Logic

    private void Awake() {

        SetSingleton();
        Analysis();
    }
}

