using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject player;
    private GameObject lookAtTarget;
    private Rigidbody zombieRb;
    private Animator zombieAnimator;
    public bool dead;
    public int maxHealth = 10, curHealth = 0;
    private AudioSource zombieAudio;
    public AudioClip zombieDead;
    private float timeLast, timeAttack = 0.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        zombieAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        lookAtTarget = GameObject.FindGameObjectWithTag("LookTarget");
        zombieRb = GetComponent<Rigidbody>();
        zombieAnimator = GetComponent<Animator>();
        dead = false;
        curHealth = maxHealth;
        timeLast = Time.time;
        
    }

    void Update()
    {
        
        
        zombieAnimator.SetBool("Dead", dead);

        Attack();

        

        if(curHealth <= 0)
        {
            dead = true;
            Dead();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!dead)
        {
            Move();
        }
        
    }

    void Move()
    {
        if(player == null || lookAtTarget == null)
        {
            return;
        }
        transform.LookAt(lookAtTarget.transform.position);
        transform.position = Vector3.Lerp(transform.position, player.transform.position, zombieAnimator.deltaPosition.magnitude*0.3f);

    }

    void Dead()
    {
        zombieRb.AddForce(Vector3.back*7, ForceMode.Impulse);
        StartCoroutine(DelayDead());
        zombieAudio.PlayOneShot(zombieDead);
        
    }

    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(1.5f);
        
        Destroy(gameObject);
        
    }

    public void Damge(int damge)
    {
        curHealth -= damge;  
    }

    public void Attack()
    {
        float distance = Vector3.Distance(lookAtTarget.transform.position, transform.position);
        zombieAnimator.SetFloat("Distance", distance);
        if(distance<=14)
        {
            if(Time.time >= timeLast+timeAttack)
            {
                player.GetComponent<PlayerController>().Damge();
                timeLast = Time.time;
            }
            
        }
    }

    
}
