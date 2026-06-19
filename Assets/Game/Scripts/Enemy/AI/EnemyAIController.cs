using System.Collections;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private BehaviorGraphAgent _behaviorGraphAgent;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private SightPerception _sightPerception;
    
    public UnityEvent OnDespawn;
    
    public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public PlayerCharacter PlayerCharacter => _playerCharacter;
    public SightPerception SightPerception => _sightPerception;

    private void Despawn()
    {
        StartCoroutine(DespawnAfterEndOfFrame());
    }
    public void CallDespawn(){Despawn();}
    
    private IEnumerator DespawnAfterEndOfFrame()
    {
        if (_behaviorGraphAgent != null)
        {
            _behaviorGraphAgent.SetVariableValue("CanSeeTarget", false);
            _behaviorGraphAgent.enabled = false;
        }

        if (_navMeshAgent != null && _navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.enabled = false;
        }
        
        OnDespawn?.Invoke();
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter character = collision.gameObject.GetComponent<PlayerCharacter>();
            if (character != null)
            {
                character.CallDeath();
            }
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerCharacter character = other.gameObject.GetComponent<PlayerCharacter>();
            if (character != null && !character.IsDead)
            {
                character.CallDeath();
            }
        }
    }
}
