using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmaeControler : MonoBehaviour
{
    [SerializeField] SpriteRenderer pill;
    [SerializeField] SpriteRenderer customer;

    static int day = 1;

    string SelectRandomPerson() {
        int personIndex = Random.Range(0, ReadPeopleData.Instance.PeopleNameList.Count - 1) + 1;
        string person = ReadPeopleData.Instance.PeopleNameList[personIndex];

        return person;
    }

    IEnumerator waitForEndCommunication() {

        yield return new WaitUntil(() => !ControleCommunicationSystem.Instance.Active);

        ControleCalculator.Instance.TurnOn();
    }
    void Start()
    {
        IndicateDay.Instance.SetDay(day);

        string person = SelectRandomPerson();

        SetCustomer.SetImage(customer, person);

        ControleCommunicationSystem.Instance.StartCommunication(person, "firstCommunication");

        StartCoroutine(waitForEndCommunication());
    }

}
