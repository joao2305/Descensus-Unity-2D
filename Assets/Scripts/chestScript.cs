using UnityEngine;

public class chestScript : MonoBehaviour
{
    private bool jogadorPerto = false;
    private bool chest = true;
    private Animator animator;
void Start()
{
    animator = GetComponent<Animator>();
}
    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E) && chest)
        {
            chest = false;
            animator.SetBool("open",true);
            PlayerController.moeda+=3;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }
}