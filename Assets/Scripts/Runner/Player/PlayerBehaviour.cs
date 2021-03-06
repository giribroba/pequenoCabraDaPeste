﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;
    public enum playerState { running, jumping, sliding, die }
    public playerState statePlayer;    

    public bool slinding, inGround, immortal; public LayerMask itsGround;
    [HideInInspector] public Rigidbody2D myRB; public float jumpForce;
    [HideInInspector] public Animator myAnim, gameOver;
    [HideInInspector] public CapsuleCollider2D[] colliders;
    
    void Awake() {

        instance = this;

    }

    void Update() {

        controllerCharacter();
        if (Input.GetKeyDown(KeyCode.I)) immortal = !immortal;

    }

    //Method responsible for controlling the behavior of the character.
    void controllerCharacter() {

        //Checking if it's on the floor.
        float playerHeigh = this.gameObject.GetComponent<CapsuleCollider2D>().bounds.size.y / 2;

        float ovelarpCircleAxisY = 0;
        if(!slinding) ovelarpCircleAxisY = this.gameObject.transform.position.y - .3f;
        if(slinding) ovelarpCircleAxisY = this.gameObject.transform.position.y - 1f;
        
        inGround = Physics2D.OverlapCircle(new Vector2(0f, (this.gameObject.transform.position.y - 1f) - 
        (playerHeigh / 2)), .2f, itsGround);

        //Lock position for best position of axis X.
        this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x, -6.34f, -6.10f), this.transform.position.y);

        //Touch in screen - Jump & Slide
        if(Input.touchCount > 0 && RunnerController.instace.currentState ==RunnerController.State.inRunner) {

            Touch touch = Input.GetTouch(Input.touchCount - 1);

            if(touch.phase == TouchPhase.Began) {

                if (touch.position.x > Screen.width / 2 && statePlayer == playerState.running) Slide();
                else if (touch.position.x < Screen.width / 2 && statePlayer != playerState.jumping) Jump();

            }

        }

        if (RunnerController.instace.currentState == RunnerController.State.inRunner) {

            if (Input.GetKeyDown(KeyCode.A) && statePlayer != playerState.jumping) Jump();
            else if (Input.GetKeyDown(KeyCode.D) && statePlayer == playerState.running) Slide();

        }

        //Switch defining game occurrence in each player state.
        switch (statePlayer)
        {
            
            case playerState.jumping:

                myAnim.SetBool("Running", false);
                myAnim.SetBool("Die", false);

                gameOver.SetBool("perde", false);

                colliders[0].enabled = true;
                colliders[1].enabled = false;

            break;

            case playerState.running:
                    
                myAnim.SetBool("Running", true);
                myAnim.SetBool("Die", false);

                gameOver.SetBool("perde", false);

                colliders[0].enabled = true;
                colliders[1].enabled = false;

            break;

            case playerState.sliding:
                    
                myAnim.SetBool("Running", false);   
                myAnim.SetBool("Die", false);

                gameOver.SetBool("perde", false);

                colliders[0].enabled = false;
                colliders[1].enabled = true;

            break;

            case playerState.die:

                myAnim.SetBool("Running", false);
                myAnim.SetBool("Die", true);

                gameOver.SetBool("perde", true);

                colliders[0].enabled = true;
                colliders[1].enabled = false;

            break;

        }

        myAnim.SetBool("Jump", !inGround);
        myAnim.SetBool("Sliding", slinding);

        //If's defining which player's current state. 
        if(inGround && !slinding && RunnerController.instace.currentState != RunnerController.State.afterRunner) { statePlayer = playerState.running; }
            else if(!inGround && !slinding) { statePlayer = playerState.jumping; }
                else if(statePlayer == playerState.die);
                    else { statePlayer = playerState.sliding; }

    }

    //Method responsible for the jump.
    public void Jump() {
        
        Coroutine slideTemp = StartCoroutine(slideTiming());
        StopCoroutine(slideTemp);

        slinding = false;

        myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);

    }

    //Method responsible for the Slide.
    public void Slide() {

        StartCoroutine(slideTiming());

    }

    //IEnumerator auxiliary to the Slide method.
    IEnumerator slideTiming(){

        slinding = true;

        float timeSlide = 0.16f + (RunnerController.instace.runnerRotate.maxDistanceRotation - RunnerController.instace.runnerRotate.defRotation) * .8f;
        yield return new WaitForSeconds(timeSlide);

        slinding = false;

    }

    //Collisions in Player Game Object.
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Obstacles" && !immortal && 
                collision.GetComponent<ObstaclesBehaviour>().typeObstacle != ObstaclesBehaviour.TypeObstacle.nothing) {

            RunnerController.instace.currentState = RunnerController.State.afterRunner;

        }
            
    }

}
