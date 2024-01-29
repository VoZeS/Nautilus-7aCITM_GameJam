

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

    private void Start()
    {
        playerScript = player.GetComponent<IsoCharacter>();
    }

    private void Update()
    {
        distance2cube = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance2cube < range && distance2cube > nearRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        orientation = playerScript.orientation;
        
        UpdateOrientation(orientation);

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
