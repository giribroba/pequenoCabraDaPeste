using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawn : MonoBehaviour
{
    private GameObject player, life;
    public GameObject asteroid;
    private float x, y, plver;
    private Vector3 pos;
    public bool spawn = true;
    private bool canceled = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        life = GameObject.Find("Vidas");
        InvokeRepeating("Spawn", 0f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Life.life == 0)
            spawn = false;
        if(!spawn && !canceled)
        {
            CancelInvoke();
            canceled = true;
        }
        if(spawn && canceled)
        {
            InvokeRepeating("Spawn", 0f, 0.7f);
            canceled = false;
        }        
    }
    public void Spawn()
    {
        plver = player.GetComponent<AvoidancePlayerBehaviour>().ver;
        x = Random.Range(-15, 15) /*+ (5 * player.GetComponent<AvoidancePlayerBehaviour>().hor)*/;
        y = (x <= -9.7f || x >= 9.7f) ? Random.Range(-15, 15) : (Random.Range(6, 15) * ((plver != 0) ? plver : (Random.Range(0, 100) > 50) ? 1 : -1 ));
        pos = new Vector2(x + player.transform.position.x, y + player.transform.position.y);
        Instantiate(asteroid, pos, Quaternion.identity);
    }
}
