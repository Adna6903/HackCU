using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class ShipController : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM7", 9600);
    private string recieved_string;
    // Start is called before the first frame update
    public GameObject light;
    void Start()
    {
        sp.Open();
    }

    // Update is called once per frame
    void Update()
    {
        recieved_string = sp.ReadLine();
        string[] datas = recieved_string.Split(',');
        if (Input.GetKey(KeyCode.Space)||datas[6]=="1")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
        if(Input.GetKey(KeyCode.RightArrow)|| datas[0] == "2.00")
        {
            transform.Rotate(-Vector3.forward*0.3f);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || datas[0]=="-10.00")
        {
            transform.Rotate(Vector3.forward * 0.3f);
        }
        if(Input.GetKey(KeyCode.UpArrow)||datas[1]=="-10.00")
        {
            transform.Rotate(Vector3.right * 0.2f);
        }
        if(Input.GetKey(KeyCode.DownArrow)||datas[1]=="2.00")
        {
            transform.Rotate(-Vector3.right * 0.2f);
        }

    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Portal1"))
        {
            
            light.SetActive(false);
            Level2();
        }
    }
}
