using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait Until Reach Destination", story: "[Agent] wait until reach destination", category: "Action", id: "87c5353aa8a2d9cc8dc3f8da30e16d60")]
public partial class WaitUntilReachDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyAIController> Agent;
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.2f);

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
        
        NavMeshAgent agent = Agent.Value.NavMeshAgent;

        if (agent == null)
        {
            return Status.Failure;
        }

        if (agent.pathPending)
        {
            return Status.Running;
        }

        if (agent.remainingDistance > agent.stoppingDistance + DistanceThreshold)
        {
            return Status.Running;
        }
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

