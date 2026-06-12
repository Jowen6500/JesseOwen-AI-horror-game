using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Can See Target", story: "Set [CanSeeTarget] from [Agent]", category: "Action", id: "c80ce0f588650086e3282d37fad86ed2")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
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
        CanSeeTarget.Value = Agent.Value.SightPerception.CanSeePlayer;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

