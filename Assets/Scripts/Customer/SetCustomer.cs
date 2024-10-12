using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCustomer 
{
    
    static void SetImage(SpriteRenderer sprite, string customer, int state) {

        Sprite  image           = ReadPeopleData.Instance.peopleDatas[customer].Images[state];

        sprite.sprite = image;
    }

    static void Disapper(SpriteRenderer sprite) {
        sprite.sprite = null;

    }
}
