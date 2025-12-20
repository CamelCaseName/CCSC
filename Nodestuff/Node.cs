using CCSC.Direct2D;
using CCSC.Glue;
using CCSC.StoryItems;
using System.Data;
using System.Text.Json.Serialization;
using static CCSC.StoryItems.StoryEnums;

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

    public sealed class MissingReferenceInfo
    {
        public MissingReferenceInfo()
        {
            Text = string.Empty;
        }
        public MissingReferenceInfo(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }

    public sealed class Node
    {
        public static readonly string[] AllowedFileNames = [Main.Player, .. Enum.GetNames<Characters>().Cast<string>(), "Phone Call", "Liz Katz", "Doja Cat"];

        public static readonly Node NullNode = new();
        public string ID;
        private SizeF size = new(Main.NodeSizeX, Main.NodeSizeY);
        public string StaticText;
        public string _text = string.Empty;
        public NodeType Type;
        private readonly Dictionary<int, PointF> Positions = [];
        private readonly List<string> dupedFileNames = [];
        private object? data = null;
        private Type dataType = typeof(MissingReferenceInfo);

        private string fileName = Main.NoCharacter;
        private readonly string origfilename = Main.NoCharacter;
        private bool CacheValid = false;
        public string OrigFileName => origfilename;

        public Node(string iD, NodeType type, string text, string file)
        {
            ID = iD;
            StaticText = text;
            Type = type;
            Size = new SizeF(Main.NodeSizeX, Main.NodeSizeY);
            Main.SetNodePos(this);
            FileName = file;
            origfilename = file;
            Positions.Add(Main.FileIndex(file), default);
        }

        public Node(string iD, NodeType type, string text, object data, string file)
        {
            ID = iD;
            StaticText = text;
            Type = type;
            RawData = data;
            DataType = data.GetType();
            Size = new SizeF(Main.NodeSizeX, Main.NodeSizeY);
            Main.SetNodePos(this);
            FileName = file;
            origfilename = file;
            Positions.Add(Main.FileIndex(file), default);
        }

        public Node()
        {
            ID = string.Empty;
            StaticText = string.Empty;
            Type = NodeType.Null;
            Main.SetNodePos(this);
            fileName = Main.NoCharacter;
            origfilename = Main.NoCharacter;
        }

        public Type DataType { get => dataType; private set => dataType = value; }

        public string FileName
        {
            get
            {
                return fileName;
            }

            private set
            {
                if (!AllowedFileNames.Contains(value))
                {
                    //todo remove once charactergroups are in
                    value = Main.Player;
                }
                fileName = value;
            }
        }

        public PointF Position
        {
            get
            {
                if (Positions.TryGetValue(Main.SelectedFile, out var result))
                {
                    if (result.X != _rect.X || result.Y != _rect.Y)
                    {
                        _rect = new(result, Size);
                        _roundedRect = _rect.ToRoundedRect(10f);
                    }
                    return result;
                }
                else
                {
                    return PointF.Empty;
                }
            }

            set
            {
                Main.ClearNodePos(this, Main.SelectedFile);
                Positions[Main.SelectedFile] = value;
                Main.SetNodePos(this, Main.SelectedFile);
                Main.NeedsSaving = true;
                _rect = new(value, Size);
                _roundedRect = _rect.ToRoundedRect(10f);
            }
        }

        public object? RawData
        {
            get
            {
                return data ?? new MissingReferenceInfo(Text);
            }
            set
            {
                if (data is IItem item)
                {
                    item.OnBeforeChange -= PreUpdate;
                    item.OnAfterChange -= PostUpdate;
                }

                data = value;
                DataType = value?.GetType() ?? typeof(object);

                if (DataType == typeof(Dialogue) || DataType == typeof(ItemInteraction) || DataType == typeof(ItemGroupInteraction))
                {
                    size.Height *= 2;
                }

                if (data is IItem item2)
                {
                    item2.OnBeforeChange += PreUpdate;
                    item2.OnAfterChange += PostUpdate;
                }
                CacheValid = false;
            }
        }

        public void PreUpdate(object d)
        {
            Main.PreUpdateNode(this);
        }

        public void PostUpdate(object d)
        {
            Main.UpdateNode(this);
            CacheValid = false;
        }

        private RectangleF _rect = default;
        public RectangleF Rectangle { get => _rect; }

        private Silk.NET.Direct2D.RoundedRect _roundedRect = default;

        // 1 = dark default, 0 = black and 2 = light
        internal int TextColor = 1;

        public Silk.NET.Direct2D.RoundedRect RoundRectangle { get => _roundedRect; }

        public string Text
        {
            get
            {
                if (DataType == typeof(MissingReferenceInfo))
                {
                    return StaticText;
                }

                if (!CacheValid)
                {
                    _text = GetText();
                    CacheValid = true;
                }
                return _text;
            }
        }

        private string GetText()
        {
            switch (Type)
            {
                case NodeType.Null:
                {
                    return "Null node!";
                }
                case NodeType.CharacterGroup:
                {
                    return $"{Data<CharacterGroup>()?.Id}|{Data<CharacterGroup>()?.Name} -> {Data<CharacterGroup>()?.CharactersInGroup?.ListRepresentation()}";
                }
                case NodeType.Criterion:
                {
                    return Data<Criterion>()!.ToString();
                }
                case NodeType.ItemAction:
                {
                    return $"{Data<ItemAction>()?.ActionName}";
                }
                case NodeType.ItemGroupBehaviour:
                {
                    return $"{Data<ItemGroupBehavior>()?.Name} -> {Data<ItemGroupBehavior>()?.Name}";
                }
                case NodeType.ItemGroupInteraction:
                {
                    return $"{Data<ItemGroupInteraction>()?.Name} on {Data<ItemGroupBehavior>()?.GroupName} | {Data<ItemGroupBehavior>()?.Id}";
                }
                case NodeType.Achievement:
                {
                    return $"{Data<Achievement>()?.Name} | {Data<Achievement>()?.Id}";
                }
                case NodeType.BGC:
                {
                    return $"{Data<BackgroundChatter>()?.Text} | {Data<BackgroundChatter>()?.Id}";
                }
                case NodeType.BGCResponse:
                {
                    return $"{Data<BackgroundChatterResponse>()?.Label} -> respondant: {Data<BackgroundChatterResponse>()?.CharacterName}|{Data<BackgroundChatterResponse>()?.ChatterId}";
                }
                case NodeType.CriteriaGroup:
                {
                    return $"{Data<CriteriaGroup>()?.Name} on group: {Data<CriteriaGroup>()?.LinkedGroupName} | {Data<CriteriaGroup>()?.Id}";
                }
                case NodeType.Dialogue:
                {
                    return $"{Data<Dialogue>()?.ID} | {Data<Dialogue>()?.Text}";
                }
                case NodeType.AlternateText:
                {
                    return $"{Data<AlternateText>()?.Text}";
                }
                case NodeType.GameEvent:
                {
                    GameEvent gevent = Data<GameEvent>()!;
                    if (gevent is null)
                    {
                        return StaticText;
                    }
                    return gevent.ToString();
                }
                case NodeType.EventTrigger:
                {
                    var etrigger = Data<EventTrigger>();
                    if (etrigger is null)
                    {
                        return StaticText;
                    }
                    return etrigger.ToString();
                }
                case NodeType.ItemGroup:
                {
                    return $"{Data<ItemGroup>()?.Name} | {Data<ItemGroup>()?.Id} |  -> {Data<ItemGroup>()?.ItemsInGroup.ListRepresentation()}";
                }
                case NodeType.Personality:
                {
                    if (dataType == typeof(Trait))
                    {
                        return $"{Data<Trait>()?.Type} : {Data<Trait>()?.Value}";
                    }
                    else
                    {
                        return StaticText;
                    }
                }
                case NodeType.Quest:
                {
                    if (Data<Quest>() is null)
                    {
                        return StaticText;
                    }
                    else
                    {
                        return $"{Data<Quest>()?.Name ?? ID} -> {Data<Quest>()?.CharacterName}|{Data<Quest>()?.Name} on completion: [{Data<Quest>()?.CompletedDetails}] on failure: [{Data<Quest>()?.FailedDetails}] ";
                    }
                }
                case NodeType.Response:
                {
                    return $"{Data<Response>()?.Id} -> {Data<Response>()?.Text} | leads to: {Data<Response>()?.Next}";
                }
                case NodeType.Value:
                {
                    return $"{fileName}:{Data<string>()} \n{StaticText}";
                }
                case NodeType.UseWith:
                {
                    return $"{Data<UseWith>()?.ItemName} -> {Data<UseWith>()?.CustomCantDoThatMessage}";
                }
                case NodeType.Pose:
                {
                    return "Pose " + EEnum.Parse<Poses>(ID);
                }
                case NodeType.Inventory:
                case NodeType.InteractiveItemBehaviour:
                case NodeType.ItemInteraction:
                case NodeType.Property:
                case NodeType.Social:
                case NodeType.State:
                case NodeType.Door:
                case NodeType.Clothing:
                case NodeType.Cutscene:
                default:
                {
                    if (string.IsNullOrEmpty(StaticText))
                    {
                        return $"{fileName} | {Type} | {ID}";
                    }
                    else
                    {
                        return StaticText;
                    }
                }
            }
        }

        public IEnumerable<string> DupedFileNames { get => dupedFileNames; }

        public SizeF Size
        {
            get => size; set
            {
                size = value;
                _rect = new(Position, value);
                _roundedRect = _rect.ToRoundedRect(10f);
            }
        }

        public static Node CreateCriteriaNode(Criterion criterion, string filename, NodeStore nodes)
        {
            //create all criteria nodes the same way so they can possibly be replaced by the actual text later
            var result = nodes.Nodes.FirstOrDefault((n) => n.Type == NodeType.Criterion && n.ID == criterion.ToString());
            if (result is not null)
            {
                return result;
            }

            return new Node(
                criterion.ToString(),
                NodeType.Criterion,
                $"{criterion.Character}|{criterion.CompareType}|{criterion.Key}|{criterion.Value}",
                filename)
            { RawData = criterion }
            ;
        }

        public void AddCriteria(List<Criterion> criteria, NodeStore nodes)
        {
            foreach (Criterion criterion in criteria)
            {
                Node tempNode = CreateCriteriaNode(criterion, FileName, nodes);
                tempNode.RawData = criterion;
                nodes.AddParent(this, tempNode);
            }
        }

        public void AddEvents(List<GameEvent> events, NodeStore nodes)
        {
            foreach (GameEvent _event in events)
            {
                var nodeEvent = new Node(_event.Id ?? "none", NodeType.GameEvent, _event.Value ?? "none", this, FileName)
                {
                    RawData = _event,
                    DataType = typeof(GameEvent),
                };

                nodeEvent.AddCriteria(_event.Criteria ?? [], nodes);

                nodes.AddChild(this, nodeEvent);
            }
        }

        public T? Data<T>() where T : class
        {
            if (typeof(T) == DataType && RawData is not null)
            {
                return (T)RawData;
            }
            else
            {
                return null;
            }
        }

        public bool DupeToOtherSorting(string filename)
        {
            int i = Main.FileIndex(filename);
            if (Positions.TryAdd(i, default))
            {
                if (origfilename != filename)
                {
                    dupedFileNames.Add(filename);
                }

                Main.SetNodePos(this, i);
                return true;
            }
            return false;
        }

        public void RemoveFromSorting(int filename)
        {
            Positions.Remove(filename);
            Main.ClearNodePos(this, filename);
        }

        public override string ToString()
        {
            return $"{Type} | {ID} | {Text}";
        }
    }
}
