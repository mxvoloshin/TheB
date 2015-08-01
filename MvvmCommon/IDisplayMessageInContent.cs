namespace MvvmCommon
{
    public interface IDisplayMessageInContent
    {
        MessageViewModel ErrorViewModel { get; set; }
        QuestionViewModel QuestionViewModel { get; set; }
    }
}