using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public List<GameObject> pointsObstacles;
    public GameObject obstaclePrefable;

    void Awake() {
        
        SetPointsObstacles(pointsObstacles);

    }

    void SetPointsObstacles(List<GameObject> pointsObstacles) {

        for (int i = 1; i < 37; i++)
        {
            
            pointsObstacles.Add(GameObject.Find("Slot (" + i + ")"));
            AddPrefable(pointsObstacles[i - 1], obstaclePrefable);

        }

    }

    void AddPrefable(GameObject parent, GameObject prefable) {

        GameObject temp = Instantiate(prefable, Vector3.zero, Quaternion.identity, parent.transform);
        temp.transform.position = parent.transform.GetChild(0).position;

    }

}
    