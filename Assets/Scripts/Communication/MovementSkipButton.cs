using UnityEngine;

public class MovementSkipButton : MonoBehaviour
{
//======================================================================================| field

    float time = 0;
    const float INCRESE = 0.01f;
    const float MULTIPLE = 3500;

    [SerializeField] GameObject target;


    public static MovementSkipButton Instance { get; private set; } = null;

//======================================================================================| Method

    private void SetSingletone() {

        if(Instance == null) {

            Instance = this;
        }
    }

    private bool GetState() {
        return ControleCommunicationSystem.Instance.Active && !CommunicationBox.Instance.Printing;
    }

    private void ShowBox() {

        target.SetActive(true);
    }

    private void DisapperBox() {

        target.SetActive(false);
    }

    private void Movement() {

        if(GetState()) {

            ShowBox();

            float before = Mathf.Sin(time);

            time += INCRESE;
            time %= Mathf.PI * 2;

            float current = Mathf.Sin(time);
            float deltaY = (current - before) * MULTIPLE * Time.deltaTime;

            Vector3 targetPosition = target.transform.position;
            targetPosition.y += deltaY;

            target.transform.position = targetPosition;
        }

        else {

            DisapperBox();
        }
    }

    public void RestartMovement() {

        time = 0;
    }

//======================================================================================| Logic

    private void Awake() {

        SetSingletone();
    }

    void Update()
    {

        Movement();
    }
}
