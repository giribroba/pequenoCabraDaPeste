using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broto : MonoBehaviour
{
    [SerializeField] private GameObject poeira;

    private void Start()
    {
        if (Objetivo.obj != "Broto" && Objetivo.obj != "Pá")
        {
            this.gameObject.tag = "Removido";
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void Poeira(){
        poeira.GetComponent<Animator>().SetTrigger("Poeira");
    }
}
