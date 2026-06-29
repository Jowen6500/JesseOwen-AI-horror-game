using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Target Last Seen Position", story: "Set [TargetLastSeenPosition] from [Agent]", category: "Action", id: "7c48db403cfdc871b4c5b55ed7dffc8d")]
public partial class SetTargetLastSeenPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> TargetLastSeenPosition;
    [SerializeReference] public BlackboardVariable<EnemyAIController> Agent;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null && Agent.Value.SightPerception == null)
        {
            return Status.Failure;
        }
        TargetLastSeenPosition.Value = Agent.Value.SightPerception.LastSeenPosition;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

