using UnityEngine;

public class InimigoLookATPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 escala = transform.localScale;

        if (player.position.x < transform.position.x)
            escala.x = -Mathf.Abs(escala.x);
        else
            escala.x = Mathf.Abs(escala.x);

        transform.localScale = escala;
    }
}
