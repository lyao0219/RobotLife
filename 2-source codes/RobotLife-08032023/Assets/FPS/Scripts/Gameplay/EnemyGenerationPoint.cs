using UnityEngine;
using Unity.FPS.Game;

public class EnemyGenerationPoint : MonoBehaviour
{
    [Tooltip("Enemy prefab to instantiate")]
    public GameObject enemyPrefab;
    [Tooltip("time interval between one enemy generation to the next one")]
    public float generationInterval;

    float idleTime = 0f;
    bool isIdle = false;
    GameObject generatedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        generatedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
    }

    private void Update()
    {
        if(isIdle) { 
            idleTime += Time.deltaTime;

            if(idleTime >= generationInterval)
            {
                isIdle = false;
                idleTime = 0f;
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void OnEnemyKilled(EnemyKillEvent evt)
    {
        if(evt.Enemy.gameObject == generatedEnemy)
            isIdle = true;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
    }

}
