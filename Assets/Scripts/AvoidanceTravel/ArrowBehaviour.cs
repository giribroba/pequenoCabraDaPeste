using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private GameObject pl;
    public GameObject[] planet;
    private float x, y, mod = -1.2f;
    public float rot;
    public int i = 0;
    // Start is called before the first frame update
    private void Awake() 
    {
        planet = GameObject.FindGameObjectsWithTag("Planeta");
        planet[0].transform.position = new Vector3(-46.9f, 5.4f);
        planet[1].transform.position = new Vector3(-105.9f, 20.2f);
        planet[2].transform.position = new Vector3(-157.9f, 9.2f);
        planet[3].transform.position = new Vector3(-206.9f, 14.7f);
        planet[4].transform.position = new Vector3(-256.1f, 8.7f);
    }
    void Start()
    {
        pl = GameObject.FindWithTag("Player");
        transform.right = new Vector3(planet[i].transform.position.x - pl.transform.position.x, planet[i].transform.position.y - pl.transform.position.y);
        rot = transform.rotation.eulerAngles.z;
        // if(rot >= 140 && rot <= 215)
        //     x = -32.16159f;
        // else if(rot <= 40 || (rot <= 360 && rot >= 315))
        //     x = 32.16159f;
        transform.localPosition += new Vector3(-32.16159f, 0);
        
        //print(planet[0].name + planet[1].name + planet[2].name + planet[3].name + planet[4].name);
    }

    // Update is called once per frame
    void Update()
    {        
        if(i < 5)
            transform.right = new Vector3(planet[i].transform.position.x - pl.transform.position.x, planet[i].transform.position.y - pl.transform.position.y);
            
        rot = transform.rotation.eulerAngles.z;
        if(rot <= 360 && rot >= 350)
            mod = -360;
        x = (rot <= 140 && rot >= 40) ? ((rot - 90) * -0.14f) + pl.transform.position.x /*: () ?*/ : transform.position.x;
        y = (rot >= 140 && rot <= 215) ? ((rot - 178.75f) * -0.09f) + pl.transform.position.y : (rot <= 40 || (rot <= 360 && rot >= 315)) ? ((rot + mod) * 0.09f) + pl.transform.position.y : transform.position.y;
        transform.position = new Vector3(x, y);
        //print(transform.rotation.eulerAngles.z);
    }
}
