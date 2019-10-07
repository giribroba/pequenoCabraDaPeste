using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhetoBehaviour : MonoBehaviour
{
    private Animator folhetoAmt;
    private EscreveTexto escreveText;
    private GameObject d;
    controladorJogador player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<controladorJogador>();
    }
    public void AparecerFolheto(){

        if (player.ReturnLevel() == 0)
        {
            player.SetLevel(1);
            if (folhetoAmt == null)
            {
                folhetoAmt = GetComponent<Animator>();
                escreveText = transform.GetChild(0).gameObject.GetComponent<EscreveTexto>();
            }
            folhetoAmt.SetBool("folheto", true);
            escreveText.StartCoroutine("ApareceTexto");
        }
    }
    public void DesparecerFolheto(){
        folhetoAmt.SetBool("folheto", false);
        d = transform.GetChild(1).gameObject;
        Destroy(this.d);
    }
}
