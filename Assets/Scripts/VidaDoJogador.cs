using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaDoJogador : MonoBehaviour
{
    [SerializeField] private AudioClip somDeDano;
    
    public int vidaMaxima=5;
    public int vidaAtual;

    [SerializeField] Image VidaOn1;
    [SerializeField] Image VidaOff1;

    [SerializeField] Image VidaOn2;
    [SerializeField] Image VidaOff2;

    [SerializeField] Image VidaOn3;
    [SerializeField] Image VidaOff3;

    [SerializeField] Image VidaOn4;
    [SerializeField] Image VidaOff4;

    [SerializeField] Image VidaOn5;
    [SerializeField] Image VidaOff5;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaAtual=vidaMaxima;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void receberDano(Vector2 origemDano, float forcaKnockback)
    {
        if (somDeDano != null){
            AudioSource.PlayClipAtPoint(somDeDano, transform.position);}
        vidaAtual-=1;
        VidaOn1.enabled = vidaAtual >= 1;
        VidaOff1.enabled = vidaAtual < 1;

        VidaOn2.enabled = vidaAtual >= 2;
        VidaOff2.enabled = vidaAtual < 2;

        VidaOn3.enabled = vidaAtual >= 3;
        VidaOff3.enabled = vidaAtual < 3;

        VidaOn4.enabled = vidaAtual >= 4;
        VidaOff4.enabled = vidaAtual < 4;

        VidaOn5.enabled = vidaAtual >= 5;
        VidaOff5.enabled = vidaAtual < 5;

        if(vidaAtual<=0)
        {
        Debug.Log("Game Over");
        SceneManager.LoadScene("game over");
        }
        Vector2 direcao = (transform.position - (Vector3)origemDano).normalized;
        StartCoroutine(AplicarKnockback(direcao, forcaKnockback));
        animator.SetTrigger("Hurt");
    }
    private IEnumerator AplicarKnockback(Vector2 direcao, float forca)
{
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        direcao = direcao.normalized;
        direcao = new Vector2(direcao.x, Mathf.Clamp(direcao.y, 0f, 0.2f));

        rb.linearVelocity = Vector2.zero; 
        rb.AddForce(direcao * forca, ForceMode2D.Impulse);

        GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(0.2f); 
        GetComponent<PlayerController>().enabled = true;
}
}
