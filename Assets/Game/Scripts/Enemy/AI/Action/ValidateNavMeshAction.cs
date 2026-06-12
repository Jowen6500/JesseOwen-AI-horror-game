using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Validate NavMesh", story: "Validate NavMesh from [Agent]", category: "Action", id: "90590f720cf6024033d81e10cb9ad036")]
public partial class ValidateNavMeshAction : Action
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

        if (Agent.Value.NavMeshAgent == null)
        {
            return Status.Failure;
        }

        if (!Agent.Value.NavMeshAgent.isActiveAndEnabled)
        {
            return Status.Failure;
        }

        if (!Agent.Value.NavMeshAgent.isOnNavMesh)
        {
            return Status.Failure;
        }
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

