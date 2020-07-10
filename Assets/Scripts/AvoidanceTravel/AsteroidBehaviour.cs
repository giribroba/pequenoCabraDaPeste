using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]private float speed;
    private Rigidbody2D rb;
    private GameObject pl;
    private Vector3 direc;
    static public float maxDis = 15;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pl = GameObject.FindWithTag("Player");  
        direc = new Vector3(pl.transform.position.x - transform.position.x + Random.Range(-4, 4), pl.transform.position.y - transform.position.y + Random.Range(-4, 4));
        //transform.up = direc; 
        rb.velocity = direc * speed;
    }

    // Update is called once per frame
    void Update()
    {        
        // transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, pl.transform.position) > maxDis)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag == "Planeta")
            Destroy(this.gameObject);
    }
}
