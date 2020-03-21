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
        protected readonly ApplicationDbContext DbContext;
        protected readonly IMapper Mapper;

        public BaseCreateCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
        {
            this.Mapper = mapper;
            this.DbContext = dbContext;
        }

        public virtual Task CustomCreateLogicAsync(TCommand request, TDatabaseModel dbModel)
        {
            return Task.CompletedTask;
        }

        public async Task<TReturn> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var mapped = this.Mapper.Map<TDatabaseModel>(request);
            await CustomCreateLogicAsync(request, mapped);
            var newEntity = await this.DbContext.Set<TDatabaseModel>()
                                      .AddAsync(mapped, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);
            return this.Mapper.Map<TReturn>(newEntity);
        }
    }
}