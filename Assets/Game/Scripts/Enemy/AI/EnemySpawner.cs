using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyAIController _enemyAIController;
    [SerializeField] private float _minSpawnDelay = 5;
    [SerializeField] private float _maxSpawnDelay = 8;
    [SerializeField] private float _minSpawnDistance = 3;
    [SerializeField] private float _maxSpawnDistance = 5;
    
    private Coroutine _spawnCoroutine;
    
    private void RestartSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
        _spawnCoroutine = StartCoroutine(StartSpawn());
    }
    public void CallRestartSpawn(){RestartSpawn();}

    public IEnumerator StartSpawn()
    {
        float spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        
        yield return new WaitForSeconds(spawnDelay);

        if (_enemyAIController.PlayerCharacter == null || _enemyAIController.PlayerCharacter.IsHiding)// << diubah jadi or
        {
            RestartSpawn();
            yield break;
        }
        
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        float spawnDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 spawnPosition = _enemyAIController.PlayerCharacter.transform.position - _enemyAIController.PlayerCharacter.transform.forward * spawnDistance;
        
        //spawnPosition.y = _enemyAIController.transform.position.y;
        //perbaikan materi video
        //spawn position y-nya diubah ke posisi y-nya player
        spawnPosition.y = _enemyAIController.PlayerCharacter.transform.position.y;
        
        _enemyAIController.NavMeshAgent.enabled = true;
        _enemyAIController.NavMeshAgent.Warp(spawnPosition);
        _enemyAIController.transform.LookAt(_enemyAIController.PlayerCharacter.transform);
        
        _enemyAIController.gameObject.SetActive(true);

        _enemyAIController.BehaviorGraphAgent.SetVariableValue("TargetLastSeenPosition", _enemyAIController.PlayerCharacter.transform.position);
        _enemyAIController.BehaviorGraphAgent.enabled = true;
    }
}
