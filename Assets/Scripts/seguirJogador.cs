using UnityEngine;

public class seguirJogador : MonoBehaviour
{
    public float velenemy;
    private bool podeSeguir = false;
    private Transform playerposition;
    [HideInInspector] public bool isStunned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerposition=GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned) return;
        if(podeSeguir && playerposition.gameObject != null){
            transform.position=Vector2.MoveTowards(transform.position,playerposition.position,velenemy*Time.deltaTime);
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
}
