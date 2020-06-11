using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRotate : MonoBehaviour
{
    public float distanceRotationStart, distanceRotation, maxDistanceRotation, minDistanceRotation;
    void Update() {
        
        Rotation(RunnerController.instace.currentState);

    }

    float count = 0;
    void Rotation(RunnerController.State currentState){

        if(currentState == RunnerController.State.afterRunner) { return; }
        
        else if(currentState == RunnerController.State.inRunner){

            count++;

            float definitiveDistance = distanceRotationStart + distanceRotation * count;
            definitiveDistance = Mathf.Clamp(definitiveDistance, minDistanceRotation, maxDistanceRotation);
            
            this.transform.Rotate(new Vector3(0f, 0f, definitiveDistance), Space.World);

            Debug.ClearDeveloperConsole();
            Debug.Log(definitiveDistance);
            
        }

    }

}
