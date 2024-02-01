using UnityEngine;


public class PlatfromCharacter : MonoBehaviour
{
    public LayerMask sueloLayer; // Capa del suelo para verificar si el jugador está en el suelo
    public LayerMask fangoLayer;

    [Header("Character")]
    public int characterScale = 5;
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;
    static public bool dead;
    private bool dying;

    [Header("Movement")]
    public GameObject parent;
    private Rigidbody2D rb;
    public bool enElSuelo;
    public bool canMove;
    public bool jumping;

    [Header("Movement Audios")]
    public AudioSource stepOnGrass1;
    public AudioSource stepOnGrass2;
    public AudioSource stepOnGrass3;
    public AudioSource stepOnConcrete1;
    public AudioSource stepOnConcrete2;
    public AudioSource stepOnWood1;
    public AudioSource stepOnWood2;
    public AudioSource jump;

    private int orientation; // 0 left, 1 right

    private Animator playerAnimator;

    [Header("Raycast")]
    public float longitudRaycast = 1f;
    public float longitudLateralesRaycast = 0.2f;
    public float longitudLateralDownRaycast = 2f;

    private bool stepping;

    void Start()
    {
        rb = parent.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        dead = false;
        dying = false;
        stepping = false;
    }

    void Update()
    {
        if(!dying)
        {
            // Verificar si el jugador está en el suelo
            //enElSuelo = Physics2D.OverlapCircle(transform.position, 0.5f, sueloLayer);
            //Debug.Log("En el suelo: " + enElSuelo);

            RaycastHit2D raycast1, raycast2, raycast3, 
                raycastLeft1, raycastLeft2, raycastLeft3, raycastLeft4,
                raycastRight1, raycastRight2, raycastRight3, raycastRight4;

            // Lanzar el raycast hacia abajo
            raycast1 = Physics2D.Raycast(parent.transform.position, Vector2.down, longitudRaycast, sueloLayer);
            raycast2 = Physics2D.Raycast(parent.transform.position + new Vector3(0.25f,0,0), Vector2.down, longitudRaycast, sueloLayer);
            raycast3 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.25f, 0,0), Vector2.down, longitudRaycast, sueloLayer);

            raycastLeft1 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0, 0), Vector2.left, longitudLateralesRaycast, sueloLayer);
            raycastLeft2 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, -0.5f, 0), Vector2.left, longitudLateralesRaycast, sueloLayer);
            raycastLeft3 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.left, longitudLateralesRaycast, sueloLayer);
            raycastLeft4 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.down, longitudLateralDownRaycast, sueloLayer);

            raycastRight1 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0, 0), Vector2.right, longitudLateralesRaycast, sueloLayer);
            raycastRight2 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right, longitudLateralesRaycast, sueloLayer);
            raycastRight3 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.right, longitudLateralesRaycast, sueloLayer);
            raycastRight4 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.down, longitudLateralDownRaycast, sueloLayer);

            RaycastHit2D raycastFango, raycastFango2, raycastFango3;

            raycastFango = Physics2D.Raycast(parent.transform.position, Vector2.down, longitudRaycast, fangoLayer);
            raycastFango2 = Physics2D.Raycast(parent.transform.position + new Vector3(0.25f, 0, 0), Vector2.down, longitudRaycast, fangoLayer);
            raycastFango3 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.25f, 0, 0), Vector2.down, longitudRaycast, fangoLayer);

            

            if (raycast1 || raycast2 || raycast3)
            {
                jumping = false;
                enElSuelo = true;
                canMove = true;

                
            }
            else if(raycastFango || raycastFango2 || raycastFango3)
            {
                jumping = false;
                enElSuelo = true;
                canMove = false;
            }
            else
                enElSuelo = false;

            if(canMove)
            {
                // Movimiento horizontal
                float movimientoHorizontal = Input.GetAxis("Horizontal");
                Vector2 velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.velocity.y);
                rb.velocity = velocidad;

                UpdateOrientation(movimientoHorizontal, velocidad);

            }

            // Saltar
            if (enElSuelo && Input.GetKey(KeyCode.Space))
            {
                jump.Play();
                rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
                playerAnimator.SetBool("Walking", false);
                jumping = true;
            }

            if ((!raycastLeft1 && !raycastLeft2 && !raycastLeft3 && !raycastLeft4)
                && (!raycastRight1 && !raycastRight2 && !raycastRight3 && !raycastRight4)
                && jumping)
            {
                canMove = true;
            }
            else
                canMove = false;
        }
        Debug.DrawRay(parent.transform.position, Vector2.down * longitudRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(0.25f, 0, 0), Vector2.down * longitudRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(-0.25f, 0, 0), Vector2.down * longitudRaycast, Color.red);

        Debug.DrawRay(parent.transform.position + new Vector3(-0.5f, 0, 0), Vector2.left * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(-0.5f, -0.5f, 0), Vector2.left * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.left * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.down * longitudLateralDownRaycast, Color.red);

        Debug.DrawRay(parent.transform.position + new Vector3(0.5f, 0, 0), Vector2.right * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.right * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right * longitudLateralesRaycast, Color.red);
        Debug.DrawRay(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.down * longitudLateralDownRaycast, Color.red);


    }

    public void Death()
    {
        dead = true;
    }
    public void Dying()
    {
        dying = true;
    }

    public void Stepping()
    {
        stepping = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cesped")
        {
            int indiceAleatorio = Random.Range(0, 2);

            switch (indiceAleatorio)
            {
                case 0:
                    stepOnGrass1.Play();
                    break;

                case 1:
                    stepOnGrass2.Play();
                    break;

                case 2:
                    stepOnGrass3.Play();
                    break;
                default:
                    break;

            }

        }

        if (collision.gameObject.tag == "Madera")
        {
            int indiceAleatorio = Random.Range(0, 1);

            switch (indiceAleatorio)
            {
                case 0:
                    stepOnWood1.Play();
                    break;

                case 1:
                    stepOnWood2.Play();
                    break;

                default:
                    break;

            }
        }

        if (collision.gameObject.tag =="Roca")
        {
            int indiceAleatorio = Random.Range(0, 1);

            switch (indiceAleatorio)
            {
                case 0:
                    stepOnConcrete1.Play();
                    break;

                case 1:
                    stepOnConcrete2.Play();
                    break;

                default:
                    break;

            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enredadera")
        { 
            velocidadMovimiento = 1f;
            fuerzaSalto = 1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enredadera")
        {
            velocidadMovimiento = 5f;
            fuerzaSalto = 5f;
        }
    }
    void UpdateOrientation(float movimientoHorizontal, Vector2 velocidad)
    {
        // Actualizar la orientación solo si hay un movimiento
        if (velocidad != Vector2.zero)
        {
            if(enElSuelo)
                playerAnimator.SetBool("Walking", true);

            //Girar el sprite
            if (movimientoHorizontal >= 0f)
                orientation = 1;
            else if (movimientoHorizontal < 0f)
                orientation = 0;
        }
        else
        {
            playerAnimator.SetBool("Walking", false);

        }

        switch (orientation)
        {
            case 0:
                transform.localScale = new Vector3(-characterScale, characterScale, characterScale);
                break;
            case 1:
                transform.localScale = new Vector3(characterScale, characterScale, characterScale);
                break;
            default:
                transform.localScale = new Vector3(characterScale, characterScale, characterScale);
                break;
        }
    }
}