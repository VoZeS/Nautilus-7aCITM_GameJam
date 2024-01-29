

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Follow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2cube;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;

    private int orientation; // 0 left, 1 right

    private IsoCharacter playerScript;

    public GameObject parent;
    private Animator followerAnimator;

    private Vector3 lastPosition;

    private void Start()
    {
        playerScript = player.GetComponent<IsoCharacter>();

        followerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        distance2cube = Vector2.Distance(parent.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - parent.transform.position;
        direction.Normalize();

        if (distance2cube < range && distance2cube > nearRange)
        {
            parent.transform.position = Vector2.MoveTowards(parent.transform.position, player.transform.position, speed * Time.deltaTime);
            
        }

        if(followerAnimator != null && (playerScript.velocidad.x != 0 || playerScript.velocidad.y != 0))
        {
            followerAnimator.SetBool("Walking", true);
        }
        else if(followerAnimator != null)
        {
            followerAnimator.SetBool("Walking", false);

        }

        orientation = playerScript.orientation;
        
        UpdateOrientation(orientation);

    }

    bool DetectMovement()
    {
        // Compara la posición actual con la última posición registrada
        if (parent.transform.position != lastPosition)
        {
            // El GameObject se está moviendo
            lastPosition = parent.transform.position;
            return true;
        }
        else
        {
            // El GameObject está quieto
            return false;
        }
    }

    void UpdateOrientation(int orient)
    {

        switch (orient)
        {
            case 0:
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            case 1:
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                break;
            default:
                break;
        }
    }
}
