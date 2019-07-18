using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float camVel, limiteMin, limiteMax, zoomMax, zoomMin;
    [SerializeField] private bool segue;

    void Update(){
        var novaCamPos = Vector3.Lerp(transform.position, player.transform.position, camVel);
        var posZ = Vector3.Lerp(transform.position, player.transform.position, 0.0075f);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(novaCamPos.y, limiteMin, limiteMax), Mathf.Clamp(posZ.z, zoomMin, zoomMax)); 
    }

    private void Kkk(){
        
    }
}
