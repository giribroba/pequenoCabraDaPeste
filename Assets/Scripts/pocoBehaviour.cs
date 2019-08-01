using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocoBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject corda, gota, iconeBalde;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int capacidade, indexArray, cBaldada = 0;
    private SpriteRenderer cRenderer;
    private bool cAgua = false, bPoco = false;
    private float profundidade;
    private RaycastHit2D[] hit;
    private Animator cAnim;
    [SerializeField] private AudioSource baldeIn;

    void Start(){
        cRenderer = corda.GetComponent<SpriteRenderer>();
        cAnim = corda.GetComponent<Animator>();
        cAnim.enabled = false;
        cRenderer.sprite = sprites[0];
    }
    
    void Update(){
        indexArray = (profundidade <= 0 && bPoco) ? ((cAgua) ? 2 : 1) : 0;
        cRenderer.sprite = sprites[indexArray];
        hit = Physics2D.CircleCastAll(transform.position, 1, -transform.up);        
        
        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i].collider != null)
            {
                var other = hit[i].collider.gameObject;
                if (other.tag == "Player")
                {                    
                    if(Input.GetKeyDown("e") && other.GetComponent<controladorJogador>().balde && !bPoco)
                    {
                        cAgua = false;
                        other.GetComponent<controladorJogador>().balde = false;
                        bPoco = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                    }
                    else if(Input.GetKeyDown("e") && !other.GetComponent<controladorJogador>().balde && bPoco && profundidade <= 0)
                    {                        
                        bPoco = false;
                        other.GetComponent<controladorJogador>().balde = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1,1,1, 0.5f);
                        if (indexArray == 2){
                            other.GetComponent<controladorJogador>().aguaCantil.fillAmount += 0.15f;
                            other.GetComponent<controladorJogador>().AddFlor();
                            other.GetComponent<controladorJogador>().contBaldada++;
                        }
                    }
                    if(bPoco)
                    {
                        if(Input.GetAxisRaw("Vertical") > 0)
                        {
                            profundidade -= (profundidade < 0)? 0 : Time.deltaTime;
                            cAnim.enabled = (Input.GetAxisRaw("Vertical") != 0 && profundidade >= 0);
                        }
                        else if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            profundidade += (profundidade > 1) ? 0 : Time.deltaTime;
                            cAnim.enabled = (Input.GetAxisRaw("Vertical") != 0 && profundidade <= 1);
                        }
                        else
                        {
                            profundidade = (Mathf.Abs(profundidade) < 0.05f)? 0 : profundidade;
                            cAnim.enabled = false;
                        }
                        if(capacidade > 0 && profundidade  >= 1 && !cAgua)
                        {            
                            baldeIn.Play();                
                            cAgua = true;
                            capacidade--;
                        }
                        else if(capacidade <= 0 && profundidade >= 1 && !cAgua)
                        {
                            Feed();
                            cAgua = false;
                        }
                    }
                }
            }
            
        }

    }

    void Feed(){
        gota.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            gota.GetComponent<SpriteRenderer>().enabled = true;
            iconeBalde.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "Player"){
            gota.GetComponent<SpriteRenderer>().enabled = false;
            iconeBalde.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
