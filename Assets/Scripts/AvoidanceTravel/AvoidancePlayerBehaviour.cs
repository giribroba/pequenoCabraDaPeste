using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvoidancePlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float speed;
    [SerializeField]private GameObject vida, arrow, spawner;
    [SerializeField]private GameObject[] cutscenes;
    private ArrowBehaviour ab;
    private int planet;
    public float hor, ver;
    public bool imortal;
    public FixedJoystick joy;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Planet", 0);
        if(AvoidanceController.instance.arcade)
            PlayerPrefs.SetInt("Planet", 0);
        planet = PlayerPrefs.GetInt("Planet", 0);
        rb = GetComponent<Rigidbody2D>();
        ab = arrow.GetComponent<ArrowBehaviour>();

        if(planet > 0)
        {
            ab.i = planet + 1;
            transform.position = new Vector3(-4, 0) + ab.planet[planet].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(ab.i);

        // hor = Input.GetAxisRaw("Horizontal");
        // ver = Input.GetAxisRaw("Vertical");
        hor = joy.Horizontal;
        ver = joy.Vertical;
        
        rb.velocity = new Vector2(hor, ver) * speed;        

        if(ab.i < 5 && Vector3.Distance(transform.position, ab.planet[ab.i].transform.position) <= 10)
        {
            spawner.GetComponent<AsteroidsSpawn>().spawn = false; 
            vida.GetComponent<Life>().ResetLife();
            cutscenes[ab.i].SetActive(true);
            StartCoroutine("CloseCs");
            AsteroidBehaviour.maxDis = 0;       
            PlayerPrefs.SetInt("Planet", ab.i);     
            ab.i++;            
        }
        else if(ab.i == 5)
            PlayerPrefs.SetInt("Planet", 0);    
    }

    public IEnumerator CloseCs()
    {
        yield return new WaitForSeconds(1f);
        cutscenes[ab.i - 1].SetActive(false);      
        spawner.GetComponent<AsteroidsSpawn>().spawn = true;   
        AsteroidBehaviour.maxDis = 15;
        print("a");
        if(ab.i == 5 && AvoidanceController.instance.arcade)        
            SceneManager.LoadScene("MenuAlfa");        
        else if(ab.i == 5 && !AvoidanceController.instance.arcade)
            SceneManager.LoadScene("Runner");                
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.tag == "Asteroide" && !imortal)
        {
            vida.GetComponent<Life>().DecrementLife();
            GetComponent<DanoPlayerPisca>().Comeca();
            Invoke("VisualNormal", 1f);
        }
    }

    void VisualNormal()
    {
        GetComponent<DanoPlayerPisca>().QuebraRepeticao();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
