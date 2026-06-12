using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Target is Hiding", story: "Set [TargetIsHiding] from [Agent]", category: "Action", id: "7f707814cd01edc61aae36423ecee72c")]
public partial class SetTargetIsHidingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> TargetIsHiding;
    [SerializeReference] public BlackboardVariable<EnemyAIController> Agent;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null && Agent.Value.PlayerCharacter == null)
        {
            return Status.Failure;
        }

        TargetIsHiding.Value = Agent.Value.PlayerCharacter.IsHiding;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

