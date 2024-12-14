using System.Collections;
using Characters.Player;
using Phase;
using UnityEngine;
using Utilities;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player m_Player;
    
    [Header("Phase config")]
    int m_PhaseCount;
    Cooldown m_SpawnCooldown;
    Cooldown m_WaveCooldown;
    [SerializeField] PhaseDatabase m_Phases;
    [SerializeField] float m_SpawnDistanceFromPlayer;

    //TODO enemy apparition
    //TODO better aim enemy AI
    //TODO better movement enemy AI
    //TODO projectile database (scriptable objects)

    void Start()
    {
        StartPhase();
    }

    void StartPhase()
    {
        m_PhaseCount++;
        StartCoroutine(SpawnWave(m_Phases.data[m_PhaseCount].waves[0]));
    }

    IEnumerator SpawnWave(Wave wave)
    {
        
        
        yield return null;
    }
}
