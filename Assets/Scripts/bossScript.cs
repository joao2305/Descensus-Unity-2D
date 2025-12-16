using UnityEngine;
using System.Collections;

public class bossScript : MonoBehaviour
{
    [SerializeField] float health;
    public float velenemy;
    private bool podeSeguir = false;
    private Transform playerposition;
    [HideInInspector] public bool isStunned = false;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 2f;

    private float fireTimer = 0f;

    private Animator animator;

    [SerializeField] private GameObject fogueiraFinal;

    void Start()
    {
        playerposition=GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isStunned) return;
        if(podeSeguir && playerposition.gameObject != null)
        {
            transform.position=Vector2.MoveTowards(transform.position,playerposition.position,velenemy*Time.deltaTime);
        }
        if (health<=0)
        {
            PlayerController.moeda+=3;
            if (fogueiraFinal != null){
                fogueiraFinal.SetActive(true);}

            Destroy(gameObject);
        }
        fireTimer += Time.deltaTime;

        if (podeSeguir && !isStunned && playerposition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerposition.position, velenemy * Time.deltaTime);

            if (fireTimer >= fireCooldown)
            {
                AtirarProjetil();
                fireTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            podeSeguir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            podeSeguir = false;
        }
    }

        public void EnemyHit2(float _damageDone)
    {
        health -=_damageDone;
        StartCoroutine(StunTemporario(0.2f));
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<VidaDoJogador>().receberDano(transform.position, 15f);
        }   
    }
    private IEnumerator StunTemporario(float duracao)
    {
    isStunned = true;
    yield return new WaitForSeconds(duracao);
    isStunned = false;
    }

    private void AtirarProjetil()
    {
    if (projectilePrefab == null) return;

    if (animator != null)
        animator.SetTrigger("Atacar");

    Vector2 direcao = (playerposition.position - transform.position).normalized;

    GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    proj.GetComponent<Projectile>().SetDirection(direcao);
    }

}
