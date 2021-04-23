using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    public float Velocidad = 10;
    public float FuerzaSalto = 25;
    public bool dobleSalto;
    public Transform GroundCheck;
    public float checkRadius = 0.1f;
    public LayerMask queEsSuelo;
    public bool onLadder = false;
    
    public BoxCollider2D plataforma;
    private bool deliza = false;
    public bool grounded;
    public int puntaje = 0;
    public int vida = 3;
    public Text puntajeTexto;
    public Text vidaTexto;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
       


    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, queEsSuelo);
        puntajeTexto.text = "Puntaje: " + puntaje;
        vidaTexto.text = "Vidas: " + vida;
    }

    // Update is called once per frame
    void Update()
    {
        if(grounded)
            dobleSalto = false;

        if (vida == 0)
        {
            //rb.velocity = Vector2.zero;
            animator.SetBool("EstaMuerto", true);
            GetComponent<PlayerController>().enabled = false;
        }

        PlayerCorrer();
        PlayerSaltar();
        
        PlayerDeslizarse();
       
     

        

    }

    

    public void PlayerCorrer()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(Velocidad, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(Velocidad * -1, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("VelocidadX", Mathf.Abs(rb.velocity.x));
    }

    public void PlayerSaltar()
    {

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);

        }

        if (Input.GetKeyDown(KeyCode.Space) && !dobleSalto && !grounded)
        {

            rb.velocity = new Vector2(rb.velocity.x, FuerzaSalto);
            dobleSalto = true;
        }

        if (!onLadder)
            animator.SetFloat("VelocidadY", Mathf.Abs(rb.velocity.y));
    }


    public void PlayerDeslizarse()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            deliza = true;
            animator.SetBool("seDesliza", deliza);
        }
        else
        {
            deliza = false;
            animator.SetBool("seDesliza", deliza);
        }
    }




}
