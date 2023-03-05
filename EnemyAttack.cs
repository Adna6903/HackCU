using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    private Animator playerAnim;
    public GameObject bullet;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
        playerAnim =  player.GetComponent<Animator>();
        enemyRb.AddForce(Physics.gravity * 10,ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * 1);
        if ((transform.position - player.transform.position).magnitude < 2.5f)
        {
           player.transform.position = new Vector3(player.transform.position.x-1.0f, player.transform.position.y, player.transform.position.z);
            // transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            playerAnim.SetTrigger("isHit");


        }
      
    }
}