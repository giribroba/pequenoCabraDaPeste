using UnityEngine;

public class pocoBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject corda, gota, iconeBalde, interagir;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int capacidade, indexArray, cBaldada = 0;
    [SerializeField] private AudioSource baldeIn;
    private SpriteRenderer cRenderer;
    private bool cAgua = false, bPoco = false, interagiu = false;
    private float profundidade;
    private RaycastHit2D[] hit;
    private Animator cAnim;
    public float vertical;

    void Start()
    {
        interagir.SetActive(false);
        cRenderer = corda.GetComponent<SpriteRenderer>();
        cAnim = corda.GetComponent<Animator>();
        cAnim.enabled = false;
        cRenderer.sprite = sprites[0];
    }

    void Update()
    {
        indexArray = (profundidade <= 0 && bPoco) ? ((cAgua) ? 2 : 1) : 0;
        cRenderer.sprite = sprites[indexArray];
        hit = Physics2D.CircleCastAll(transform.position, 1, -transform.up);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                var other = hit[i].collider.gameObject;
                if (other.tag == "Player")
                {
#if UNITY_ANDROID
                    if (other.GetComponent<controladorJogador>().balde || (bPoco && profundidade == 0))
                    {
                        interagir.SetActive(true);
                    }
                    else
                    {
                        interagir.SetActive(false);
                    }
                    if (interagiu && other.GetComponent<controladorJogador>().balde && !bPoco)
                    {
                        interagiu = false;
                        cAgua = false;
                        other.GetComponent<controladorJogador>().balde = false;
                        bPoco = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                    }
                    else if (interagiu && !other.GetComponent<controladorJogador>().balde && bPoco && profundidade <= 0)
                    {
                        interagiu = false;
                        if (capacidade <= 0)
                        {
                            this.transform.GetChild(0).gameObject.tag = "Removido";
                            Objetivo.SetObjetivo("Poço");
                            Feed();
                        }
                        bPoco = false;
                        other.GetComponent<controladorJogador>().balde = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                        if (indexArray == 2)
                        {
                            other.GetComponent<controladorJogador>().aguaCantil.fillAmount += 0.15f;
                            other.GetComponent<controladorJogador>().AddFlor();
                            other.GetComponent<controladorJogador>().contBaldada++;
                        }
                    }
                    if (bPoco)
                    {

                        if (vertical > 0)
                        {
                            profundidade -= (profundidade < 0) ? 0 : Time.deltaTime;
                            cAnim.enabled = (vertical != 0 && profundidade >= 0);
                        }
                        else if (vertical < 0)
                        {
                            profundidade += (profundidade > 1) ? 0 : Time.deltaTime;
                            cAnim.enabled = (vertical != 0 && profundidade <= 1);
                        }
                        else
                        {
                            profundidade = (Mathf.Abs(profundidade) < 0.05f) ? 0 : profundidade;
                            cAnim.enabled = false;
                        }
                        if (capacidade > 0 && profundidade >= 1 && !cAgua)
                        {
                            baldeIn.Play();
                            cAgua = true;
                            capacidade--;
                        }
                        else if (capacidade <= 0 && profundidade <= 0.08f && !cAgua)
                        {
                            cAgua = false;
                        }
                    }
#else
                    if (Input.GetButtonDown("Fire1") && other.GetComponent<controladorJogador>().balde && !bPoco)
                    {
                        cAgua = false;
                        other.GetComponent<controladorJogador>().balde = false;
                        bPoco = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                    }
                    else if (Input.GetButtonDown("Fire1") && !other.GetComponent<controladorJogador>().balde && bPoco && profundidade <= 0)
                    {
                        if (capacidade <= 0)
                        {
                            this.transform.GetChild(0).gameObject.tag = "Removido";
                            Objetivo.SetObjetivo("Poço");
                            Feed();
                        }
                        bPoco = false;
                        other.GetComponent<controladorJogador>().balde = true;
                        iconeBalde.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                        if (indexArray == 2)
                        {
                            other.GetComponent<controladorJogador>().aguaCantil.fillAmount += 0.15f;
                            other.GetComponent<controladorJogador>().AddFlor();
                            other.GetComponent<controladorJogador>().contBaldada++;
                        }
                    }
                    if (bPoco)
                    {

                        if (Input.GetAxisRaw("Vertical") > 0)
                        {
                            profundidade -= (profundidade < 0) ? 0 : Time.deltaTime;
                            cAnim.enabled = (Input.GetAxisRaw("Vertical") != 0 && profundidade >= 0);
                        }
                        else if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            profundidade += (profundidade > 1) ? 0 : Time.deltaTime;
                            cAnim.enabled = (Input.GetAxisRaw("Vertical") != 0 && profundidade <= 1);
                        }
                        else
                        {
                            profundidade = (Mathf.Abs(profundidade) < 0.05f) ? 0 : profundidade;
                            cAnim.enabled = false;
                        }
                        if (capacidade > 0 && profundidade >= 1 && !cAgua)
                        {
                            baldeIn.Play();
                            cAgua = true;
                            capacidade--;
                        }
                        else if (capacidade <= 0 && profundidade <= 0.08f && !cAgua)
                        {
                            cAgua = false;
                        }
                    }
#endif
                }
            }

        }

    }

    void Feed()
    {
        gota.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            gota.GetComponent<SpriteRenderer>().enabled = true;
            iconeBalde.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interagir.SetActive(false);
            gota.GetComponent<SpriteRenderer>().enabled = false;
            iconeBalde.GetComponent<SpriteRenderer>().enabled = false;
#if UNITY_STANDALONE
            if (collision.GetComponent<controladorJogador>().balde && collision.GetComponent<controladorJogador>().primeiroPoco)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                collision.GetComponent<controladorJogador>().primeiroPoco = collision.GetComponent<controladorJogador>().contBaldada == 0;
            }
#endif
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && col.GetComponent<controladorJogador>().balde && col.GetComponent<controladorJogador>().primeiroPoco)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Interagir()
    {
        interagiu = true;
    }
}
