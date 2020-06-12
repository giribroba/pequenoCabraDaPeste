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

        inGround = Physics2D.OverlapCircle(new Vector2(0f, (this.gameObject.transform.position.y - .3f) - 
        (playerHeigh / 2)), .2f, itsGround);


        //Switch defining game occurrence in each player state.
        switch (statePlayer)
        {
            
            case playerState.jumping:

                myAnim.SetBool("Running", false);

            break;

            case playerState.running:
                    
                myAnim.SetBool("Running", true);

            break;

            case playerState.sliding:
                    
                myAnim.SetBool("Running", false);

            break;

        }

        myAnim.SetBool("Jump", !inGround);
        myAnim.SetBool("Sliding", slinding);

        //If's defining which player's current state. 
        if(inGround && !slinding) { statePlayer = playerState.running; }
            else if(!inGround) { statePlayer = playerState.jumping; }
                else { statePlayer = playerState.sliding; }

    }

    //Method responsible for the jump.
    public void Jump() {

        myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);

    }

}
