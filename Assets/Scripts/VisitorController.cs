using UnityEngine;

public class VisitorController : MonoBehaviour
{
    [SerializeField] CanvasController canvasController;

    ConveyorController conveyorController;
    private Vector3 targetPosition;
    private Vector3 directionVector;
    private Vector3 startPos;
    private float speed = 4f;

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
                if (tray.GetComponent<TrayMovement>().primitiveType[0] == primitiveType[0] && tray.GetComponent<TrayMovement>().primitiveType[1] == primitiveType[1]
                    && tray.GetComponent<TrayMovement>().primitiveType[2] == primitiveType[2])
                {
                    if (tray.transform.position.x >= transform.position.x && tray.transform.position.x - transform.position.x <= .5f)
                    {
                        tray.GetComponent<TrayMovement>().startMove = false;
                        tray.transform.SetParent(transform);
                        tray.transform.localPosition = new Vector3(0, tray.transform.localPosition.y, -.6f);
                        ConveyorController.score++;
                        timer = 0;
                    }
                }
            }
        }
    }
}
