using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoronaApi.Db;
using MediatR;

namespace CoronaApi.MediatR.Core.CommandHandlers
{
    public class BaseCreateCommandHandler<TCommand, TReturn, TDatabaseModel> : IRequestHandler<TCommand, TReturn>
        where TCommand : IRequest<TReturn> where TDatabaseModel : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BaseCreateCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
        }

        public async Task<TReturn> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var mapped = this._mapper.Map<TDatabaseModel>(request);
            var newEntity = await this._dbContext.Set<TDatabaseModel>()
                                      .AddAsync(mapped, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);
            return this._mapper.Map<TReturn>(newEntity);
        }
    }
}