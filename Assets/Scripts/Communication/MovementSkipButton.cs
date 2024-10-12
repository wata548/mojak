using UnityEngine;

public class MovementSkipButton : MonoBehaviour
{
//======================================================================================| field

    float time = 0;
    const float INCRESE = 0.03f;
    const float MULTIPLE = 1300;
    Vector2 defaultPosition;

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

            float before = Mathf.Cos(time);

            time += INCRESE;
            time %= Mathf.PI * 2;

            float current = Mathf.Cos(time);
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
        target.transform.position = defaultPosition;
    }

//======================================================================================| Logic

    private void Awake() {

        defaultPosition = target.transform.position;
        SetSingletone();

    }

    void Update()
    {

        Movement();
    }
}
