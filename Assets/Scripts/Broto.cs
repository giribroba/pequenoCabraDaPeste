using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broto : MonoBehaviour
{
    [SerializeField] private GameObject poeira;
    public void Poeira(){
        poeira.GetComponent<Animator>().SetTrigger("Poeira");
    }
}
