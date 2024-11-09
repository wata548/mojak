using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerProfile {

    public static void SetProfile(UnityEngine.UI.Image sprite, string actor) {

        Sprite speakerProfile = ReadPeopleData.Instance.peopleDatas[actor].Profile;

        sprite.sprite = speakerProfile;
    }
}
