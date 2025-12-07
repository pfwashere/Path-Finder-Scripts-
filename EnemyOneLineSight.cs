using System.Collections;
using UnityEngine;


public class EnemyOneLineSight : MonoBehaviour
{
    public Transform player;         
    public float followSpeed = 5f;   
    public float raycastDistance = 10f; 
    public LayerMask layerMask;      
    public LineRenderer lineRenderer;

    private bool SeeP = false;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
 
        lineRenderer.startWidth = 0.1f;      
        lineRenderer.endWidth = 0.1f;       
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, layerMask))
        {
            lineRenderer.SetPosition(0, transform.position); 
            lineRenderer.SetPosition(1, hit.point);          

            if (hit.collider.CompareTag("Player"))          
            {
                SeeP = true;
            }
            else
            {
                SeeP = false;
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * raycastDistance);

            SeeP = false;
        }

        if (SeeP)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        transform.position += directionToPlayer * followSpeed * Time.deltaTime;

        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * followSpeed);
    }
}
