using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool shoot;
    private Animator gunAnimator;
    private AudioSource playerAudio;
    public AudioClip gun;
    public int point = 0;
    public int playerMaxHealth = 10, playerCurHealth = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot = false;
        gunAnimator = GameObject.Find("Gun").GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerCurHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerAudio.PlayOneShot(gun);
            Fire();
            shoot = true;
        }else if(Input.GetMouseButtonUp(0))
        {
            shoot = false;
        }
        gunAnimator.SetBool("Shoot", shoot);
    }

    void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("Zombie"))
            {
                hit.transform.SendMessage("Damge",10);
                point ++;
                
            }
        }
    }

    public void Damge()
    {
        StartCoroutine(DelayDamge());
    }

    IEnumerator DelayDamge()
    {
        yield return new WaitForSeconds(3);
        playerCurHealth --;
    }

}
