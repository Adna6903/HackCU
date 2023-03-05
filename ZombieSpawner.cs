using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    private int count;
    private int wave = 1;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        ZombieSpawners(wave);
    }

    void ZombieSpawners(int WaveCount)
    {
        for (int i = 0; i < WaveCount; i++)
        {
            Instantiate(zombie, new Vector3(Random.Range(60, 70), 3, Random.Range(60, 70)), zombie.transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        count = FindObjectsOfType<EnemyAttack>().Length;
        if(count == 1&&wave<5)
        {
            wave++;
            ZombieSpawners(wave);
        }
        if(wave>=5)
        {
            portal.SetActive(true);
        }

    }

  

}
