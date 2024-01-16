using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTriggers : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {   
            other.GetComponent<BallController>().Restart();
            other.GetComponent<BallController>().gm.LostBall();
        }
    }
}
