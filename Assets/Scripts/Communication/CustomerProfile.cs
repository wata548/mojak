using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerProfile {

    private static string[] actors;

    public static Sprite SetProfile(int nameTag) {

        Sprite speakerProfile = ReadPeopleData.Instance.peopleDatas[actors[nameTag]].profile;

        return speakerProfile;
    }

    public static void GetActors(string[] actors) {

        CustomerProfile.actors = actors;
    }
}
