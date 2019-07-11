using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorJogador : MonoBehaviour
{
    [SerializeField] private Vector3 limiteMin, limiteMax;
    [SerializeField] private float velocidadeMaxima, forcaPulo;
    [SerializeField] private GameObject planeta, vida, iconePa, iconeBroto;
    [SerializeField] private Sprite terra;
    [SerializeField] private SpriteRenderer balaoBaoba;
    [SerializeField] private Text contador;
    private bool noChao, podePa = false, iconeUmaVez = true;
    private float movimento, velocidade;    
    private int contBroto = 0;
    private string balaoNoSim = "Broto";
    private Animator animator;
    private RaycastHit2D hit;
    private SpriteRenderer renderer; 
    private Rigidbody2D rbPlayer;
    private Collider2D colPlayer;

    void Start(){
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
        hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.8f), -transform.up, 0.5f);
        noChao = Physics2D.OverlapCircle(transform.position - new Vector3(0,0.84f), 0.3f, LayerMask.GetMask("Chao"));
        if (hit.collider != null){
            if (hit.collider.gameObject.tag == "Broto"){
                var other = hit.collider.gameObject;
                if (other.tag == "Broto" && Input.GetKeyDown(KeyCode.E) && podePa){
                    contBroto++;
                    contador.text = contBroto.ToString();
                    animator.SetTrigger("pasada");
                    other.GetComponent<SpriteRenderer>().sprite = terra;
                    Destroy(other.GetComponent<BoxCollider2D>());
                    Destroy(iconeBroto.GetComponent<IconesBehaviour>());
                    iconeBroto.GetComponent<Image>().enabled = true;
                    iconeBroto.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
                else if (other.tag == "Broto" && Input.GetKeyDown(KeyCode.E) && !podePa && iconeUmaVez){
                    iconeUmaVez = false;
                    iconePa.GetComponent<IconesBehaviour>().Comeca();
                }
            }
        }
        
    }

    void FixedUpdate(){
        Move();
    }

    void Move(){
        planeta.transform.eulerAngles = new Vector3(planeta.transform.eulerAngles.x, planeta.transform.eulerAngles.y, planeta.transform.eulerAngles.z + velocidade);

        if (movimento > 0){
            velocidade = (velocidade < velocidadeMaxima)? velocidade += Time.deltaTime * 1.5f : velocidade = velocidadeMaxima;
            animator.SetBool("walk", true);
            renderer.flipX = false;
        }
        else if(movimento < 0){
            velocidade = (velocidade > -velocidadeMaxima)? velocidade -= Time.deltaTime * 1.5f : velocidade = -velocidadeMaxima;
            animator.SetBool("walk", true);
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
            animator.SetBool("walk", false);
        }
    }

    void Jump(){
        animator.SetBool("jump", !noChao);
        if (noChao && Input.GetKeyDown(KeyCode.Space)){
                rbPlayer.velocity = new Vector2(0, forcaPulo);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag == "Cacto"){
            velocidade = (transform.position.x < collision.gameObject.transform.position.x)? -0.5f : 0.5f;
            vida.GetComponent<Life>().DecrementLife();
            renderer.color = new Color(200, 0, 0);
            Invoke("VisualNormal", 0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Pá"){
            podePa = true;
            Destroy(iconePa.GetComponent<IconesBehaviour>());
            iconePa.GetComponent<Image>().enabled = true;
            iconePa.GetComponent<Image>().color = new Color(255, 255, 255, 255);
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
        renderer.color = new Color(255, 255, 255, 255);
    }
}

