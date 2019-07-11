using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactoBehaviour : MonoBehaviour
{

    private SpriteRenderer renderer;
    private EdgeCollider2D cactoCol;
    [SerializeField] private float comeco;
    void Start(){
        renderer = GetComponent<SpriteRenderer>();
        cactoCol = GetComponent<EdgeCollider2D>();
        Invoke("Comeca", comeco);
    }

    void Comeca(){
        InvokeRepeating("Desespinho", 0f, 2f);
        InvokeRepeating("Espinho", 1f, 2f);
    }
    void Espinho(){
        GetComponent<Animator>().SetBool("Espinhos", true);
        cactoCol.enabled = true;
    }

    void Desespinho(){
        GetComponent<Animator>().SetBool("Espinhos", false);
        cactoCol.enabled = false;
    }
}
