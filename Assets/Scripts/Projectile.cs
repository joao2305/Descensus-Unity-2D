using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VidaDoJogador>().receberDano(transform.position, damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Map") && other.CompareTag("Boss"))
        {
            Destroy(gameObject); 
        }
    }
}