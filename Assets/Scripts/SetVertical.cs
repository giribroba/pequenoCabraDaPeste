using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetVertical : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private enum tipo { U, D };
    [SerializeField] tipo botao;
    [SerializeField] GameObject poco;


    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.gray;
        if (botao == tipo.U)
        {
            poco.GetComponent<pocoBehaviour>().vertical = 1;
        }
        if (botao == tipo.D)
        {
            poco.GetComponent<pocoBehaviour>().vertical = -1;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().color = Color.white;
        poco.GetComponent<pocoBehaviour>().vertical = 0;
    }
}
