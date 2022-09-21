namespace CatchMeUp.Core.Entities;

public interface IUnitOfWork
{
    IRepository<User> MemberRepository { get; }
    IRepository<Interest> InterestRepository { get; }

    IRepository<Following> FollowingsRepository { get; }
    IRepository<MemberInterest> MemberInterestRepository { get; }
    IRepository<Team> TeamRepository { get; }
    IRepository<TeamEvent> TeamEventRepository { get; }

    Task Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CatchMeUpDbContext _dbContext;

    public UnitOfWork(CatchMeUpDbContext dbContext, IRepository<User> memberRepository,
        IRepository<Interest> interestRepository, IRepository<Following> followingsRepository, IRepository<MemberInterest> memberInterestRepository, IRepository<TeamEvent> teamEventRepository, IRepository<Team> teamRepository)
    {
        _dbContext = dbContext;
        MemberRepository = memberRepository;
        InterestRepository = interestRepository;
        FollowingsRepository = followingsRepository;
        MemberInterestRepository = memberInterestRepository;
        TeamEventRepository = teamEventRepository;
        TeamRepository = teamRepository;
    }

    public IRepository<User> MemberRepository { get; }
    public IRepository<Interest> InterestRepository { get; }
    public IRepository<Following> FollowingsRepository { get; }
    public IRepository<MemberInterest> MemberInterestRepository { get; }
    public IRepository<Team> TeamRepository { get; }
    public IRepository<TeamEvent> TeamEventRepository { get; }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}