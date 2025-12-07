using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;              
    public float followSpeed = 5f;        
    public float raycastDistance = 3f;    
    public float fieldOfView = 120f;      
    public int rayCount = 120;            
    public LayerMask layerMask;           

    [Header("Line Customization")]
    public Color lineStartColor = Color.red; 
    public Color lineEndColor = Color.red;     
    public float lineWidth = 0.1f;            

    private bool canSeePlayer = false;         
    private LineRenderer[] lineRenderers;     

    void Start()
    {      
        lineRenderers = new LineRenderer[rayCount];
        for (int i = 0; i < rayCount; i++)
        {
            GameObject lineObj = new GameObject($"LineRenderer_{i}");
            lineObj.transform.parent = transform;
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = lineStartColor;
            lr.endColor = lineEndColor;
            lineRenderers[i] = lr;
        }
    }

    void FixedUpdate()
    {
        canSeePlayer = false;
        float halfFOV = fieldOfView / 2f;
        float angleStep = fieldOfView / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = -halfFOV + (i * angleStep);
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, rayDirection, out hit, raycastDistance, layerMask))
            {            
                lineRenderers[i].SetPosition(0, transform.position);
                lineRenderers[i].SetPosition(1, hit.point);
              
                if (hit.collider.CompareTag("Player"))
                {
                    canSeePlayer = true;
                    Debug.Log("Player spotted!");
                }
            }
            else
            {               
                lineRenderers[i].SetPosition(0, transform.position);
                lineRenderers[i].SetPosition(1, transform.position + rayDirection * raycastDistance);
            }
        }
        
        if (canSeePlayer)
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
