using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float raycastDistance = .2f;
    public float traceDistance = 2f;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.magnitude > traceDistance)
            return;


        Vector2 directionNormalized = direction.normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized, raycastDistance);
        Debug.DrawRay(transform.position, direction.normalized * raycastDistance, Color.red);



        foreach (RaycastHit2D rhit in hits)
        {
            if (rhit.collider != null && rhit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
;
        }



}

}
