using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerProfile : MonoBehaviour {

    public  static CustomerProfile Instance { get; private set; } = null;

    [SerializeField] UnityEngine.UI.Image profile;
    public Sprite a;

    private string[] actors;


    private void SetSingletone() {

        if(Instance == null) {
            Instance = this;
        }
    }

    public void SetProfile(int nameTag) {

        Sprite speakerProfile = ReadPeopleData.Instance.peopleDatas[actors[nameTag]].profile;
        profile.sprite = speakerProfile;
    }

    public void GetActors(string[] actors) {

        this.actors = actors;
    }

    private void Awake() {

        SetSingletone();
    }
}
