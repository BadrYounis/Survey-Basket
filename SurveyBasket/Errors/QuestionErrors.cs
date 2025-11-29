namespace SurveyBasket.Errors;
public static class QuestionErrors
{
    public static readonly Error QuestionNotFound =
        new("Question.QuestionNotFound", "No question Found With The Given ID", StatusCodes.Status404NotFound);

    public static readonly Error DuplicatedQuestionContent =
        new("Question.DuplicatedQuestionContent", "Another question with ths same content is already exists", StatusCodes.Status409Conflict);
}