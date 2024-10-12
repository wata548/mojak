using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadItemsData : MonoBehaviour
{
    [Serializable] 
    private class ItemInfo {
        public string item;
        public int price;
        public string image;
    }

    [Serializable]
    private class Items {
        public ItemInfo[] items;
    }

    public class Item { 
    
        public int Price { get; private set; }
        public Sprite Image { get; private set; }

        public Item(int price, Sprite image) {

            Price = price;
            Image = image;
        }
    }

    public Dictionary<string, Item> itemData { get; private set; } = new();

    private Items datas;

    public static ReadItemsData Instance { get; private set; } = null;

    private void AnalysisJson() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Jsons/itemInfo");
        datas = JsonUtility.FromJson<Items>("{\"items\":" + jsonFile.text + "}");
    }

    private void TransDictionary() {

        foreach (var item in datas.items) {

            Sprite image = Resources.Load<Sprite>(item.image);
            Item newItem = new(item.price, image);

            itemData.Add(item.item, newItem);
        }
    }

    private void Analysis() {

        AnalysisJson();

        TransDictionary();
    }

    private void SetSingletone() {

        if(Instance == null) {
            Instance = this;
        }
    }

    private void Awake() {

        SetSingletone();
        Analysis();
    }
}
