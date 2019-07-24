using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscreveTexto : MonoBehaviour
{
    private float intervalo = 0.1f;
    private string[] textoCompleto;
    private string textoAtual = "";
    [SerializeField] private int texto;
    void Start(){
      textoCompleto = new string[]  { "Bem na esquina do sertão\ndesse mundo que é só meu\nalgo vem a me arretar\né uma semente que só eu\nteria toda coragem\ne até mesmo a vontade \nde ir tirar com os braços meus\n" ,"mas eu preciso de uma força\nalgo para me ajudar\nnão estou tão forte assim\nquero algo pra cavucar\nvá correndo alí do lado\npra pegar aquela pá\n"};
        StartCoroutine("ApareceTexto");
    }
    
    IEnumerator ApareceTexto(){
        yield return new WaitForSeconds((texto == 1) ? 10f : 0);
        for (int i = 0; i < textoCompleto[texto].Length ; i++){
            textoAtual = textoCompleto[texto].Substring(0, i);
            this.GetComponent<Text>().text = textoAtual;
            yield return new WaitForSeconds(intervalo);
        }
    }
}
