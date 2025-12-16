using UnityEngine;
using System.Collections;
public class enimy1 : MonoBehaviour
{
    [SerializeField] float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0)
        {
            Destroy(gameObject);
            PlayerController.moeda+=1;
        }
    }

    public void EnemyHit(float _damageDone)
    {
        health -=_damageDone;
        StartCoroutine(StunTemporario(0.5f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<VidaDoJogador>().receberDano(transform.position, 15f);
        }   
    }
    private IEnumerator StunTemporario(float duracao)
    {
    GetComponent<seguirJogador>().isStunned = true;
    yield return new WaitForSeconds(duracao);
    GetComponent<seguirJogador>().isStunned = false;
    }
}
