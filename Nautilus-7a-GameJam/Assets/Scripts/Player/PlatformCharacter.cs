using UnityEngine;


public class PlatfromCharacter : MonoBehaviour
{
    public LayerMask sueloLayer; // Capa del suelo para verificar si el jugador está en el suelo

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

    private int orientation; // 0 left, 1 right

    private Animator playerAnimator;

    public float longitudRaycast = 1f;
    public float longitudLateralesRaycast = 0.2f;
    public float longitudLateralDownRaycast = 2f;

    void Start()
    {
        rb = parent.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        dead = false;
        dying = false;
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

            raycastLeft1 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0, 0), Vector2.left, longitudLateralesRaycast);
            raycastLeft2 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, -0.5f, 0), Vector2.left, longitudLateralesRaycast);
            raycastLeft3 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.left, longitudLateralesRaycast);
            raycastLeft4 = Physics2D.Raycast(parent.transform.position + new Vector3(-0.5f, 0.5f, 0), Vector2.down, longitudLateralDownRaycast);

            raycastRight1 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0, 0), Vector2.right, longitudLateralesRaycast);
            raycastRight2 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right, longitudLateralesRaycast);
            raycastRight3 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.right, longitudLateralesRaycast);
            raycastRight4 = Physics2D.Raycast(parent.transform.position + new Vector3(0.5f, 0.5f, 0), Vector2.down, longitudLateralDownRaycast);

            if (raycast1 || raycast2 || raycast3)
            {
                jumping = false;
                enElSuelo = true;
                canMove = true;

                
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