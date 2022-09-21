namespace CatchMeUp.Core.Entities;

public interface IUnitOfWork
{
    IRepository<User> MemberRepository { get; }
    IRepository<Interest> InterestRepository { get; }

    IRepository<Following> FollowingsRepository { get; }
    IRepository<MemberInterest> MemberInterestRepository { get; }

    Task Save();
}

public class UnitOfWork :IUnitOfWork
{
    private readonly CatchMeUpDbContext _dbContext;

    public UnitOfWork(CatchMeUpDbContext dbContext, IRepository<User> memberRepository,
        IRepository<Interest> interestRepository, IRepository<Following> followingsRepository, IRepository<MemberInterest> memberInterestRepository)
    {
        _dbContext = dbContext;
        MemberRepository = memberRepository;
        InterestRepository = interestRepository;
        FollowingsRepository = followingsRepository;
        MemberInterestRepository = memberInterestRepository;
    }

    public IRepository<User> MemberRepository { get; }
    public IRepository<Interest> InterestRepository { get; }
    public IRepository<Following> FollowingsRepository { get; }
    public IRepository<MemberInterest> MemberInterestRepository { get; }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}