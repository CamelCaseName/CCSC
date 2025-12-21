using System.Text.Json.Serialization;

namespace CCSC.Nodestuff
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NodeType
    {
        Achievement,
        AlternateText,
        BGC,
        BGCResponse,
        CharacterGroup,
        Clothing,
        CriteriaGroup,
        Criterion,
        Cutscene,
        Dialogue,
        Door,
        EventTrigger,
        GameEvent,
        Inventory,
        ItemAction,
        ItemGroup,
        ItemGroupBehaviour,
        ItemGroupInteraction,
        ItemInteraction,
        Null,
        Personality,
        Pose,
        Property,
        Quest,
        Response,
        Social,
        State,
        InteractiveItemBehaviour,
        UseWith,
        Value,
    }

    public enum SpawnableNodeType
    {
        Achievement,
        AlternateText,
        BGC,
        BGCResponse,
        CriteriaGroup,
        Criterion,
        Dialogue,
        EventTrigger,
        GameEvent,
        ItemAction,
        ItemGroup,
        ItemGroupInteraction,
        ItemInteraction,
        Quest,
        Response,
        InteractiveItemBehaviour,
        UseWith,
        Value,
        ItemGroupBehaviour
    }

    public static class NodeConstants
    {
        internal static SpawnableNodeType[] GetLinkableNodeTypes(NodeType input)
        {
            //constructed as the inverse of AllLink()
            if (Main.SelectedCharacter == Main.Player)
            {
                //main story link options are different than characterstory
                switch (input)
                {
                    case NodeType.Criterion:
                    {
                        return [SpawnableNodeType.ItemAction, SpawnableNodeType.UseWith, SpawnableNodeType.CriteriaGroup, SpawnableNodeType.GameEvent, SpawnableNodeType.EventTrigger, SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.ItemGroupBehaviour, SpawnableNodeType.ItemGroup, SpawnableNodeType.Value];
                    }
                    case NodeType.ItemAction:
                    case NodeType.UseWith:
                    {
                        return [SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.ItemGroupBehaviour, SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent];
                    }
                    case NodeType.InteractiveItemBehaviour:
                    {
                        return [SpawnableNodeType.ItemGroupBehaviour, SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent, SpawnableNodeType.ItemAction, SpawnableNodeType.UseWith, SpawnableNodeType.ItemGroup];
                    }
                    case NodeType.ItemGroupBehaviour:
                    {
                        return [SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent, SpawnableNodeType.ItemAction, SpawnableNodeType.UseWith];
                    }
                    case NodeType.CriteriaGroup:
                    {
                        return [SpawnableNodeType.CriteriaGroup, SpawnableNodeType.Criterion];
                    }
                    case NodeType.ItemGroup:
                    {
                        return [SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent];
                    }
                    case NodeType.Value:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.Criterion, SpawnableNodeType.Value];
                    }
                    case NodeType.EventTrigger:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.Criterion];
                    }
                    case NodeType.GameEvent:
                    {
                        return [SpawnableNodeType.Criterion, SpawnableNodeType.ItemAction, SpawnableNodeType.UseWith, SpawnableNodeType.EventTrigger, SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.ItemGroupBehaviour, SpawnableNodeType.ItemGroup, SpawnableNodeType.Value];
                    }
                    default:
                    {
                        return [SpawnableNodeType.ItemAction, SpawnableNodeType.UseWith, SpawnableNodeType.EventTrigger, SpawnableNodeType.InteractiveItemBehaviour, SpawnableNodeType.ItemGroupBehaviour, SpawnableNodeType.ItemGroup, SpawnableNodeType.Value, SpawnableNodeType.CriteriaGroup];
                    }
                }
            }
            else
            {
                //has more than main story as the interactions are added
                switch (input)
                {
                    case NodeType.Criterion:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.EventTrigger, SpawnableNodeType.AlternateText, SpawnableNodeType.Response, SpawnableNodeType.BGC, SpawnableNodeType.ItemInteraction, SpawnableNodeType.ItemGroupInteraction, SpawnableNodeType.Quest, SpawnableNodeType.Value];
                    }
                    case NodeType.AlternateText:
                    {
                        return [SpawnableNodeType.Criterion, SpawnableNodeType.Dialogue];
                    }
                    case NodeType.Response:
                    {
                        return [SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent, SpawnableNodeType.Dialogue];
                    }
                    case NodeType.Dialogue:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.Response, SpawnableNodeType.AlternateText];
                    }
                    case NodeType.BGC:
                    {
                        return [SpawnableNodeType.Criterion, SpawnableNodeType.GameEvent, SpawnableNodeType.BGCResponse];
                    }
                    case NodeType.BGCResponse:
                    {
                        return [SpawnableNodeType.BGC];
                    }
                    case NodeType.Value:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.Criterion, SpawnableNodeType.Value];
                    }
                    case NodeType.Quest:
                    {
                        return [SpawnableNodeType.Quest, SpawnableNodeType.GameEvent, SpawnableNodeType.Criterion];
                    }
                    case NodeType.ItemInteraction:
                    case NodeType.ItemGroupInteraction:
                    case NodeType.Personality:
                    case NodeType.EventTrigger:
                    case NodeType.State:
                    {
                        return [SpawnableNodeType.GameEvent, SpawnableNodeType.Criterion];
                    }
                    case NodeType.GameEvent:
                    {
                        return [SpawnableNodeType.Criterion, SpawnableNodeType.EventTrigger, SpawnableNodeType.Value, SpawnableNodeType.Response, SpawnableNodeType.Dialogue, SpawnableNodeType.BGC, SpawnableNodeType.ItemInteraction, SpawnableNodeType.ItemGroupInteraction, SpawnableNodeType.Quest];
                    }
                    default:
                    {
                        return [SpawnableNodeType.EventTrigger, SpawnableNodeType.Value, SpawnableNodeType.Dialogue, SpawnableNodeType.BGC, SpawnableNodeType.GameEvent];
                    }
                }
            }
        }
    }
}