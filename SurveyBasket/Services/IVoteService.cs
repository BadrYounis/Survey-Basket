using SurveyBasket.Contracts.Votes;

namespace SurveyBasket.Services;
public interface IVoteService
{
    //Only one method to add a new vote inside database
    Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default);
}