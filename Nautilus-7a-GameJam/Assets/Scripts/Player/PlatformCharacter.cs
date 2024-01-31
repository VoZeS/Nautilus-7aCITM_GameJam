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

    private int orientation; // 0 left, 1 right

    private Animator playerAnimator;

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
            enElSuelo = Physics2D.OverlapCircle(transform.position, 0.5f, sueloLayer);
            //Debug.Log("En el suelo: " + enElSuelo);

            // Movimiento horizontal
            float movimientoHorizontal = Input.GetAxis("Horizontal");
            Vector2 velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.velocity.y);
            rb.velocity = velocidad;

            UpdateOrientation(movimientoHorizontal, velocidad);

            // Saltar
            if (enElSuelo && Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
                playerAnimator.SetBool("Walking", false);
            }
        }
        
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