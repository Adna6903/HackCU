using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossBattle : MonoBehaviour
{
    public GameObject player;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    private Animator playerAnim;
    public GameObject bullet;
    public TextMeshProUGUI win;
    public TextMeshProUGUI lose;
    public Button restart;
    public GameObject camera;
   
    private int hitPoints = 100;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        enemyAnim = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.AddForce(Vector3.down*100, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == new Vector3(-25,0,4))
        {
            enemyAnim.SetBool("isOnGround", true);

        }
        else
        {
            enemyAnim.SetBool("isOnGround", false);
        }
        count = FindObjectsOfType<PlayerController>().Length;
        if(count ==0)
        {
            lose.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            camera.SetActive(true);
            return;
            
        }
        if (player.transform.position.y<-2)
        {
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        }
        if(hitPoints<=0||transform.position.y<-1)
        {
            Destroy(gameObject);
            win.gameObject.SetActive(true);
        }
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * 1);
        if ((transform.position - player.transform.position).magnitude < 0.7f)
        {
            player.transform.position = new Vector3(player.transform.position.x - 1.0f, player.transform.position.y, player.transform.position.z);
            // transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            playerAnim.SetTrigger("isHit");


        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bulllet"))
        {
            hitPoints -= 2;
            
            Destroy(collision.gameObject);
        }
    }
}
