using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button sphereButton;
    [SerializeField] Button cubeButton;
    [SerializeField] Button parralelButton;
    [SerializeField] Button spawnTrayButton;
    [SerializeField] Button restartButton;

    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject endWindow;

    [SerializeField] Text time;
    [SerializeField] Text score;

    [SerializeField] GameObject sphere;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject parralel;

    [SerializeField] ConveyorController conveyorController;
    [SerializeField] SpawnController spawnController;

    private int currentTray = -1;
    private float gameTimer = 60f;
    private bool endGame = true;

    void Start()
    {
        sphereButton.onClick.AddListener(SpawnSphere);
        cubeButton.onClick.AddListener(SpawnCube);
        parralelButton.onClick.AddListener(SpawnParralel);
        spawnTrayButton.onClick.AddListener(StartMove);
        restartButton.onClick.AddListener(Restart);
    }

    void Update()
    {
        score.text = "Score: " + ConveyorController.score.ToString();
        TimeSpan sec = TimeSpan.FromSeconds(gameTimer);
        time.text = "Time: " + sec.ToString("mm':'ss");
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0 && endGame)
        {
            gameUi.SetActive(false);
            endWindow.SetActive(true);
            Time.timeScale = 0;
            endGame = false;
        }
    }

    void SpawnSphere()
    {
        if (currentTray >= 0 && conveyorController.trays[currentTray] != null)
        {
            if (conveyorController.trays[currentTray].transform.childCount < 4)
            {
                GameObject newSphere = Instantiate(sphere, conveyorController.trays[currentTray].transform);
                switch (conveyorController.trays[currentTray].transform.childCount)
                {
                    case 2:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[0] = 1;
                        newSphere.transform.localPosition = new Vector3(0.103f, .15f, -0.059f);
                        break;
                    case 3:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[1] = 1;
                        newSphere.transform.localPosition = new Vector3(-0.134f, .15f, -0.082f);
                        break;
                    case 4:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[2] = 1;
                        newSphere.transform.localPosition = new Vector3(0f, .25f, 0.138f);
                        break;
                }
            }
        }
    }

    void SpawnCube()
    {
        if (currentTray >= 0 && conveyorController.trays[currentTray] != null)
        {
            if (conveyorController.trays[currentTray].transform.childCount < 4)
            {
                GameObject newCube = Instantiate(cube, conveyorController.trays[currentTray].transform);
                switch (conveyorController.trays[currentTray].transform.childCount)
                {
                    case 2:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[0] = 2;
                        newCube.transform.localPosition = new Vector3(0.103f, .15f, -0.059f);
                        break;
                    case 3:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[1] = 2;
                        newCube.transform.localPosition = new Vector3(-0.134f, .15f, -0.082f);
                        break;
                    case 4:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[2] = 2;
                        newCube.transform.localPosition = new Vector3(0f, .25f, 0.138f);
                        break;
                }
            }
        }
    }

    void SpawnParralel()
    {
        if (currentTray >= 0 && conveyorController.trays[currentTray] != null)
        {
            if (conveyorController.trays[currentTray].transform.childCount < 4)
            {
                GameObject newParralel = Instantiate(parralel, conveyorController.trays[currentTray].transform);
                switch (conveyorController.trays[currentTray].transform.childCount)
                {
                    case 2:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[0] = 3;
                        newParralel.transform.localPosition = new Vector3(0.103f, .15f, -0.059f);
                        break;
                    case 3:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[1] = 3;
                        newParralel.transform.localPosition = new Vector3(-0.134f, .15f, -0.082f);
                        break;
                    case 4:
                        conveyorController.trays[currentTray].GetComponent<TrayMovement>().primitiveType[2] = 3;
                        newParralel.transform.localPosition = new Vector3(0f, .25f, 0.138f);
                        break;
                }
            }
        }
    }

    void StartMove()
    {
        currentTray++;
        if (conveyorController.trays[currentTray] != null)
        {
            conveyorController.trays[currentTray].GetComponent<TrayMovement>().startMove = true;
        }
        StartCoroutine(SpawnNewTray());
    }

    IEnumerator SpawnNewTray()
    {
        spawnTrayButton.interactable = false;
        yield return new WaitForSeconds(1f);
        spawnController.SpawnTray();
        spawnTrayButton.interactable = true;
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
