using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public float HP = 50;

    public GameObject dustParticle;

    public AudioClip Broke;

   // MeshRenderer mesh;

    //AudioSource asrce;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //mesh = GetComponent<MeshRenderer>();
       // asrce = GetComponent<AudioSource>();
        //gm = GameManager.g_ball.GetComponent<BallController>().gm;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "Ball")
        {

            if (!collision.GetComponent<BallController>().hitted){
                //mesh.enabled = false;
                //asrce.PlayOneShot(Broke);
                Instantiate(dustParticle, transform.position, transform.rotation);
                HP -= 50;
                collision.GetComponent<BallController>().hitted = true;
                if (HP <= 0)
                {
                    gm.score++;
                    Destroy(this.gameObject);
                }
                
            }
            
        }
    }
}
