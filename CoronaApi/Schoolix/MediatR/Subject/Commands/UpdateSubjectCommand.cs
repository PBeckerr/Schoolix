using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Schoolix.Core;
using Schoolix.Db;
using Schoolix.Db.Types;
using Schoolix.Dtos;
using Schoolix.Mapping;
using Schoolix.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Schoolix.MediatR.Subject.Commands
{
    public class UpdateSubjectCommand : IRequest<SubjectDto>, IMapFrom<DbSubject>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
        {
            public UpdateSubjectCommandValidator(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
                IHttpContextAccessor contextAccessor)
            {
                RuleFor(c => c.Id)
                    .NotEmpty()
                    .MustAsync(async (id, cancellationToken) =>
                    {
                        var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                        var dbEntry = await dbContext.Subjects.SingleOrDefaultAsync(e => e.Id == id);
                        return dbEntry != null && user.SchoolId == dbEntry.SchoolId;
                    });
                RuleFor(c => c.Name)
                    .MaximumLength(50)
                    .NotEmpty();
            }
        }

        public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, SubjectDto>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IMapper _mapper;

            public UpdateSubjectCommandHandler(IMapper mapper, ApplicationDbContext dbContext)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }
            
            public async Task<SubjectDto> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
            {
                var existing =
                    await this._dbContext.Subjects.SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(DbSubject), request.Id);
                _mapper.Map(request, existing);

                _dbContext.Update(existing);

                return _mapper.Map<SubjectDto>(existing);
            }
        }
    }
}
