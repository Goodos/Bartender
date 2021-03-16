using UnityEngine;

public class TrayMovement : MonoBehaviour
{
    private float speed = 2;
    public bool startMove = false;
    public int[] primitiveType = new int[] { 0, 0, 0 };


    void Update()
    {
        if (startMove)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (transform.position.x >= 4.7f)
        {
            ConveyorController.score--;
            Destroy(gameObject);
        }
    }
}
