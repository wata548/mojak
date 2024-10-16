using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCustomer 
{
    
    public static void SetImage(SpriteRenderer sprite, string customer, int state = 0) {

        Sprite image = ReadPeopleData.Instance.peopleDatas[customer].Images[state];

        sprite.sprite = image;
    }

    public static void Disapper(SpriteRenderer sprite) {
        sprite.sprite = null;
    }

}
