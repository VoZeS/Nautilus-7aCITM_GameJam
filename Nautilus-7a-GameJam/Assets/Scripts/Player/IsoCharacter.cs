using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsoCharacter : MonoBehaviour
{
    public List<Transform> monedasRecogidas = new List<Transform>();
    public float coinCount = 0;

    [Header("Character")]
    public int characterScale = 5;
    public float velocidadMovimiento = 5f;

    [Header("Movement")]
    public GameObject parent;
    private Rigidbody2D rb;
    public int orientation; // 0 left, 1 right

    [Header("Audios")]
    public AudioSource stepOnConcrete1;
    public AudioSource stepOnConcrete2;
    public AudioSource stepOnGrass1;
    public AudioSource stepOnGrass2;
    public AudioSource stepOnGrass3;

    private bool stepping;


    private Animator playerAnimator;
    public Animator shadowAnimator;

    public float movimientoHorizontal;
    public Vector2 velocidad;
    public float tiempoQuieto = 1f;

    private float velocidadNormal; // Almacena la velocidad normal del jugador.
    private float velocidadReducida = 0f; // Velocidad reducida después de entrar en el trigger.
    [SerializeField] public float duracionVelocidadReducida; // Duración de la velocidad reducida en segundos.
    private bool velocidadReducidaActiva = false; // Bandera para verificar si la velocidad reducida está activa.

    public static bool dead;
    public static bool dying;

    void Start()
    {
        rb = parent.GetComponent<Rigidbody2D>();
        orientation = 1;

        playerAnimator = GetComponent<Animator>();

        // Inicializar la velocidad normal.
        velocidadNormal = velocidadMovimiento;
    }

    void Update()
    {
        if(!dying)
        {
            Walk(out movimientoHorizontal, out velocidad);

            UpdateOrientation(movimientoHorizontal, velocidad);
        }

        if (stepping)
        {
            Debug.Log("Pisando");
            if (SceneManager.GetActiveScene().name == "Pesadilla2" || SceneManager.GetActiveScene().name == "Pesadilla1")
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

            if (SceneManager.GetActiveScene().name == "Pesadilla3")
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

    private void Walk(out float movimientoHorizontal, out Vector2 velocidad)
    {
        // Verificar si la velocidad reducida está activa.
        if (velocidadReducidaActiva)
        {
            // Restar tiempo reducido y desactivar la velocidad reducida al alcanzar cero.
            duracionVelocidadReducida -= Time.deltaTime;
            if (duracionVelocidadReducida <= 0f)
            {
                velocidadReducidaActiva = false;
                RestaurarVelocidadNormal();
            }
        }

        playerAnimator.SetBool("Walking", true);
        shadowAnimator.SetBool("Walking", true);

        // Movimiento en un juego 2D isométrico
        movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Aplicar la velocidad al Rigidbody
        velocidad = new Vector2(movimientoHorizontal * velocidadMovimiento, movimientoVertical * velocidadMovimiento);
        rb.velocity = new Vector2(velocidad.x, velocidad.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Hit")
        {
            // Activar la velocidad reducida al entrar en el trigger.
            AplicarVelocidadReducida();
        }
    }

    void UpdateOrientation(float movimientoHorizontal, Vector2 direccion)
    {
        // Actualizar la orientación solo si hay un movimiento
        if (direccion != Vector2.zero)
        {
            // Girar el sprite
            if (movimientoHorizontal >= 0f)
                orientation = 1;
            else if (movimientoHorizontal < 0f)
                orientation = 0;
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
            shadowAnimator.SetBool("Walking", false);
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

    void AplicarVelocidadReducida()
    {
        // Activar la velocidad reducida y configurar la duración.
        velocidadReducidaActiva = true;
        duracionVelocidadReducida = 2f; // Puedes ajustar la duración según tus necesidades.
        velocidadMovimiento = velocidadReducida; // Establecer la velocidad reducida.
    }

    void RestaurarVelocidadNormal()
    {
        // Restaurar la velocidad normal.
        velocidadMovimiento = velocidadNormal;
    }

    public void Stepping()
    {
        stepping = true;
        Debug.Log("Stepping Trueeeeee");
    }

}
