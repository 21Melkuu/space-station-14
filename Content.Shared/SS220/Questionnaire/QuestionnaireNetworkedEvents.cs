using Robust.Shared.Serialization;

namespace Content.Shared.SS220.Questionnaire;

[Serializable, NetSerializable]
public sealed class QuestionnaireResponse
{
    public Dictionary<int, List<string>> Answers = new();
}
