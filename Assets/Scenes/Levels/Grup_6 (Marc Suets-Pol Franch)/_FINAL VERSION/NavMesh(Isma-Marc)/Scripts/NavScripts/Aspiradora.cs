using UnityEngine;
using UnityEngine.AI;

public class Aspiradora : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 targetPosition;
    private bool movingToTarget = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetPosition = transform.position; // Inicializar con la posición actual
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Utiliza Camera.main para referenciar automáticamente la cámara principal
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
            {
                targetPosition = hit.point;
                movingToTarget = true;
            }
        }

        if (movingToTarget)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > 0.1f) // Umbral para detener el movimiento
        {
            navMeshAgent.Move(direction * Time.deltaTime);
        }
        else
        {
            movingToTarget = false;
        }
    }
}
