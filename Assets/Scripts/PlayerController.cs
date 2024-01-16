using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;

    float h, v; 

    public Transform hitPoint;

    public float hitBoxRadius = 0.5f;

    public LayerMask ballLayer;

    public float hitpointOffset;


    [HideInInspector] public bool attacking;

    AudioSource asrce;
    Animator anim;
    CharacterController chrcon;
    GameManager gm;

    public AudioClip hitBallSFX;
    public AudioClip whooshSFX;
    public AudioClip playerDamageSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        chrcon = GetComponent<CharacterController>();
        gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
        asrce = GetComponent<AudioSource>();
        PauseMenu.paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        chrcon.Move (Vector3.forward * v + Vector3.right * h);

        Raquet();

       if (!gm.endLevel && !gm.lostLevel )
        {
            if (!PauseMenu.paused)
            {
                if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                }
            }
        }

    }

    void Shoot()
    {
        anim.SetTrigger("Attack");
        asrce.PlayOneShot(whooshSFX);
        attacking = true;

    }

    void Raquet()
    {
        if (attacking)
        {
            Collider[] Targets = Physics.OverlapSphere(hitPoint.position + new Vector3 (0 , hitpointOffset , 0), hitBoxRadius, ballLayer);

            foreach (Collider ball in Targets)
            {
                if (!ball.GetComponent<BallController>().hitByPlayer)
                {
                    ball.GetComponent<BallController>().hitByPlayer = true;
                    ball.GetComponent<BallController>().rebound = false;
                    ball.GetComponent<BallController>().rebound_2 = false;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Debug.Log(hit.transform.name);
                        gm.target.position = hit.point;

                    }
                    asrce.PlayOneShot(hitBallSFX);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(hitPoint.position + new Vector3(0, hitpointOffset, 0), hitBoxRadius);
    }

    public void stopAttacking()
    {
        attacking = false;
    }

    public void DamagePlayer()
    {
        
        if (!asrce.isPlaying)
        {
            asrce.PlayOneShot(playerDamageSound);
            anim.SetTrigger("Death");
            Time.timeScale = 0.1f;
            StartCoroutine(RestartPlayer());
        }

        
    }


    IEnumerator RestartPlayer ()
    {
        yield return new WaitForSeconds(0.125f);
        //transform.position = initialPos;
        gm.LostBall();
        Time.timeScale = 1;
        StopAllCoroutines();
    }

}
