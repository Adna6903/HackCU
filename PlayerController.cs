using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    SerialPort sp = new SerialPort("COM7", 9600);

    public int health = 100;

    public float playerSpeed = 10f;
    private Animator playerAnim;
    private Rigidbody playerRigidbody;
    private CapsuleCollider playerCapsule;
    public GameObject gun;
    public GameObject camera;
    private bool isSprint = false;
    public TextMeshProUGUI crosshair;
    public GameObject bullet;
    public float r = 2.0f;
    private string recieved_string;
    


    void Start()
    {
        sp.Open();
        gun.SetActive(false);
        playerAnim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerCapsule = GetComponent<CapsuleCollider>();
       
    }


    // Update is called once per frame
    void LateUpdate()
    {

        

        

            recieved_string = sp.ReadLine();
            string[] datas = recieved_string.Split(',');
        Debug.Log(datas[6]);
        
        if(datas[2]=="-7.00")
        {
        float z = 5 * Input.GetAxis("Mouse X");
            transform.Rotate(0, z, 0);

        }
        else if(datas[2]=="-10.00")
        {
            float z = 5;
            transform.Rotate(0, z, 0);
        }
        else if (datas[2] == "3.00")
        {
            float z = 5;
            transform.Rotate(0, -z, 0);
        }
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");
        
        if(health<=0)
        {
            Destroy(gameObject);
        }







        if(verticalInput>0 || datas[0]=="2.00"  )
        {
            transform.Rotate(new Vector3(0, r, 0));
        }
        else if(verticalInput<0 || datas[0] == "-10.00")
        {
            transform.Rotate(new Vector3(0, -r, 0));
        }
       

        if (horizontalInput > 0 || datas[1] == "-10.00")
        {
            playerAnim.SetBool("IsNotIdle", true);
            if (Input.GetKeyDown(KeyCode.Space)||datas[6]=="1")
            {
                playerAnim.SetTrigger("isJumpWhileWalk");
                if (!isSprint)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * horizontalInput * 150);

                }
                else
                {
                    playerAnim.ResetTrigger("isJumpWhileWalk");
                }



            }
            playerAnim.SetTrigger("isWalk");
        }
        else if(horizontalInput<0 || datas[1] == "2.00")
        {
            playerAnim.SetTrigger("isWalkBack");

        }
            
            else
        {
            playerAnim.SetTrigger("isNotWalk");
            playerAnim.SetTrigger("isNotWalkBack");
            playerAnim.SetBool("IsNotIdle", false);
        }
        if (horizontalInput != 0 && Input.GetKey(KeyCode.LeftShift)||datas[4]=="1")
        {
            playerAnim.SetTrigger("isSprint");
            isSprint = true;
        }
        else
        {
            playerAnim.SetTrigger("isNotSprint");
            isSprint = false;
        }
        if (Input.GetKeyDown(KeyCode.X)|| datas[7] == "1")
        {
            playerAnim.SetBool("isGun", !playerAnim.GetBool("isGun"));
            gun.SetActive(!gun.activeInHierarchy);
            camera.SetActive(!camera.activeInHierarchy);
            crosshair.gameObject.SetActive(!crosshair.IsActive());
           
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)||datas[8]=="1")
        {
            playerAnim.SetTrigger("isShoot");
            playerAnim.SetTrigger("isAttack");
            float x = transform.rotation.x;
            float y = transform.rotation.y;
            float a = transform.rotation.z;
            float w = transform.rotation.w;
            if (gun.activeInHierarchy)
            {

            Instantiate(bullet, gun.transform.position, new Quaternion(x,y,a,w));
            }
            

        }
        if (Input.GetKeyDown(KeyCode.Space)||datas[6]=="1")
        {
            playerAnim.SetTrigger("isJump");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerAnim.SetTrigger("isRoll");
        }
        
    bullet.transform.Translate(Vector3.forward* Time.deltaTime* 100);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boss"))
        {
            health -= 10;
            Debug.Log(health);
        }
        if (collision.gameObject.CompareTag("Portal2"))
        {
            SceneManager.LoadScene("Level3", LoadSceneMode.Single);
        }
    }
    
      
}

