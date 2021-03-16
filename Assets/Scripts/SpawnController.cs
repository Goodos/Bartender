using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject trayFab;
    [SerializeField] ConveyorController conveyorController;

    void Start()
    {
        SpawnTray();
    }

    public void SpawnTray()
    {
        GameObject newTray = Instantiate(trayFab);
        newTray.transform.position = transform.position;
        conveyorController.trays.Add(newTray);        
    }
}
