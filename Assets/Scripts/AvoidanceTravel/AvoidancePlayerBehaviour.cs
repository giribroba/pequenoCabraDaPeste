using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidancePlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float speed;
    private float hor, ver;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(hor, ver) * speed;
    }
}
