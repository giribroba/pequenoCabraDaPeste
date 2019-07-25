using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscreveTexto : MonoBehaviour
{
    private float intervalo = 0.08f;
    private string textoCompleto = "Bem na esquina do sertão\ndesse mundo que é só meu\nalgo vem a me arretar\né uma semente que só eu\nteria toda coragem\ne até mesmo a vontade \nde ir tirar com os braços meus\n\n\nmas eu preciso de uma força\nalgo para me ajudar\nnão estou tão forte assim\nquero algo pra cavucar\nvá correndo alí do lado\npra pegar aquela pá\n"; 
    private string textoAtual = "";
    
    public IEnumerator ApareceTexto(){
        for (int i = 0; i < textoCompleto.Length ; i++){
            textoAtual = textoCompleto.Substring(0, i);
            this.GetComponent<Text>().text = textoAtual;
            yield return new WaitForSeconds(intervalo);
        }
        transform.parent.gameObject.GetComponent<FolhetoBehaviour>().DesparecerFolheto();
    }
}
