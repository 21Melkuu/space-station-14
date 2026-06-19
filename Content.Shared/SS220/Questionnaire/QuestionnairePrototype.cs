using Robust.Shared.Prototypes;

namespace Content.Shared.SS220.Questionnaire;

/// <summary>
///
/// </summary>
[Prototype]
public sealed partial class QuestionnairePrototype : IPrototype
{
    /// <inheritdoc/>
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public string Title  = string.Empty;

    [DataField]
    public List<QuestionData> Questions = new();
}

[DataDefinition]
public sealed partial class QuestionData
{
    [DataField]
    public string Text;

    [DataField]
    public QuestionType Type;

    [DataField]
    public List<string> Options;
}

public enum QuestionType : byte
{
    Single,
    Multiple,
    FreeText
}
