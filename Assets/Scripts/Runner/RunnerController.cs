using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public enum State {beforeRunner, inRunner, afterRunner}
    public State currentState; 

    public static RunnerController instace;

    void Awake() {
        
        instace = this;

    }

}
