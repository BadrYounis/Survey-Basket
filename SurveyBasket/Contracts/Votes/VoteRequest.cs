namespace SurveyBasket.Contracts.Votes;
public record VoteRequest(
    //PollId will not be received into body but will received into routes
    IEnumerable<VoteAnswerRequest> Answers
);