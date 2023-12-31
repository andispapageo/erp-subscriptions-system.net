﻿using Application.Shared.Commands.DynamicFields.Main;
using Application.Shared.Events;
using Application.Shared.ViewModels;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using MediatR;

namespace Application.Shared.Handlers.DynamicFields.Main
{
    internal class DynamicFieldsCommandHandler : IRequestHandler<DynamicFieldsCommand, Result>
    {
        public DynamicFieldsCommandHandler(
            IUnitOfWork<TbView> uowTbView,
            IUnitOfWork<TbField> uowTbField)
        {
            UowTbView = uowTbView;
            UowTbField = uowTbField;
        }

        IUnitOfWork<TbView> UowTbView { get; }
        IUnitOfWork<TbField> UowTbField { get; }
        public async Task<Result> Handle(DynamicFieldsCommand request, CancellationToken cancellationToken)
        {
            Result result = new Result(true);
            var textFieldRequests = request.DynamicFieldsPostVm.AvailableTextFields?.Any() ?? false;
            if (textFieldRequests)
            {
                foreach (var view in request.DynamicFieldsPostVm.AvailableTextFields?.Where(x => !string.IsNullOrEmpty(x.FieldName)))
                {
                    var entity = new TbView()
                    {
                        TypeId = 1,
                        Name = view.FieldName
                    };

                    entity.AddDomainEvent(new TbCustomerFieldsEvent(entity));
                    var res = await UowTbView.Repository.InsertOrUpdate(entity);

                    if (res.Item2 >= 0)
                        await UowTbView.Repository.PublishDomain(entity);

                    entity.ClearDomainEvents();
                }
            }

            if (!string.IsNullOrEmpty(request.DynamicFieldsPostVm.DropdownKeyName) && (request.DynamicFieldsPostVm.DropdownValues?.Any() ?? false))
            {
                var entity = new TbView()
                {
                    TypeId = 2,
                    Name = request.DynamicFieldsPostVm.DropdownKeyName
                };

                entity.AddDomainEvent(new TbCustomerFieldsEvent(entity));
                var res = await UowTbView.Repository.InsertOrUpdate(entity);

                if (res.Item2 <= 0) return new Result(false, new string[] {"Dynamic fields addition, exception on inserting data"});

                await UowTbView.Repository.PublishDomain(entity);
                entity.ClearDomainEvents();

                foreach (var fields in request.DynamicFieldsPostVm.DropdownValues.Where(x => !string.IsNullOrEmpty(x.DropdownFieldName)))
                {
                    await UowTbField.Repository.InsertOrUpdate(new TbField()
                    {
                        Name = fields?.DropdownFieldName ?? string.Empty,
                        ViewId = entity.Id
                    });
                }
            }
            else
            {
                new Result(false, new string[] { "No Additions of dynamic fields occured" });
            }
            return result;
        }
    }
}
