using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public float bulletSpeed = 1;
    public float fireRate = 5f;
    public Transform bullet;
    public Transform bulletInitalPos;

    public LayerMask playerLayer;
    public GameManager gm;

    Vector3 dir;

    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = gm.player.GetComponent<PlayerController>();

        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
       


        if (!gm.lostLevel && !gm.endLevel)
        {
            bullet.transform.position += dir * bulletSpeed * Time.deltaTime;

            if (!gm.bulletHitPlayer)
            {
                Collider[] targets = Physics.OverlapSphere(bullet.position, 0.03f, playerLayer);

                foreach (Collider col in targets)
                {
                    pc.DamagePlayer();
                    gm.ball.Restart();
                    gm.bulletHitPlayer = true;
                }
            }
        }
    }

    IEnumerator timer()
    {
        dir = gm.player.transform.position - bullet.position;
        yield return new WaitForSeconds(fireRate);
        bullet.position = bulletInitalPos.position;
        gm.bulletHitPlayer = false;
        StopAllCoroutines();

        if (!gm.lostLevel && !gm.endLevel)
            StartCoroutine(timer());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(bullet.position , 0.03f);
    }
}
