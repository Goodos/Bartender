using UnityEngine;

public class VisitorController : MonoBehaviour
{
    [SerializeField] CanvasController canvasController;

    ConveyorController conveyorController;
    private Vector3 targetPosition;
    private Vector3 directionVector;
    private Vector3 startPos;
    private float speed = 4f;
    private bool decreaseScore = true;

    public float timer = 0;
    public bool startTimer = false;
    public int[] primitiveType = new int[] { 0, 0, 0 };
    public Vector3 target;

    private void Awake()
    {
        PrimitiveGenerator();
    }

    void Start()
    {
        conveyorController = GameObject.Find("Сonveyor").GetComponent<ConveyorController>();
        startPos = transform.position;
    }

    void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (decreaseScore)
                {
                    ConveyorController.score--;
                    decreaseScore = false;
                }
                Movement(startPos);
                if (transform.position == startPos)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                FindTray();
            }
        }
        else
        {
            Movement(target);
        }
    }

    void Movement(Vector3 target)
    {
        targetPosition = target;
        directionVector = target - transform.position;
        if (directionVector.magnitude > 1)
        {
            directionVector = directionVector.normalized;
        }
        transform.Translate(directionVector * Time.deltaTime * speed);
        Vector3 diff = targetPosition - transform.position;
        if (diff.magnitude < .05f)
        {
            startTimer = true;
            transform.position = targetPosition;
        }
    }

    void PrimitiveGenerator()
    {
        int randomCount = Random.Range(1, 3);
        for (int i = 0; i <= randomCount; i++)
        {
            primitiveType[i] = Random.Range(1, 4);
        }
    }

    void FindTray()
    {
        foreach (GameObject tray in conveyorController.trays)
        {
            if (tray != null)
            {
                bool check = false;
                int[] primitiveTypeCheck = new int[3];
                primitiveType.CopyTo(primitiveTypeCheck, 0);

                for (int i = 0; i < 3; i++)
                {
                    check = false;
                    for (int j = 0; j < 3; j++)
                    {
                        if (tray.GetComponent<TrayMovement>().primitiveType[i] == primitiveTypeCheck[j])
                        {
                            primitiveTypeCheck[j] = -1;
                            check = true;
                            break;
                        }
                    }
                    if (!check)
                    {
                        break;
                    }
                }
                if (check)
                {
                    if (tray.transform.position.x >= transform.position.x && tray.transform.position.x - transform.position.x <= .65f)
                    {
                        tray.GetComponent<TrayMovement>().startMove = false;
                        tray.transform.SetParent(transform);
                        tray.transform.localPosition = new Vector3(0, tray.transform.localPosition.y, -.6f);
                        decreaseScore = false;
                        ConveyorController.score++;
                        timer = 0;
                    }
                }
            }
        }
    }
}
