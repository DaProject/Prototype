using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject[] whatToSpawnPrefabBig;
    public GameObject[] whatToSpawnCloneBig;
    public GameObject[] whatToSpawnPrefabBoss;
    public GameObject[] whatToSpawnCloneBoss;
    public GameObject[] whatToSpawnPrefabFast;
    public GameObject[] whatToSpawnCloneFast;
    private float timeSpawnerBig;
    public float timeSpawnerBigAux;
    private int numWaveBig;
    public int numMaxWaveBig;
    private float timeSpawnerBoss;
    public float timeSpawnerBossAux;
    private int numWaveBoss;
    public int numMaxWaveBoss;
    private float timeSpawnerFast;
    public float timeSpawnerFastAux;
    private int numWaveFast;
    public int numMaxWaveFast;

    void Start()
    {
        timeSpawnerBig = timeSpawnerBigAux;
        timeSpawnerBoss = timeSpawnerBossAux;
        timeSpawnerFast = timeSpawnerFastAux;
    }

    void Update()
    {
        if(numWaveBig < numMaxWaveBig)
        {
            if (timeSpawnerBig >= 0) timeSpawnerBig -= 1 * Time.deltaTime;
            else
            {
                numWaveBig++;
                SpawnEnemieBig();
            }
        }

        if (numWaveBoss < numMaxWaveBoss)
        {
            if (timeSpawnerBoss >= 0) timeSpawnerBoss -= 1 * Time.deltaTime;
            else
            {
                numWaveBoss++;
                SpawnEnemieBoss();
            }
        }

        if (numWaveFast < numMaxWaveFast)
        {
            if (timeSpawnerFast >= 0) timeSpawnerFast -= 1 * Time.deltaTime;
            else
            {
                numWaveFast++;
                SpawnEnemieFast();
            }
        }
    }

    void SpawnEnemieBig()
    {
        for (int i = 0; i < whatToSpawnCloneBig.Length; i++)
        {
            whatToSpawnCloneBig[i] = Instantiate(whatToSpawnPrefabBig[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerBig = timeSpawnerBigAux;
        }
    }

    void SpawnEnemieBoss()
    {
        for (int i = 0; i < whatToSpawnCloneBoss.Length; i++)
        {
            whatToSpawnCloneBoss[i] = Instantiate(whatToSpawnPrefabBoss[0], spawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerBoss = timeSpawnerBossAux;
        }
    }

    void SpawnEnemieFast()
    {
        for (int i = 0; i < whatToSpawnCloneFast.Length; i++)
        {
            whatToSpawnCloneFast[i] = Instantiate(whatToSpawnPrefabFast[0], spawnLocation[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerFast = timeSpawnerFastAux;
        }
    }
}
