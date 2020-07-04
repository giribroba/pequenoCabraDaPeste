using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawn : MonoBehaviour
{
    private GameObject player;
    public GameObject asteroid;
    private float x, y;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("Spawn", 0f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        x = Random.Range(-14, 14);
        y = (x <= -9.7f || x >= 9.7f) ? Random.Range(-9, 9) : (Random.Range(6, 9) * ((Random.Range(0, 100) > 50) ? 1 : -1));
        pos = new Vector2(x + player.transform.position.x, y + player.transform.position.y);
        Instantiate(asteroid, pos, Quaternion.identity);
    }
}
