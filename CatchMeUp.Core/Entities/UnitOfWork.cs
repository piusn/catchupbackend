namespace CatchMeUp.Core.Entities;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Interest> InterestRepository { get; }
    IRepository<Following> FollowingsRepository { get; }
    IRepository<UserInterest> UserInterestRepository { get; }
    IRepository<Favourite> FavouriteRepository { get; }
    IRepository<Team> TeamRepository { get; }
    IRepository<TeamEvent> TeamEventRepository { get; }
    IRepository<UserAvailability> AvailabilityRepository { get; }

    Task Save();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly CatchMeUpDbContext _dbContext;

    public UnitOfWork(CatchMeUpDbContext dbContext,
        IRepository<User> userRepository,
        IRepository<Favourite> favouriteRepository,
        IRepository<Interest> interestRepository,
        IRepository<Following> followingsRepository,
        IRepository<UserInterest> userInterestRepository,
        IRepository<TeamEvent> teamEventRepository,
        IRepository<Team> teamRepository,
        IRepository<UserAvailability> availabilityRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        InterestRepository = interestRepository;
        FollowingsRepository = followingsRepository;
        UserInterestRepository = userInterestRepository;
        FavouriteRepository = favouriteRepository;
        TeamEventRepository = teamEventRepository;
        TeamRepository = teamRepository;
        AvailabilityRepository = availabilityRepository;
    }

    public IRepository<User> UserRepository { get; }
    public IRepository<Interest> InterestRepository { get; }
    public IRepository<Following> FollowingsRepository { get; }
    public IRepository<UserInterest> UserInterestRepository { get; }
    public IRepository<Favourite> FavouriteRepository { get; }
    public IRepository<Team> TeamRepository { get; }
    public IRepository<TeamEvent> TeamEventRepository { get; }
    public IRepository<UserAvailability> AvailabilityRepository { get; }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
}