

using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public float speed = 5f;

    public float distance2cube;
    [SerializeField] public float range = 10f;
    [SerializeField] public float nearRange = 1.2f;


    private void Start()
    {
        
    }

    private void Update()
    {
        distance2cube = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance2cube < range && distance2cube > nearRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed*Time.deltaTime);
        }
    }
}
