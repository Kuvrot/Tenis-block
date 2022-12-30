using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speedMovement = 2;

    public Transform[] hitPoints;

    public Transform target;

    public bool initState = true;

    public bool hitByPlayer = false;

    public bool rebound = false , rebound_2 = false;

    bool cGNR = true;

    [HideInInspector] public bool hitted = false;

    [HideInInspector] public GameManager gm;

    AudioSource asrce;

    public AudioClip kickSFX;

    Vector3 initalPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        asrce = GetComponent<AudioSource>();
        GetComponent<SphereCollider>().radius = 0.6f;
        initalPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (hitByPlayer)
        {
            //transform.position = Vector3.Slerp(transform.position , gm.target.position , speedMovement * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, gm.target.position, speedMovement * Time.deltaTime);

            if (transform.position == gm.target.position)
            {
                hitByPlayer = false;
                rebound = true;
            }

            cGNR = true;

        }

        if (rebound)
        {
            if (cGNR)
            {
                int ran = Random.Range(0, hitPoints.Length);
                target = hitPoints[ran];
                cGNR = false;
               
            }

            //transform.position = Vector3.Slerp(transform.position, target.position, speedMovement * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedMovement * Time.deltaTime);

            if (transform.position == target.position)
            {
                hitByPlayer = false;
                rebound = false;
                rebound_2 = true;
            }
        }

        if (rebound_2)
        {
            if (!asrce.isPlaying && !cGNR)
            {
                asrce.PlayOneShot(kickSFX);
                cGNR = true;
            }

            transform.position += new Vector3(0, 0.25f * Time.deltaTime, -speedMovement * Time.deltaTime);
            hitted = false;
        }

    }

   public void Restart()
    {
        transform.position = initalPos;
        hitByPlayer = false;
        rebound = false;
        rebound_2 = false;
    }

}
