﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controladorJogador : MonoBehaviour
{
    [SerializeField] private float velocidadeMaxima, forcaPulo, aceleracao, pKB;
    [SerializeField] private GameObject planeta, vida, iconePa, iconeBroto, pause, interagir, mobileButtons;
    public static float aclPubli;
    [SerializeField] private Sprite terra, imgVulcao;
    [SerializeField] private Sprite[] florUI;
    public Text contador;
    public Image cantil, aguaCantil, flor;
    [HideInInspector] public float velocidade;
    public bool balde = false, primeiroPoco = true, imortal = false, podePa = false, encontrouRosa = false, parar, jaEnsinou = false;
    [SerializeField] private bool noChao, iconeUmaVez = true;
    private bool pulou, coletar;
    private float movimento, KB;
    public int contBroto = 0, contVulcao, contBaldada = 0;
    private string balaoNoSim = "Broto";
    private Animator animator;
    private RaycastHit2D[] hit;
    private SpriteRenderer renderer;
    private Rigidbody2D rbPlayer;
    public GameObject tocaSons;
    [SerializeField] private Joystick joystick;
    [SerializeField] private PuloJoystick puloJoystick; 
    public int level;
    void Start()
    {
#if UNITY_ANDROID
        mobileButtons.SetActive(true);
        jaEnsinou = true;
#else
        mobileButtons.SetActive(false);
#endif
        Time.timeScale = 1;
        Objetivo.SetObjetivo("Pá");
        level = 0;
        contVulcao = 0;
        tocaSons = GameObject.FindWithTag("Sound");
        podePa = false;
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        parar = true;
        Invoke("NoParar", 6.5f);
    }
    void Update()
    {

        aceleracao = aclPubli;

        if (Input.GetButtonDown("Cancel"))
        {
            if (pause.GetComponent<Animator>().GetBool("perde"))
            {
                pause.GetComponent<MenuBotoesBehaviour>().Despausa();
            }
            else
            {
                pause.GetComponent<Animator>().SetBool("perde", !pause.GetComponent<Animator>().GetBool("perde"));
                Time.timeScale = 0;
            }
        }
#if UNITY_STANDALONE
        movimento = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID
        movimento = joystick.Horizontal;
#endif
        if (contBroto == 6)
        {
            Objetivo.SetObjetivo("Vulcão");
            contador.text = (contVulcao.ToString() + "/5");
            iconeBroto.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            iconeBroto.GetComponent<Image>().sprite = imgVulcao;
            iconeBroto.GetComponent<IconesBehaviour>().Comeca();
            contBroto++;
        }
        if (contVulcao == 5)
        {
            contVulcao++;
            Objetivo.SetObjetivo("Rosa");
        }
        if (contBaldada == 6)
        {
            contBaldada++;
            Objetivo.SetObjetivo("Rosa");
        }
        Jump();
        Raycasts();
    }
    public void ResetLeve2()
    {

    }
    public void ResetLeve3()
    {

    }
    public void SetLevel(int a)
    {
        level = a;
    }
    public int ReturnLevel()
    {
        return level;
    }
    public void UpdateLevel()
    {
        if (contBroto == 6 && contVulcao < 5)
        {
            SetLevel(2);
        }
        else if (contVulcao >= 5)
        {
            SetLevel(3);
        }

    }

    void Raycasts()
    {
        hit = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.8f), -transform.up, 0.7f);
        noChao = Physics2D.OverlapCircle(transform.position - new Vector3(0, 0.84f), 1f, LayerMask.GetMask("Chao"));
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
#if UNITY_ANDROID
                interagir.SetActive(hit[i].collider.gameObject.tag == "Broto" && podePa);  
#endif
                if (hit[i].collider.gameObject.tag == "Broto")
                {
                    var other = hit[i].collider.gameObject;
                    if (other.tag == "Broto" && (Input.GetButtonDown("Fire1") || coletar) && podePa && !(contBroto >= 6) && velocidade <= 0.0001)
                    {
                        coletar = false;
                        other.tag = "Removido";
                        Objetivo.SetObjetivo("Broto");
                        other.transform.GetChild(1).gameObject.SetActive(false);
                        tocaSons.GetComponent<Sons>().PlaySound("broto");
                        contBroto++;
                        contador.text = (contBroto.ToString() + "/6");
                        animator.SetTrigger("pasada");
                        other.GetComponent<SpriteRenderer>().sprite = terra;
                        //other.GetComponent<Transform>().localScale /= 2;
                        Destroy(other.GetComponent<BoxCollider2D>());
                        iconeBroto.GetComponent<IconesBehaviour>().QuebraRepeticao();
                        iconeBroto.GetComponent<Image>().enabled = true;
                        iconeBroto.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        other.GetComponent<Broto>().Poeira();
                        parar = true;
                        Invoke("NoParar", 0.8f);
                    }
                    else if (other.tag == "Broto" && Input.GetButtonDown("Fire1") && !podePa && iconeUmaVez)
                    {
                        iconeUmaVez = false;
                        iconePa.GetComponent<IconesBehaviour>().Comeca();
                    }

                }
            }
        }

    }
    public void VulcaoDespisca()
    {
        iconeBroto.GetComponent<IconesBehaviour>().QuebraRepeticao();
        iconeBroto.GetComponent<IconesBehaviour>().Pisca();
        iconeBroto.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        planeta.transform.eulerAngles = new Vector3(planeta.transform.eulerAngles.x, planeta.transform.eulerAngles.y, planeta.transform.eulerAngles.z + velocidade + KB);
        animator.SetBool("walk", velocidade != 0 && Mathf.Abs(movimento) >= 0.005f);
        if (Mathf.Abs(KB) < 0.1f)
        {
            KB = 0;
        }
        else if (KB > 0)
        {
            KB -= Time.deltaTime * pKB;
        }
        else if (KB < 0)
        {
            KB += Time.deltaTime * pKB;
        }
        if (movimento > 0 && !parar)
        {
            //velocidade = (velocidade < velocidadeMaxima) ? velocidade += Time.deltaTime * aceleracao : velocidade = velocidadeMaxima;
            velocidade = movimento * aceleracao;
            renderer.flipX = false;
        }
        else if (movimento < 0 && !parar)
        {
            velocidade = movimento * aceleracao;
            //velocidade = (velocidade > -velocidadeMaxima) ? velocidade -= Time.deltaTime * aceleracao : velocidade = -velocidadeMaxima;
            renderer.flipX = true;
        }
        else
        {
            if (Mathf.Abs(velocidade) < 0.1f)
            {
                velocidade = 0;
            }
            else if (velocidade > 0)
            {
                velocidade -= Time.deltaTime * 1.5f;
            }
            else if (velocidade < 0)
            {
                velocidade += Time.deltaTime * 1.5f;
            }
        }
    }

    void Jump()
    {
#if UNITY_STANDALONE
        pulou = Input.GetButtonDown("Jump");
#elif UNITY_ANDROID
        pulou = puloJoystick.pulou;
#endif
        animator.SetBool("jump", !noChao);
        if (noChao && pulou)
        {
#if UNITY_ANDROID
            puloJoystick.pulou = false;
#endif
            tocaSons.GetComponent<Sons>().PlaySound("pulo");
            rbPlayer.velocity = new Vector2(0, forcaPulo);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cacto" && !imortal)
        {
            tocaSons.GetComponent<Sons>().PlaySound("hit");
            KB = (transform.position.x < collision.gameObject.transform.position.x) ? -0.5f : 0.5f;
            vida.GetComponent<Life>().DecrementLife();
            GetComponent<DanoPlayerPisca>().Comeca();
            Invoke("VisualNormal", 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Pá")
        {
            Objetivo.SetObjetivo("Broto");
            tocaSons.GetComponent<Sons>().PlaySound("pá");
            podePa = true;
            Destroy(iconePa.GetComponent<IconesBehaviour>());
            iconePa.GetComponent<Image>().enabled = true;
            iconePa.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            iconeBroto.GetComponent<IconesBehaviour>().Comeca();
            Destroy(other.gameObject);
        }
        else if (other.tag == balaoNoSim && podePa && !jaEnsinou)
        {
            other.transform.GetChild(1).gameObject.SetActive(true);
            jaEnsinou = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "eventRosa" && contVulcao >= 5)
        {
            print("adad");
            Objetivo.SetObjetivo("Balde");
            tocaSons.GetComponent<Sons>().PlaySound("rosa");
            col.gameObject.transform.parent.GetComponent<Animator>().SetBool("murchando", true);
            RealcaIconePa();
            encontrouRosa = true;
            parar = true;
            flor.GetComponent<Animator>().SetBool("encontrouRosa", true);
            Invoke("NoParar", 2f);
            Destroy(col.gameObject);
        }
        if (col.tag == "eventWins" && contVulcao >= 5 && contBaldada >= 6)
        {
            col.gameObject.transform.parent.GetComponent<Animator>().SetBool("murchando", false);
            SceneManager.LoadScene(4);
        }
    }

    void VisualNormal()
    {
        GetComponent<DanoPlayerPisca>().QuebraRepeticao();
        renderer.color = new Color(1, 1, 1, 1);
    }
    public void RealcaIconePa()
    {
        cantil.color = new Color(1, 1, 1, 1);
        aguaCantil.color = new Color(1, 1, 1, 1);
        flor.color = new Color(1, 1, 1, 1);
    }
    public void MudaIconePa()
    {
        iconePa.GetComponent<Image>().enabled = false;
        Destroy(iconeBroto.GetComponent<Image>());
        cantil.enabled = true;
        aguaCantil.enabled = true;
        flor.enabled = true;
        contador.enabled = false;
    }
    void NoParar()
    {
        parar = false;
    }
    public void AddFlor()
    {
        if (contBaldada == 0)
            Destroy(flor.GetComponent<Animator>());
        flor.GetComponent<Image>().sprite = florUI[contBaldada];
    }

    public void Coletar()
    {
        coletar = true;
    }
}

