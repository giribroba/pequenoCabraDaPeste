using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRotate : MonoBehaviour
{
    [HideInInspector] public float distanceRotationStart, distanceRotation, maxDistanceRotation, minDistanceRotation, defRotation;
    
    public GameObject solBackground;
    public Animator animatorRedoneta;

    void Update() {
        
        Rotation(RunnerController.instace.currentState);

    }

    float count = 0; float countAux = 0;
    float definitiveDistance;
    void Rotation(RunnerController.State currentState){


        if(currentState == RunnerController.State.afterRunner) {

            PlayerBehaviour.instance.statePlayer = PlayerBehaviour.playerState.die;

            solBackground.GetComponent<Animator>().speed = 0;
            animatorRedoneta.speed = 0;
            PlayerBehaviour.instance.myAnim.speed = 0;

            countAux = 0;

        }

            else if(currentState == RunnerController.State.inRunner) {

                    if(countAux == 2) { StartCoroutine(TimeToNotImmortal()); countAux++; }                

                    PlayerBehaviour.instance.myAnim.speed = 1;
                    animatorRedoneta.speed = 1;

                    count++;
            
                    solBackground.GetComponent<Animator>().speed = 1;
                    if(!MenuBotoesBehaviour.inPause) animatorRedoneta.speed += 0.0001f * Time.deltaTime;
                
                    if(animatorRedoneta.speed > 1.35) animatorRedoneta.speed = 1.35f * Time.deltaTime;

                    if (!MenuBotoesBehaviour.inPause) definitiveDistance = distanceRotationStart + distanceRotation * count;
                    definitiveDistance = Mathf.Clamp(definitiveDistance, minDistanceRotation, maxDistanceRotation);
                
            }

                else if(currentState == RunnerController.State.beforeRunner) { 
                    
                    countAux++;
                    
                    if(countAux > 2) countAux = 2;

                    solBackground.GetComponent<Animator>().speed = 1; 
                    if(countAux == 1) animatorRedoneta.Play("Rotation", 0, 0); animatorRedoneta.speed = 1;
                    PlayerBehaviour.instance.myAnim.speed = 1;
                    PlayerBehaviour.instance.statePlayer =  PlayerBehaviour.playerState.running;

                    PlayerBehaviour.instance.immortal = true;

                }

    }


    IEnumerator TimeToNotImmortal() {

        yield return new WaitForSeconds(1.5f);
        PlayerBehaviour.instance.immortal = false;

    }

}
