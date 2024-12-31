using System.Collections;
using Characters;
using Characters.Enemy;
using Characters.Player;
using Phase;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player m_Player;
    
    [Header("Phase settings")]
    int m_PhaseCount;
    int m_EnemiesLeft;
    bool m_IsSpawning;
    [SerializeField] PhaseDatabase m_Phases;
    [SerializeField] float m_SpawnDistanceFromPlayer;

    [Header("Offscreen indicator ref")]
    [SerializeField] GameObject m_IndicatorGo;
    [SerializeField] RectTransform m_IndicatorContainer;
    [SerializeField] UnityEngine.Camera m_Camera;

    //TODO Yes, it's my TODO list. :)
    
    //TODO enemy apparition
    //TODO projectile factory pattern?
    //TODO projectile + indicator pool pattern
    //TODO new projectiles (seeker, bomb, laser, inversed bomb)
    //TODO projectile database (scriptable objects)
    //TODO visuals and glowing effects (bling bling you know)
    //TODO better aim enemy AI
    //TODO better movement enemy AI

    void Start()
    {
        StartPhase();
    }

    void StartPhase()
    {
        
        // No more phases : level complete
        if (m_PhaseCount == m_Phases.data.Count) return;
        
        StartCoroutine(SpawnWaves(m_Phases.data[m_PhaseCount].waves));
        m_PhaseCount++;
    }

    IEnumerator SpawnWaves(Wave[] waves)
    {
        print("Start phase.");
        m_IsSpawning = true;
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].amount; j++)
            {
                Enemy newEnemy = Instantiate(waves[i].enemyGo, GetEnemySpawnPosition(), Quaternion.identity).GetComponent<Enemy>();
                newEnemy.Init(m_Player);
                newEnemy.OnDie += OnEnemyDied;

                OffscreenIndicator indicator = Instantiate(m_IndicatorGo, m_IndicatorContainer).GetComponent<OffscreenIndicator>();
                indicator.Init(m_Camera, m_IndicatorContainer.GetComponent<RectTransform>(), newEnemy);

                m_EnemiesLeft++;
                yield return new WaitForSeconds(waves[i].spawnCooldown);
            }
            
            yield return new WaitForSeconds(waves[i].timeBeforeNext);
        }
        
        print("Spawn ENDED");
        m_IsSpawning = false;
        yield return null;
    }

    Vector2 GetEnemySpawnPosition()
    {
        float angle = Random.Range(0f, 360f);
        Vector2 positionFromZero = new Vector2
        {
            x = Mathf.Sin(angle) * m_SpawnDistanceFromPlayer,
            y = Mathf.Cos(angle) * m_SpawnDistanceFromPlayer
        };
        return positionFromZero + (Vector2)m_Player.transform.position;
    }

    void OnEnemyDied(Character enemy)
    {
        m_EnemiesLeft--;
        enemy.OnDie -= OnEnemyDied;

        print("Enemies left : " + m_EnemiesLeft);
        
        // Phase complete
        if (!m_IsSpawning && m_EnemiesLeft == 0) StartPhase();
    }
}
