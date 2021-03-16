using System.Collections;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] GameObject visitorFab;
    private Vector3[] visitorPlaces = new Vector3[] { new Vector3(-2f, .6f, -2f), new Vector3(0, .6f, -2f), new Vector3(2f, .6f, -2f) };
    private int placeCounter = 2;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            GameObject newVisitor = Instantiate(visitorFab);
            newVisitor.transform.position = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z);
            newVisitor.GetComponent<VisitorController>().target = visitorPlaces[placeCounter];
            newVisitor.GetComponent<VisitorController>().timer = Random.Range(5, 11);
            if (placeCounter == 0)
            {
                placeCounter = 2;
            }
            else
            {
                placeCounter--;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
