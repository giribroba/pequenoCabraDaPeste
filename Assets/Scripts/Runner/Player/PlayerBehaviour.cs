using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;
    enum playerState { running, jumping, sliding }
    playerState statePlayer;    

    public bool slinding, inGround; public LayerMask itsGround;
    [HideInInspector] public Rigidbody2D myRB; public float jumpForce;
    [HideInInspector] public Animator myAnim;
    [HideInInspector] public CapsuleCollider2D[] colliders;
    
    void Awake() {

        instance = this;

    }

    void Update() {

        controllerCharacter();

        Debug.Log(statePlayer);        

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

        //Aceleration in jump moment.
        if (this.transform.position.y >= 5.88) {

            this.transform.position = Vector2.MoveTowards(this.transform.position,
                new Vector2(this.transform.position.x + 1f, this.transform.position.y), 1f * Time.deltaTime);

        }
            else if (this.transform.position.y <= 4.7f) {

                this.transform.position = Vector2.MoveTowards(this.transform.position,
                    new Vector2(this.transform.position.x - 1f, this.transform.position.y), 1f * Time.deltaTime);

            }

        //Lock position for best position of axis X.
        this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x, -6.34f, -6.10f), this.transform.position.y);

        //Switch defining game occurrence in each player state.
        switch (statePlayer)
        {
            
            case playerState.jumping:

                myAnim.SetBool("Running", false);
                
                colliders[0].enabled = true;
                colliders[1].enabled = false;

            break;

            case playerState.running:
                    
                myAnim.SetBool("Running", true);

                colliders[0].enabled = true;
                colliders[1].enabled = false;

            break;

            case playerState.sliding:
                    
                myAnim.SetBool("Running", false);

                colliders[0].enabled = false;
                colliders[1].enabled = true;

            break;

        }

        myAnim.SetBool("Jump", !inGround);
        myAnim.SetBool("Sliding", slinding);

        //If's defining which player's current state. 
        if(inGround && !slinding) { statePlayer = playerState.running; }
            else if(!inGround && !slinding) { statePlayer = playerState.jumping; }
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

        float timeSlide = 0.8f + (RunnerController.instace.runnerRotate.maxDistanceRotation - RunnerController.instace.runnerRotate.defRotation) * .8f;
        yield return new WaitForSeconds(timeSlide);

        slinding = false;

        Debug.Log(timeSlide);

    }

}
