using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _kamikadze;
    [SerializeField] private GameObject _easyEnemy; 
    [SerializeField] private GameObject _beamEnemy;
    [SerializeField] private GameObject _diagonalEnemy;
    [SerializeField] private Vector2 ScreenSpawn = new Vector2(30f, 21f);
    [SerializeField] private float spawnInterval = 7.0f;
    [SerializeField] private int EnemyCount;
    [SerializeField] private float offScreenOffset = 5f;
    private float Timer;

    void Start ()
    {
        StartCoroutine(WaveSpawner());
    }

    void Update ()
    {

    }

    private IEnumerator WaveSpawner ()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            LeftKamikadzeWave(2);
            yield return new WaitForSeconds(spawnInterval / 2);
            RightKamikadzeWave(2);
            yield return new WaitForSeconds(spawnInterval);
            EasyEnemyWave(3);
            yield return new WaitForSeconds(spawnInterval);
            DoubleBeamEnemy(2);
            yield return new WaitForSeconds(spawnInterval);
            DiagonalEnemyWave(3);
            yield return new WaitForSeconds(spawnInterval / 2);
            LeftKamikadzeWave(2);
            yield return new WaitForSeconds(spawnInterval / 2);
            RightKamikadzeWave(2);
            yield return new WaitForSeconds(spawnInterval);
            EasyEnemyWave(3);
            yield return new WaitForSeconds(spawnInterval);
            DoubleBeamEnemy(2);
            yield return new WaitForSeconds(spawnInterval);
            DiagonalEnemyWave(3);
            spawnInterval /= (float) 1.15;
        }
    }
    void LeftKamikadzeWave (int EnemyCount)
    {
        for (int i = 0; i < EnemyCount / 2; i++)
        {
            float x = Random.Range(-30f, -26f);
            float y = ScreenSpawn.y;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, -ScreenSpawn.y - offScreenOffset); 
            SpawnEnemy(spawnPoint, target, _kamikadze);
        }
        for (int i = 0; i < EnemyCount / 2; i++)
        {
            float x = Random.Range(-24f, -21f);
            float y = ScreenSpawn.y;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, -ScreenSpawn.y - offScreenOffset);
            SpawnEnemy(spawnPoint, target, _kamikadze);
        }
    }
    void RightKamikadzeWave (int EnemyCount)
    {
        for (int i = 0; i < EnemyCount / 2; i++)
        {
            float x = Random.Range(21f, 24f);
            float y = ScreenSpawn.y;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, -ScreenSpawn.y - offScreenOffset);
            SpawnEnemy(spawnPoint, target, _kamikadze);
        }
        for (int i = 0; i < EnemyCount / 2; i++)
        {
            float x = Random.Range(24f, 30f);
            float y = ScreenSpawn.y;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, -ScreenSpawn.y - offScreenOffset);
            SpawnEnemy(spawnPoint, target, _kamikadze);
        }
    }
    void EasyEnemyWave (int EnemyCount)
    {
        float spacingX = (ScreenSpawn.x * 2) / EnemyCount;

        for (int i = 0; i < EnemyCount; i++)
        {
            float x = -ScreenSpawn.x + spacingX * i;
            float y = ScreenSpawn.y + offScreenOffset;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, Random.Range(ScreenSpawn.y - offScreenOffset, ScreenSpawn.y));
            SpawnEnemy(spawnPoint, target, _easyEnemy);
        }
    }

    void DiagonalEnemyWave (int EnemyCount)
    {
        float spacingX = (ScreenSpawn.x * 2) / EnemyCount;

        for (int i = 0; i < EnemyCount; i++)
        {
            float x = -ScreenSpawn.x + spacingX * i;
            float y = ScreenSpawn.y + offScreenOffset;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, Random.Range(ScreenSpawn.y - offScreenOffset, ScreenSpawn.y));
            SpawnEnemy(spawnPoint, target, _diagonalEnemy);
        }
    }
    void DoubleBeamEnemy (int EnemyCount)
    {
        float spacingX = (ScreenSpawn.x * 2) / EnemyCount;

        for (int i = 0; i < EnemyCount; i++)
        {
            float x = -ScreenSpawn.x + spacingX * i;
            float y = ScreenSpawn.y + offScreenOffset;
            Vector2 spawnPoint = new Vector2(x, y);
            Vector2 target = new Vector2(x, Random.Range(ScreenSpawn.y - offScreenOffset, ScreenSpawn.y));
            SpawnEnemy(spawnPoint, target, _beamEnemy);
        }
    }
    void SpawnEnemy (Vector2 spawnPoint, Vector2 target, GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        IEnemyMovement enemyScript = enemy.GetComponent<IEnemyMovement>();
        if (enemyScript != null)
        {
            enemyScript.SetPositions(spawnPoint, target);
        }
        else
        {
            Debug.LogWarning($"Enemy prefab {enemyPrefab.name} does not have an IEnemyMovement component!");
        }
    }
}