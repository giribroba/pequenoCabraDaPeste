using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRotate : MonoBehaviour
{
    [HideInInspector] public float maxDistanceRotation, defRotation, speedRedoneta;
    
    public GameObject solBackground;
    public Animator animatorRedoneta;

    void Update() {
        
        Rotation(RunnerController.instace.currentState);

        //Apagar depois
        //Debug.LogWarning("Speed Redoneta: " + animatorRedoneta.speed);

    }

    float count = 0; float countAux = 0;
    void Rotation(RunnerController.State currentState){


        if(currentState == RunnerController.State.afterRunner) {

            PlayerBehaviour.instance.statePlayer = PlayerBehaviour.playerState.die;

            solBackground.GetComponent<Animator>().speed = 0;
            animatorRedoneta.speed = 0;
            PlayerBehaviour.instance.myAnim.speed = 0;

            countAux = 0;

        }

            else if(currentState == RunnerController.State.inRunner) {

                    if(countAux == 2) { 

                        StartCoroutine(TimeToNotImmortal()); 
                        countAux++; 
                        animatorRedoneta.speed = 1;

                    }

                     solBackground.GetComponent<Animator>().speed = 1.5f;
                     PlayerBehaviour.instance.myAnim.speed = 1;

                    if (PlayerBehaviour.instance.statePlayer == PlayerBehaviour.playerState.jumping || PlayerBehaviour.instance.statePlayer == PlayerBehaviour.playerState.sliding) {

                        animatorRedoneta.speed = speedRedoneta * 1.1f;

                    }
                        else {

                            animatorRedoneta.speed = speedRedoneta;
                            if (!MenuBotoesBehaviour.inPause) animatorRedoneta.speed += 0.0001f * Time.deltaTime;
                            speedRedoneta = animatorRedoneta.speed;

                        }


            }

                else if(currentState == RunnerController.State.beforeRunner) { 
                    
                    countAux++;
                    
                    if(countAux > 2) countAux = 2;

                    solBackground.GetComponent<Animator>().speed = 1;
                    PlayerPrefs.SetFloat("SpeedRedonetaBeforeJumpOrSlide", 1);
                    if (countAux == 1) animatorRedoneta.Play("Rotation", 0, 0); animatorRedoneta.speed = 1;
                    speedRedoneta = 1;
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
