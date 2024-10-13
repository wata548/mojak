using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPill
{
    public static void SetImage(SpriteRenderer sprite, string name) {

        Sprite image = ReadItemsData.Instance.itemData[name].Image;

        sprite.sprite = image;
    }

    public static void Disapear(SpriteRenderer sprite) {

        sprite.sprite = null;
    }
}
