using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Despawn Agent", story: "Despawn [Agent]", category: "Action", id: "3b514593f979b070e34ee6812cedea9a")]
public partial class DespawnAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAIController> Agent;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null)
        {
            return Status.Failure;
        }
        Agent.Value.CallDespawn();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

