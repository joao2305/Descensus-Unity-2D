using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip somDePasso;
    [SerializeField] private AudioClip somDeAtaque;
    [SerializeField] private AudioSource audioSource;

    private Rigidbody2D rb;
    public Transform GroundCheck;
    public LayerMask whatIsGround;

    public float movespeed=6f;
    private float moveX,ymove;
    private float JumpForce=8f;
    public float GroundCheckY=0.2f;
    public float GroundCheckX=0.5f;
    public float damage;

    private Animator animator;
    private SpriteRenderer sprinteRenderer;

    private bool attacking=false;
    private float timeBetweenAttack=0.5f, timeSinceAttack=0;
    public Transform SideAttackTransform,UpAttackTransform,DownAttackTransform;
    public Vector2 SideAttackArea,UpAttackArea,DownAttackArea;
    public LayerMask AttackLayer;
    public GameObject slashEffect;

    [HideInInspector] public static int moeda=0;
    [SerializeField] private TextMeshProUGUI textoMoedas;


    private static int puloExtra;
    private static bool podeDash = true;
    private static bool estaDashando = false;

    public float dashVelocidade = 15f;
    public float dashDuracao = 0.2f;
    public float dashCooldown = 1f;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprinteRenderer=GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        if (!estaDashando) 
        {
            Move();
        }
        Jump();
        UpdateAnimator();
        UpdateSpriteRenderer();
        attack();
        textoMoedas.text = "Moedas: " + moeda;

        if (Grounded())
        {
            puloExtra = 1;
        }
        if (Input.GetButtonDown("Jump") && !Grounded() && HabilidadeManager.instance != null && HabilidadeManager.instance.puloDuploHabilitado && puloExtra > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            puloExtra--;
        }

        if (HabilidadeManager.instance != null && HabilidadeManager.instance.dashHabilitado && Input.GetKeyDown(KeyCode.R) && podeDash)
        {
            StartCoroutine(ExecutarDash());
        }
    }

    private System.Collections.IEnumerator ExecutarDash()
    {
        podeDash = false;
        estaDashando = true;

        float direcao = Mathf.Sign(moveX);
        if (direcao == 0) direcao = transform.localScale.x >= 0 ? 1 : -1;
        rb.linearVelocity = new Vector2(direcao * dashVelocidade, 0f);
        yield return new WaitForSeconds(dashDuracao);

        estaDashando = false;
        
        yield return new WaitForSeconds(dashCooldown);
        podeDash = true;
    }
     public bool Grounded()
     {
        if (Physics2D.Raycast(GroundCheck.position,Vector2.down,GroundCheckY,whatIsGround)
        ||Physics2D.Raycast(GroundCheck.position + new Vector3(GroundCheckX,0,0),Vector2.down,GroundCheckY,whatIsGround)
        ||Physics2D.Raycast(GroundCheck.position + new Vector3(-GroundCheckX,0,0),Vector2.down,GroundCheckY,whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
     }

     void Jump()
     {
        if (estaDashando) return; 
         if (Input.GetButtonDown("Jump")&&Grounded())
        {
            rb.linearVelocity=new Vector3(rb.linearVelocity.x, JumpForce);
            
        }
        if (Input.GetButtonUp("Jump")&&rb.linearVelocity.y>0)
        {
            rb.linearVelocity=new Vector2(rb.linearVelocity.x,0);
        }
     }
    
    void Move()
    {

        moveX=Input.GetAxisRaw("Horizontal");
        ymove=Input.GetAxisRaw("Vertical");
        rb.linearVelocity=new Vector2(movespeed*moveX, rb.linearVelocity.y);
    if (moveX != 0 && Grounded())
    {
        if (!audioSource.isPlaying || audioSource.clip != somDePasso)
        {
            audioSource.clip = somDePasso;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    else
    {
        if (audioSource.isPlaying && audioSource.clip == somDePasso)
        {
            audioSource.Stop();
        }
    }
    }

    void UpdateAnimator()
    {
        animator.SetBool("Walking",moveX!=0);
        animator.SetBool("Jumping",!Grounded());
    }

void UpdateSpriteRenderer()
{
    Vector3 scale = transform.localScale;

    if (moveX > 0)
    {
        scale.x = Mathf.Abs(scale.x); 
    }
    else if (moveX < 0)
    {
        scale.x = -Mathf.Abs(scale.x);
    }

    transform.localScale = scale;
}
    void attack(){
        timeSinceAttack+=Time.deltaTime;
        if(attacking && timeSinceAttack>=timeBetweenAttack){
            timeSinceAttack=0;
            animator.SetTrigger("attacking");
            if (somDeAtaque != null && audioSource != null)
                {
                audioSource.PlayOneShot(somDeAtaque);
                }
            if(ymove==0|| ymove<0 && Grounded() ){
                hit(UpAttackTransform,SideAttackArea);
                Instantiate(slashEffect,SideAttackTransform);
            }
            else if(ymove>0){
                hit(UpAttackTransform,UpAttackArea);
                Slash_effectAngle(slashEffect,90,UpAttackTransform);
            }
            else if (ymove<0 && !Grounded()){
                hit(DownAttackTransform,DownAttackArea);
                Slash_effectAngle(slashEffect,-90,DownAttackTransform);
            }
        }
        attacking=Input.GetMouseButtonDown(0);
    }
    private void OnDrawGizmos(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position,SideAttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position,UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position,DownAttackArea);
    }
    void hit(Transform _attackTransform, Vector2 _attackArea){
        Collider2D[] objectsToHit=Physics2D.OverlapBoxAll(_attackTransform.position,_attackArea,0,AttackLayer);
        if(objectsToHit.Length>0){
            Debug.Log("Hit");
        }
        for(int i=0;i<objectsToHit.Length;i++){
            if(objectsToHit[i].GetComponent<enimy1>()!=null)
            {
                objectsToHit[i].GetComponent<enimy1>().EnemyHit(damage);
            }
            if(objectsToHit[i].GetComponent<bossScript>()!=null)
            {
                objectsToHit[i].GetComponent<bossScript>().EnemyHit2(damage);
            }
    }
    }
    void Slash_effectAngle(GameObject _slashEffect, int _effectAnglee,Transform _attackTransform){
        _slashEffect=Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0,0,_effectAnglee);
        _slashEffect.transform.localScale=new Vector2(transform.localScale.x,transform.localScale.y);
    }
}
