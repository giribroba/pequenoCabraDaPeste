using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorJogador : MonoBehaviour
{
    [SerializeField] private float velocidadeMaxima, forcaPulo;
    [SerializeField] private GameObject planeta, vida, iconePa, iconeBroto;
    [SerializeField] private Sprite terra, imgVulcao;
    [SerializeField] private SpriteRenderer balaoBaoba;
    [SerializeField] private Text contador;
    [HideInInspector]public float velocidade;
    public bool balde = false, imortal = false, podePa = false;
    private bool noChao, iconeUmaVez = true;
    private float movimento, KB;    
    public int contBroto = 0, contVulcao;
    private string balaoNoSim = "Broto";
    private Animator animator;
    private RaycastHit2D[] hit;
    private SpriteRenderer renderer; 
    private Rigidbody2D rbPlayer;
    private Collider2D colPlayer;
    public GameObject tocaSons;

    void Start(){
        tocaSons = GameObject.FindWithTag("Sound");
        podePa = false;
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    void Update(){
        movimento = Input.GetAxisRaw("Horizontal");
        Jump();
        Raycasts();
    }

    void Raycasts(){
        hit = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.8f), -transform.up, 0.7f);     
        noChao = Physics2D.OverlapCircle(transform.position - new Vector3(0,0.84f), 0.7f, LayerMask.GetMask("Chao"));
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null){
                if (hit[i].collider.gameObject.tag == "Broto"){
                    var other = hit[i].collider.gameObject;
                    if (other.tag == "Broto" && Input.GetButtonDown("Fire1") && podePa && !(contBroto >= 6))
                    {
                        tocaSons.GetComponent<Sons>().PlaySound("broto");
                        contBroto++;
                        contador.text = (contBroto.ToString() + "/6");
                        animator.SetTrigger("pasada");
                        other.GetComponent<SpriteRenderer>().sprite = terra;
                        Destroy(other.GetComponent<BoxCollider2D>());
                        iconeBroto.GetComponent<IconesBehaviour>().QuebraRepeticao();
                        iconeBroto.GetComponent<Image>().enabled = true;
                        iconeBroto.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
                    else if (other.tag == "Broto" && Input.GetKeyDown(KeyCode.E) && !podePa && iconeUmaVez){
                        iconeUmaVez = false;
                        iconePa.GetComponent<IconesBehaviour>().Comeca();
                    }
                }
            }
            if (contBroto == 6){
                contador.text = (contVulcao.ToString() + "/5");
                iconeBroto.GetComponent<Image>().sprite = imgVulcao;
                iconeBroto.GetComponent<IconesBehaviour>().Comeca();
                contBroto++;
            }
        }
        
    }
    public void VulcaoDespisca(){
        iconeBroto.GetComponent<IconesBehaviour>().QuebraRepeticao();
        iconeBroto.GetComponent<IconesBehaviour>().Pisca();
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        planeta.transform.eulerAngles = new Vector3(planeta.transform.eulerAngles.x, planeta.transform.eulerAngles.y, planeta.transform.eulerAngles.z + velocidade + KB);
        animator.SetBool("walk", velocidade != 0);
        if (Mathf.Abs(KB) < 0.1f)
        {
            KB = 0;
        }
        else if (KB > 0)
        {
            KB -= Time.deltaTime * 1.5f;
        }
        else if (KB < 0)
        {
            KB += Time.deltaTime * 1.5f;
        }
        if (movimento > 0){
            velocidade = (velocidade < velocidadeMaxima)? velocidade += Time.deltaTime * 1.5f : velocidade = velocidadeMaxima;
            renderer.flipX = false;
        }
        else if(movimento < 0){
            velocidade = (velocidade > -velocidadeMaxima)? velocidade -= Time.deltaTime * 1.5f : velocidade = -velocidadeMaxima;
            renderer.flipX = true;
        }
        else{
            if(Mathf.Abs(velocidade) < 0.1f){
                velocidade = 0;
            }
            else if(velocidade > 0){
                velocidade -= Time.deltaTime * 1.5f;
            }
            else if(velocidade < 0){
                velocidade += Time.deltaTime * 1.5f;
            }
        }
    }

    void Jump(){
        animator.SetBool("jump", !noChao);
        if (noChao && Input.GetButtonDown("Jump")){
            tocaSons.GetComponent<Sons>().PlaySound("pulo");
            rbPlayer.velocity = new Vector2(0, forcaPulo);
        }
    }

    void OnCollisionStay2D(Collision2D collision){
    if(collision.gameObject.tag == "Cacto" && !imortal){
            tocaSons.GetComponent<Sons>().PlaySound("hit");
            KB = (transform.position.x < collision.gameObject.transform.position.x)? -0.5f : 0.5f;
            vida.GetComponent<Life>().DecrementLife();
            GetComponent<DanoPlayerPisca>().Comeca();
            Invoke("VisualNormal", 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Pá"){
            tocaSons.GetComponent<Sons>().PlaySound("pá");
            podePa = true;
            Destroy(iconePa.GetComponent<IconesBehaviour>());
            iconePa.GetComponent<Image>().enabled = true;
            iconePa.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            iconeBroto.GetComponent<IconesBehaviour>().Comeca();
            Destroy(other.gameObject);
        }
        else if(other.tag == balaoNoSim){
            balaoBaoba.enabled = true;
            Invoke("NoBalao", 15f);
        }
    }

    void NoBalao(){
        balaoNoSim = "";
        balaoBaoba.enabled = false;
    }

    void VisualNormal(){
        GetComponent<DanoPlayerPisca>().QuebraRepeticao();
        renderer.color = new Color(1, 1, 1, 1);
    }
}

