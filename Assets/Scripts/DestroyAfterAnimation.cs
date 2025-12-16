using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Opcional: se quiser garantir que o objeto será destruído mesmo sem animação
        if (anim == null)
        {
            Debug.LogWarning("Nenhum Animator encontrado! O objeto será destruído após 1 segundo.");
            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        if (anim != null)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}