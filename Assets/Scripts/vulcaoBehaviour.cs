using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vulcaoBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite[] sNivel;
    [SerializeField] private GameObject indicador, barra;
    private RaycastHit2D[] hit;
    private int nivel = 3;
    private bool direita = true, interagindo = false, move = true;

    void Start()
    {
    }

    void Update()
    {
        indicador.GetComponent<SpriteRenderer>().enabled = false;
        barra.GetComponent<SpriteRenderer>().enabled = false; 
        barra.GetComponent<SpriteRenderer>().sprite = sNivel[nivel - 1];
        hit = Physics2D.CircleCastAll(transform.position + (Vector3.up * 0.2f), 0.3f, transform.up);
        foreach(RaycastHit2D i in hit)
        {
            if(i.collider != null)
            {
                var other = i.collider.gameObject;
                //colisões
                if(other.tag == "Player")
                {
                    indicador.GetComponent<SpriteRenderer>().enabled = true;
                    barra.GetComponent<SpriteRenderer>().enabled = true;
                    other.GetComponent<controladorJogador>().iVulcao = interagindo;
                    if(interagindo)
                    {
                        other.GetComponent<controladorJogador>().velocidade = 0;
                        if(direita && move)
                        {
                            indicador.transform.Translate(Vector3.right * Time.deltaTime);
                            direita = (indicador.transform.localPosition.x < 4.8f);
                        }
                        else if(!direita && move)
                        {
                            indicador.transform.Translate(-Vector3.right * Time.deltaTime);
                            direita = (indicador.transform.localPosition.x <= -4.8f);
                        }
                        if(Input.GetKeyDown("e") && move)
                        {
                            switch(nivel)
                            {
                                case 3:
                                    if(indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel--;
                                        indicador.transform.localPosition = Vector2.zero;
                                    }
                                    else
                                    {
                                        indicador.transform.localPosition = Vector2.zero;
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                    }
                                break;    
                                    case 2:
                                    if(indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel--;
                                        indicador.transform.localPosition = Vector2.zero;
                                    }
                                    else
                                    {
                                        indicador.transform.localPosition = Vector2.zero;
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 3;
                                    }
                                break;
                                    case 1:
                                    if(indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        Destroy(gameObject);
                                    }
                                    else
                                    {
                                        indicador.transform.localPosition = Vector2.zero;
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 3;
                                    }
                                break;
                            }
                        }
                        if(Input.GetKeyDown("q"))
                        {
                            interagindo = false;
                        }
                    }
                    if(Input.GetKeyDown("e"))
                    {
                        interagindo = true;
                    }    
                }
            }
        }
    }

    IEnumerator Pisca(Color cor)
    {
        move = false;
        barra.GetComponent<SpriteRenderer>().color = cor;
        yield return new WaitForSeconds(0.5f);
        barra.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        yield return new WaitForSeconds(0.5f);
        barra.GetComponent<SpriteRenderer>().color = cor;
        yield return new WaitForSeconds(0.5f);
        barra.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        move = true;
    }
}
