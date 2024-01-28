using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] public float velocidadMovimiento = 5f; // Velocidad de movimiento del jugador
    [SerializeField] public float fuerzaSalto = 10f; // Fuerza aplicada al saltar
    public LayerMask sueloLayer; // Capa del suelo para verificar si el jugador está en el suelo

    private Rigidbody2D rb;
    private bool enElSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        enElSuelo = Physics2D.OverlapCircle(transform.position, 1f, sueloLayer);
        Debug.Log("En el suelo: " + enElSuelo);

        // Movimiento horizontal
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        Vector2 velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.velocity.y);
        rb.velocity = velocidad;

        //Girar el spite
        if(movimientoHorizontal >0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (movimientoHorizontal < 0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        // Saltar
        if (enElSuelo && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }
    }
}