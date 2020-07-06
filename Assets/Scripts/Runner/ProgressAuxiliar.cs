using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAuxiliar : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision) {
        
        if(collision.gameObject.name == "Image Progress") {

            Progress.instance.endGame = true;

        }

    }

}
