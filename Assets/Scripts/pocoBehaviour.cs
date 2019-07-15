using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocoBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player, corda, gota, iconeBalde;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int capacidade;
    private SpriteRenderer cRenderer;
    private bool balde, cAgua = false, bPoco = false;
    private float profundidade;
    private RaycastHit2D[] hit;
    private Animator cAnim;

    void Start()
    {
        cRenderer = corda.GetComponent<SpriteRenderer>();
        cAnim = corda.GetComponent<Animator>();
        cAnim.enabled = false;
        cRenderer.sprite = sprites[0];
    }
    
    void Update()
    {
        cRenderer.sprite = sprites[(profundidade <= 0 && bPoco)? ((cAgua)?2 : 1) : 0];
        balde = player.GetComponent<controladorJogador>().balde;
        hit = Physics2D.CircleCastAll(transform.position, 1, -transform.up);        
        
        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i].collider != null)
            {
                var other = hit[i].collider.gameObject;
                if (other.tag == "Player")
                {
                    if(Input.GetKeyDown("e") && balde && !bPoco)
                    {
                        cAgua = false;
                        player.GetComponent<controladorJogador>().balde = false;
                        bPoco = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                    }
                    else if(Input.GetKeyDown("e") && !balde && bPoco && profundidade <= 0)
                    {
                        bPoco = false;
                        player.GetComponent<controladorJogador>().balde = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1,1,1, 0.5f);
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

        void Feed(){
                gota.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
    }

}
