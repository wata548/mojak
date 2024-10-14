using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {

//===============================================================================================| Field
    private SpriteRenderer pill;
    private SpriteRenderer customer;

    private int day = 1;
    private int thisDaysVisitor = 0;
    private string person;
    private long sumPrice = 0;
    public bool ShowPrice { get; private set; } = false;

    const float NEW_VISITOR_DELAY = 1;
    const int LIMIT_DAY_VISITOR = 3;
    const int LIMIT_SUM_DAY = 3;
    const float PRICE_DELAY = 1f;
    const int PURCHASING_PILL_RANGE = 3;

    public static GameControler Instance { get; private set; } = null;

//===============================================================================================| Method

    string SelectRandomPerson() {
        int personIndex = UnityEngine.Random.Range(0, ReadPeopleData.Instance.PeopleNameList.Count - 1) + 1;
        string person = ReadPeopleData.Instance.PeopleNameList[personIndex];

        return person;
    }

    IEnumerator WaitCommunication(string person, string situation, Action action) {

        ControleCommunicationSystem.Instance.StartCommunication(person, situation);
        yield return new WaitUntil(() => !ControleCommunicationSystem.Instance.Active);

        action?.Invoke();
    }

    void HelloAndShowPrice() {

        StartCoroutine(WaitCommunication(person, "firstCommunication", () => ShowPriceAction()));
    }    

    void ShowPriceAction() {
        
        ControleCalculator.Instance.TurnOn();

        int buyPillCount = UnityEngine.Random.Range(0, 3) + 1;
        StartCoroutine(PriceAppear(buyPillCount));

        IEnumerator PriceAppear(int pillCount, int index = 0) {

            ShowPrice = true;

            //set pill info
            string pill = SetRandomPill();
            int countBuyingPill = UnityEngine.Random.Range(0, PURCHASING_PILL_RANGE) + 1;
            int pillPrice = ReadItemsData.Instance.itemData[pill].Price;

            sumPrice += pillPrice * countBuyingPill;

            //wait untill disappear price and process set pill image
            yield return new WaitUntil(() => !IndicatePrice.Instance.Active);
            yield return new WaitForSeconds(PRICE_DELAY / 2);
            SetPill.SetImage(this.pill, pill);
            yield return new WaitForSeconds(PRICE_DELAY / 2);

            //show price
            IndicatePrice.Instance.AppearAndDisappear($"{pill} {pillPrice}¿ø {countBuyingPill}°³");

            //control how many price are shown
            index++;
            if (index < pillCount) {
                StartCoroutine(PriceAppear(pillCount, index));
            }

            else {

                //process when Showing price end
                yield return new WaitUntil(() => !IndicatePrice.Instance.Active);
                yield return new WaitForSeconds(PRICE_DELAY);

                StartCoroutine(WaitCommunication(person, "endOrder", () => ShowPrice = false));
            }

            string SetRandomPill() {
                int countPill = ReadPeopleData.Instance.peopleDatas[person].BuyingItem.Length;
                int randomIndex = UnityEngine.Random.Range(0, countPill);
                string pill = ReadPeopleData.Instance.peopleDatas[person].BuyingItem[randomIndex];

                return pill;
            }

        }
    }

    public void EventSubmitPrice(long expectPrice) {

        if(expectPrice == sumPrice) {

            if(thisDaysVisitor >= LIMIT_DAY_VISITOR) {
                
                if(day == LIMIT_SUM_DAY) {
                    StartCoroutine(WaitCommunication(person, "correctAnswer", null));
                    return;
                }

                day++;
                IndicateDay.Instance.SetDay(day);
                thisDaysVisitor = 0;
            }

            StartCoroutine(WaitCommunication(person, "correctAnswer", () => GiveDelayAndNewVisitor(NEW_VISITOR_DELAY)));
        }

        else {

            StartCoroutine(WaitCommunication(person, "wrongAnswer", () => GiveDelayAndNewVisitor(NEW_VISITOR_DELAY)));

        }
    }

    private void GiveDelayAndNewVisitor(float delay) {

        SetCustomer.Disapper(customer);

        StartCoroutine(Delay(delay,NewVisitor));
        IEnumerator Delay(float delay, Action action) {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }

    private void NewVisitor() {

        sumPrice = 0;
        thisDaysVisitor++;
        person = SelectRandomPerson();

        SetCustomer.SetImage(customer, person);

        HelloAndShowPrice();
    }

    private void SetSingleton() {
        if(Instance == null) {
            Instance = this;
        }
    }

//===============================================================================================| Logic

    void Start()
    {
        day = 1;
        thisDaysVisitor = 0;
        IndicateDay.Instance.SetDay(day);

        NewVisitor();
    }

    private void Awake() {
        SetSingleton();
    }

}
