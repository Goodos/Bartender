using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] VisitorController visitorController;
    [SerializeField] Image foreground;
    [SerializeField] Image[] primitives;

    private float maxTimer;

    void Start()
    {        
        maxTimer = visitorController.timer;

        for (int i = 0; i < 3; i++)
        {
            switch (visitorController.primitiveType[i])
            {
                case 1:
                    primitives[i].sprite = Resources.Load<Sprite>("sphere");
                    break;
                case 2:
                    primitives[i].sprite = Resources.Load<Sprite>("cube");
                    break;
                case 3:
                    primitives[i].sprite = Resources.Load<Sprite>("parallel");
                    break;
                default:
                    primitives[i].sprite = null;
                    break;
            }
        }
    }

    void Update()
    {
        if (visitorController.startTimer)
        {
            if (visitorController.timer > 0)
            {
                foreground.fillAmount = visitorController.timer / maxTimer;
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0f, 180f, 0f);
    }
}
