namespace CatchMeUp.Core.Entities;

public interface IUnitOfWork
{
    IRepository<Member> MemberRepository { get; }
    IRepository<Interest> InterestRepository { get; }

    IRepository<Following> FollowingsRepository { get; }
    IRepository<MemberInterest> MemberInterestRepository { get; }
    IRepository<Favourite> FavouriteRepository { get; }

    Task Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CatchMeUpDbContext _dbContext;

    public UnitOfWork(CatchMeUpDbContext dbContext, IRepository<Member> memberRepository,
        IRepository<Interest> interestRepository, IRepository<Following> followingsRepository, IRepository<MemberInterest> memberInterestRepository, IRepository<Favourite> favouriteRepository)
    {
        _dbContext = dbContext;
        MemberRepository = memberRepository;
        InterestRepository = interestRepository;
        FollowingsRepository = followingsRepository;
        MemberInterestRepository = memberInterestRepository;
        FavouriteRepository = favouriteRepository;
    }

    public IRepository<Member> MemberRepository { get; }
    public IRepository<Interest> InterestRepository { get; }
    public IRepository<Following> FollowingsRepository { get; }
    public IRepository<MemberInterest> MemberInterestRepository { get; }
    public IRepository<Favourite> FavouriteRepository { get; }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}