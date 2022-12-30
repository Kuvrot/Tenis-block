using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer_Script : MonoBehaviour
{

    public float smooth = 5f;
    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirLook = GameManager.g_ball.position - head.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dirLook);
        transform.rotation = Quaternion.Slerp(head.transform.rotation , lookRot , smooth * Time.deltaTime);

    }
}
