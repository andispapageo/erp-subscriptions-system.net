﻿using Application.Shared.ViewModels.DynamicFields;
using System.ComponentModel.DataAnnotations;

namespace Application.Shared.ViewModels
{
    public class InheritorPresenterVm
    {
        public List<OrderVm>? OrderVms { get; set; }
        [Required]
        public List<CustomerFieldsVm>? CustomerFields { get; set; }
    }
}
