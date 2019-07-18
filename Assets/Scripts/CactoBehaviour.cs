using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactoBehaviour : MonoBehaviour
{

    private SpriteRenderer renderer;
    private GameObject player;
    private EdgeCollider2D cactoCol;
    [SerializeField] private float comeco;
    public AudioSource[] somEspinho;
    void Start(){
        player = GameObject.FindWithTag("Player");
        renderer = GetComponent<SpriteRenderer>();
        cactoCol = GetComponent<EdgeCollider2D>();
        Invoke("Comeca", comeco);
    }

    void Update()
    {
        //GetComponent<EdgeCollider2D>().enabled = !(player.GetComponent<controladorJogador>().imortal);
    }

    void Comeca(){
        InvokeRepeating("Desespinho", 0f, 2f);
        InvokeRepeating("Espinho", 1f, 2f);
    }
    void Espinho(){
        somEspinho[1].Play();
        GetComponent<Animator>().SetBool("Espinhos", true);
        cactoCol.enabled = true;
    }

    void Desespinho(){
        somEspinho[0].Play();
        GetComponent<Animator>().SetBool("Espinhos", false);
        cactoCol.enabled = false;
    }
}
