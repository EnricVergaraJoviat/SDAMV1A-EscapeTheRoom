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
        targetPosition = transform.position;
        navMeshAgent.enabled = false; // Desactivar el NavMeshAgent al inicio
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
            {
                targetPosition = hit.point;
                movingToTarget = true;

                // Activar el NavMeshAgent cuando el jugador hace clic
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(targetPosition);
            }
        }

        if (movingToTarget && navMeshAgent.enabled)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
            {
                movingToTarget = false;

                // Desactivar el NavMeshAgent cuando el jugador ha alcanzado la posición deseada
                navMeshAgent.enabled = false;
            }
        }
    }
}