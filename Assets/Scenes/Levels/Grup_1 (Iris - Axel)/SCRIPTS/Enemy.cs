
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    /*
     * Variables mostradas en el inspector para configuración
     */

    //Vida del personaje
    public float vida = 100;
    //Variación del daño, entre menor sea menor daño
    public float variation = 0.6f;
    //Variación del daño, entre menor sea menor daño
    public float altura =1;
    //Tags que no causan daño al enemigo, el primero debe ser Enemy
    public List<string> tagEnemy;
    //Velocidad de la animación de caminar
    public float speedAnimation = 1;
    //Partes del enemigo que detectan el daño
    public List<Rigidbody> partes;
    //Rango deteccion
    public float rango;
    //Rango ataque
    public float rangoAtaque;
    //Rango ataque
    public LayerMask playerLayer;
    //Barra de vida
    public Slider vidaBar;
    
    public bool mostrarArbol = false;


    /*
     * Variables para el enemigo
     */

    //Player a seguir
    public GameObject player;

    public Animator anim;
    private NavMeshAgent nav;
    Vector3 previous;


    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        previous = transform.position;
    }
    /*
     *Metodo para buscar todos  los rigidbody del enemigo y agregarles el componente de daño 
     */
    public void BuscaPartes(Transform parent) {
        foreach (Transform parte in parent)
        {
            Rigidbody rb = parte.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.gameObject.tag = tagEnemy[0];
                EnemyPart enemyPart= rb.gameObject.AddComponent<EnemyPart>();
                enemyPart.enemy = GetComponent<Enemy>();
              partes.Add(rb);
            }
            BuscaPartes(parte);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (vida <= 0) {
            if (vidaBar != null)
            {
                vidaBar.gameObject.SetActive(false);
            }
            nav.isStopped = true;
            return;
        }
        if (vidaBar != null)
        {
            vidaBar.value = vida;
        }

        Collider[] players = Physics.OverlapSphere(transform.position, rango, playerLayer);
        players = players.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).ToArray();
        if (players.Length == 0)
        {
            player = null;
        }
        else {
            player = players[0].gameObject;
        }

        if (player == null)
        {
            nav.isStopped = true;
            anim.SetFloat("Velocidad", 0);
            anim.SetFloat("VelocidadAnim",0);
            return;
        }
        else {
            nav.isStopped = false;
        }
        //Calcula la velocidad
        float vel = (transform.position - previous).magnitude / Time.deltaTime;
        previous = transform.position;

        //Hace que persiga al player
        nav.SetDestination(player.transform.position);

        //Se cambia la velocidad acorde al movimiento
        anim.SetFloat("Velocidad", vel);
        anim.SetFloat("VelocidadAnim", vel* speedAnimation);
        anim.SetBool("Atacar", (player.transform.position-transform.position).magnitude <= rangoAtaque);
    }

    /*
     * Para esta función activar el animator IK, hace que el enemigo mire al player
     */
    void OnAnimatorIK(int layerIndex)
    {
        if (player == null) {
            return;
        }
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y+altura, player.transform.position.z);
        anim.SetLookAtPosition(pos);
        anim.SetLookAtWeight(1);
    }
    /*
     * Metodo para bajar la vida
     */
    public void TakeDano(float dano,string name) {
        Debug.Log("Daño recibido en "+name+" de "+dano*variation);
        vida -= dano*variation;
        if (vida <= 0) {
            vida = 0;
            anim.enabled = false;
        }
    }
}
