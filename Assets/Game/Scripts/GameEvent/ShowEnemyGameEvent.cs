using UnityEngine;

public class ShowEnemyGameEvent : GameEventBase
{
    [SerializeField] private GameObject _enemyObject;
    [SerializeField] private bool _isDestroyAfterFinished;

    public override void Trigger()
    {
        if (_enemyObject != null)
        {
            _enemyObject.SetActive(true);
        }
        base.Trigger();
    }

    public override void Finish()
    {
        if (_enemyObject != null && _isDestroyAfterFinished)
        {
            Destroy(_enemyObject);
        }
        base.Finish();
    }
}
