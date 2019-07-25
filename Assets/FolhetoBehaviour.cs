using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhetoBehaviour : MonoBehaviour
{
    private Animator folhetoAmt;
    private EscreveTexto escreveText;
    
    public void AparecerFolheto(){
        if(folhetoAmt == null){
            folhetoAmt = GetComponent<Animator>();
            escreveText = transform.GetChild(0).gameObject.GetComponent<EscreveTexto>();
        }
        folhetoAmt.SetBool("folheto", true);
        escreveText.StartCoroutine("ApareceTexto");
    }
    public void DesparecerFolheto(){
        folhetoAmt.SetBool("folheto", false);
    }
}
