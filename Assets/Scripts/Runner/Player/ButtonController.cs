using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void JumpButton() {

        if(PlayerBehaviour.instance.inGround || PlayerBehaviour.instance.slinding) {

            PlayerBehaviour.instance.Jump();

        }

    }

    public void SlideButton() {
    
        if(!PlayerBehaviour.instance.slinding && PlayerBehaviour.instance.inGround){

            PlayerBehaviour.instance.Slide();

        }

    }

}