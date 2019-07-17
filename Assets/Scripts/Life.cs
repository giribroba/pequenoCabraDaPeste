using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    [SerializeField] private int maxLife;
    private int life;
    [SerializeField] GameObject[] spriteHearts;
    [SerializeField] Animator menu;

    void Start() {
        life = maxLife;
        StartCoroutine(LowerUpdate());
        menu.SetBool("perde", false);
    }

    private void SetHearts(int curLife){
        for(int i = 0; i < spriteHearts.Length; i++){
            if(i > (curLife - 1)){
                Animator anim = spriteHearts[i].GetComponent<Animator>();
                anim.SetTrigger("Broken");
            }
        }
    }

    public void DecrementLife(){
        life--;
        if(life > maxLife)
            life = maxLife;
        else if(life <= 0)
            Invoke("ResetScene", 0.2f);
            gameObject.GetComponent<AudioSource>().Play();
    }

    IEnumerator LowerUpdate(){
        for(;;){
            yield return new WaitForSeconds(0.01f);
            SetHearts(life);
        }
    }

    void ResetScene(){
        menu.SetBool("perde", true);
    }
}
