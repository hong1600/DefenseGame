using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    Terrain terrain;
    
    event Action onEnemySpawn;

    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<Transform> enemySpawnPointList;
    [SerializeField] Vector3[] enemySpawnPos = new Vector3[4];
    [SerializeField] Transform[] targetPoints;
    [SerializeField] float enemySpawnDelay;

    private void Awake()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Start()
    {
        enemySpawnDelay = 0;

        enemySpawnPos[0] = new Vector3(-3f, 0, 0);
        enemySpawnPos[1] = new Vector3(-1f, 0, 0);
        enemySpawnPos[2] = new Vector3(1f, 0, 0);
        enemySpawnPos[3] = new Vector3(3f, 0, 0);
    }

    public void SpawnEnemy()
    {
        if (Shared.gameManager.Round.GetCurRound() == 0 || Shared.gameManager.Round.GetIsBossRound()) { return; }

        if (enemySpawnDelay <= 0)
        {
            int firstSpawnPoint = Random.Range(0, enemySpawnPointList.Count);
            int secondSpawnPoint;
            do
            {
                secondSpawnPoint = Random.Range(0, enemySpawnPointList.Count);
            } while (firstSpawnPoint == secondSpawnPoint);

            EEnemy eEnemy1 = (EEnemy)Random.Range(0, 2);
            EEnemy eEnemy2 = (EEnemy)Random.Range(0, 2);

            for (int i = 0; i < 1; i++)
            {
                //�� ��������
                GameObject obj1 = Shared.objectPoolManager.EnemyPool.FindEnemy(eEnemy1);
                GameObject obj2 = Shared.objectPoolManager.EnemyPool.FindEnemy(eEnemy2);

                //�� �ʱ���ġ
                Vector3 spawnPos1 = enemySpawnPointList[firstSpawnPoint].transform.position + (enemySpawnPos[i]);
                Vector3 spawnPos2 = enemySpawnPointList[secondSpawnPoint].transform.position + (enemySpawnPos[i]);

                //�ͷ��� ���� ����
                spawnPos1.y = terrain.SampleHeight(spawnPos1);
                spawnPos2.y = terrain.SampleHeight(spawnPos2);

                //�� ���� ������ġ
                obj1.transform.position = spawnPos1;
                obj2.transform.position = spawnPos2;

                onEnemySpawn?.Invoke();
            }

            enemySpawnDelay = 2f;
        }

        enemySpawnDelay -= Time.deltaTime;
    }

    public void SubEnemySpawn(Action _listener) { onEnemySpawn += _listener; }
    public void UnEnemySpawn(Action _listener) { onEnemySpawn -= _listener; }
    public float GetEnemySpawnDelay() { return enemySpawnDelay; }
    public void SetEnemySpawnDelay(float _value) { enemySpawnDelay = _value; }
    public Transform[] GetTargetPoint() { return targetPoints; }
    public List<Transform> GetEnemySpawnPointList() { return enemySpawnPointList; }
}
