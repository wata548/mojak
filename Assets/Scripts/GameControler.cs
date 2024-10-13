using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReadPeopleData;

public class GameControler : MonoBehaviour {
    [SerializeField] SpriteRenderer pill;
    [SerializeField] SpriteRenderer customer;

    static int day = 1;
    static string person;
    public int sumPrice = 0;
    public static bool ShowPrice { get; private set; } = false;

    const float PRICE_DELAY = 1f;
    const int PURCHASING_PILL_RANGE = 3;

    string SelectRandomPerson() {
        int personIndex = Random.Range(0, ReadPeopleData.Instance.PeopleNameList.Count - 1) + 1;
        string person = ReadPeopleData.Instance.PeopleNameList[personIndex];

        return person;
    }

    IEnumerator waitUntilCommunicationEnd(string person, string situation) {

        ControleCommunicationSystem.Instance.StartCommunication(person, situation);
        yield return new WaitUntil(() => !ControleCommunicationSystem.Instance.Active);

        ControleCalculator.Instance.TurnOn();

        int buyPillCount = Random.Range(0, 3) + 1;
        StartCoroutine(PriceAppear(buyPillCount));

        IEnumerator PriceAppear(int pillCount, int index = 0) {

            ShowPrice = true;

            string pill = SetRandomPill();
            int countBuyingPill = Random.Range(0, PURCHASING_PILL_RANGE) + 1;
            int pillPrice = ReadItemsData.Instance.itemData[pill].Price;

            sumPrice += pillPrice * countBuyingPill;

            yield return new WaitUntil(() => !global::IndicatePrice.Instance.Active);
            yield return new WaitForSeconds(PRICE_DELAY / 2);
            SetPill.SetImage(this.pill, pill);
            yield return new WaitForSeconds(PRICE_DELAY / 2);



            IndicatePrice.Instance.AppearAndDisappear($"{pill} {pillPrice}¿ø {countBuyingPill}°³");

            index++;
            if (index < pillCount) {
                StartCoroutine(PriceAppear(pillCount, index));
            }

            else {

                yield return new WaitUntil(() => !global::IndicatePrice.Instance.Active);
                yield return new WaitForSeconds(PRICE_DELAY);
                ControleCommunicationSystem.Instance.StartCommunication(person, "endOrder");
                yield return new WaitUntil(() => !ControleCommunicationSystem.Instance.Active);
                ShowPrice = false;

            }

            string SetRandomPill() {
                int countPill = ReadPeopleData.Instance.peopleDatas[person].BuyingItem.Length;
                int randomIndex = Random.Range(0, countPill);
                string pill = ReadPeopleData.Instance.peopleDatas[person].BuyingItem[randomIndex];

                return pill;
            }

        }
    }    

    void Start()
    {
        IndicateDay.Instance.SetDay(day);

        person = SelectRandomPerson();

        SetCustomer.SetImage(customer, person);

        StartCoroutine(waitUntilCommunicationEnd(person, "firstCommunication"));
    }

}
