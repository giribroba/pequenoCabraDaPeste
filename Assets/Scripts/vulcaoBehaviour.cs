using System.Collections;
using UnityEngine;

public class vulcaoBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite[] sNivel;
    [SerializeField] private GameObject indicador, barra, interagir;
    [SerializeField] private Sprite[] vulcoesAbertos;
    [SerializeField] private float[] velocidadeBarra;
    [SerializeField] private Animator fumaca;
    private RaycastHit2D[] hit;
    private int nivel = 1;
    private bool direita = true, move = true, trocouVulcao = false, interagiu;
    private controladorJogador p;
    [SerializeField] private AudioSource cavar;

    void Update()
    {
        if(interagir != null)
            interagir.SetActive(false);
        indicador.GetComponent<SpriteRenderer>().enabled = false;
        barra.GetComponent<SpriteRenderer>().enabled = false;
        barra.GetComponent<SpriteRenderer>().sprite = sNivel[nivel - 1];
        hit = Physics2D.CircleCastAll(transform.position + (Vector3.up * 0.2f), 0.3f, transform.up);
        foreach (RaycastHit2D i in hit)
        {
            if (i.collider != null)
            {
                var other = i.collider.gameObject;
                //colisões
                if (other.tag == "Player" && other.GetComponent<controladorJogador>().contBroto >= 6 && ((gameObject.tag == "Balde") ? ((other.GetComponent<controladorJogador>().encontrouRosa) ? true : false) : true))
                {
                    
                    p = other.GetComponent<controladorJogador>();
                    indicador.GetComponent<SpriteRenderer>().enabled = p.podePa;
                    barra.GetComponent<SpriteRenderer>().enabled = p.podePa;
                    indicador.transform.localPosition = new Vector3(Random.Range(-4, 4), indicador.transform.localPosition.y);
                    if (p.podePa)
                    {
                        interagir.SetActive(true);
                        if (direita && move)
                        {
                            indicador.transform.Translate(Vector3.right * Time.deltaTime * velocidadeBarra[nivel - 1]);
                            direita = (indicador.transform.localPosition.x < 4.8f);
                        }
                        else if (!direita && move)
                        {
                            indicador.transform.Translate(-Vector3.right * Time.deltaTime * velocidadeBarra[nivel - 1]);
                            direita = (indicador.transform.localPosition.x <= -4.8f);
                        }
#if UNITY_ANDROID
                        if (interagiu && move)
                        {
                            interagiu = false;
                            switch (nivel)
                            {
                                case 1:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel++;
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                    }
                                    break;
                                case 2:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel++;
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 1;
                                    }
                                    break;
                                case 3:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        GameObject.FindWithTag("Sound").GetComponent<Sons>().PlaySound("vulcao");
                                        p.contVulcao += (gameObject.tag == "Balde") ? 0 : 1;
                                        p.contador.text = (p.contVulcao.ToString() + "/5");
                                        Destroy(indicador);
                                        Destroy(barra);
                                        if (p.contVulcao == 5)
                                            p.MudaIconePa();
                                        if (gameObject.tag == "Balde")
                                        {
                                            this.gameObject.tag = "Removido";
                                            Objetivo.SetObjetivo("Poço");
                                            p.balde = true;
                                            Destroy(this.gameObject);
                                        }
                                        else
                                        {
                                            this.gameObject.tag = "Removido";
                                            Objetivo.SetObjetivo("Vulcão");
                                            other.GetComponent<Animator>().SetTrigger("pasadaVulcao");
                                            Invoke("trocouVulcao", 1.5f);
                                            p.VulcaoDespisca();
                                            fumaca.SetTrigger("Fumaca");
                                        }
                                        interagir.SetActive(false);
                                        Destroy(this);
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 1;
                                    }
                                    break;
                            }
                        }
#else
                        if (Input.GetButtonDown("Fire1") && move)
                        {
                            switch (nivel)
                            {
                                case 1:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel++;
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                    }
                                    break;
                                case 2:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        StartCoroutine(Pisca(new Color(0.5f, 1, 0.5f, 1)));
                                        nivel++;
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 1;
                                    }
                                    break;
                                case 3:
                                    if (indicador.transform.localPosition.x > -1.6 && indicador.transform.localPosition.x < 1.6)
                                    {
                                        GameObject.FindWithTag("Sound").GetComponent<Sons>().PlaySound("vulcao");
                                        p.contVulcao += (gameObject.tag == "Balde") ? 0 : 1;
                                        p.contador.text = (p.contVulcao.ToString() + "/5");
                                        Destroy(indicador);
                                        Destroy(barra);
                                        if (p.contVulcao == 5)
                                            p.MudaIconePa();
                                        if (gameObject.tag == "Balde")
                                        {
                                            this.gameObject.tag = "Removido";
                                            Objetivo.SetObjetivo("Poço");
                                            p.balde = true;
                                            Destroy(this.gameObject);
                                        }
                                        else
                                        {
                                            this.gameObject.tag = "Removido";
                                            Objetivo.SetObjetivo("Vulcão");
                                            other.GetComponent<Animator>().SetTrigger("pasadaVulcao");
                                            Invoke("trocouVulcao", 1.5f);
                                            p.VulcaoDespisca();
                                            fumaca.SetTrigger("Fumaca");
                                        }
                                        Destroy(this);
                                    }
                                    else
                                    {
                                        StartCoroutine(Pisca(new Color(1, 0.5f, 0.5f, 1)));
                                        nivel = 1;
                                    }
                                    break;
                            }
                        }
#endif
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
        barra.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        barra.GetComponent<SpriteRenderer>().color = cor;
        yield return new WaitForSeconds(0.5f);
        barra.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        indicador.transform.localPosition = Vector2.zero;
        move = true;
    }
    void TrocaVulcao()
    {
        GetComponent<SpriteRenderer>().sprite = vulcoesAbertos[Random.Range(0, 2)];
    }

    public void Interagir()
    {
        print("aeee");
        interagiu = true;
    }
}
