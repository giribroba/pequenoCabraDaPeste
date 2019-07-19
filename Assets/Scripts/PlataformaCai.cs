using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCai : MonoBehaviour
{
    [SerializeField] private bool caido;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Player" && !caido)
        {
            StartCoroutine(Cair());
        }
    }

    IEnumerator Cair()
    {
        caido = true;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Tremendo", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Tremendo", false);
        anim.SetBool("Caindo", true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Caindo", false);
        anim.SetBool("Subindo", true);
    }
    private void Subiu()
    {
        anim.SetBool("Subindo", false);
        caido = false;
    }
}
