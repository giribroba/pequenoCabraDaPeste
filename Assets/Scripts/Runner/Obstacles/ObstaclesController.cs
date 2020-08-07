using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstaclesController : MonoBehaviour
{
    public List<GameObject> pointsObstacles;
    public GameObject obstaclePrefable;

    public static ObstaclesController instace;
    public bool snakeMoment; public float forSnake, levelDificulty;
    [Range(0, 10)]
    public float time;

    public string sequence;
    public Text txtStartingTimer;

    float varAuxForStart = 0;

    void Awake() {

        forSnake = 0;

        SetPointsObstacles(pointsObstacles);
        instace = this;

    }

    private void Update() {

        Time.timeScale = time;

        if (RunnerController.instace.currentState == RunnerController.State.beforeRunner) {

            levelDificulty = 0; forSnake = 0;
            if (varAuxForStart == 0) {

                for (int i = 24; i < 36; i++) {

                    pointsObstacles[i].GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 0);

                }

                for (int i = 0; i < pointsObstacles.Count; i++) {

                    pointsObstacles[i].SetActive(true); pointsObstacles[i].transform.GetChild(1)
                    .GetComponent<ObstaclesBehaviour>().typeObstacle = ObstaclesBehaviour.TypeObstacle.nothing;

                }

                StartCoroutine(StartingTimer());

            }

            varAuxForStart++;

        }
            else if (RunnerController.instace.currentState == RunnerController.State.inRunner) {

                if(pointsObstacles[24] != null && pointsObstacles[24].GetComponentInChildren<SpriteRenderer>().color == new Color(255, 255, 255, 0)) StartCoroutine(ReturnObstracles());
                
            }
                else {

                    varAuxForStart = 0;
                    StopAllCoroutines();

                }

    }

    void SetPointsObstacles(List<GameObject> pointsObstacles) {

        for (int i = 1; i < 37; i++)
        {
            
            if(i < 10) pointsObstacles.Add(GameObject.Find("Slot (0"+ i + ")"));
                else pointsObstacles.Add(GameObject.Find("Slot (" + i + ")"));
            
            AddPrefable(pointsObstacles[i - 1], obstaclePrefable);

        }

    }

    void AddPrefable(GameObject parent, GameObject prefable) {

        GameObject temp = Instantiate(prefable, Vector3.zero, Quaternion.identity, parent.transform);
        temp.transform.position = parent.transform.GetChild(0).position;

    }

    IEnumerator StartingTimer() {

        txtStartingTimer.text = "3";
        yield return new WaitForSeconds(1);

        txtStartingTimer.text = "2";
        yield return new WaitForSeconds(1);

        txtStartingTimer.text = "1";
        yield return new WaitForSeconds(1);

        txtStartingTimer.text = "Vai!";
        RunnerController.instace.currentState = RunnerController.State.inRunner;
        yield return new WaitForSeconds(.5f);
        txtStartingTimer.text = "";

    }

    IEnumerator ReturnObstracles() {

        yield return new WaitForSeconds(12);

        for (int i = 24; i < 35; i++) {

            pointsObstacles[i].GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);

        }

    }

}