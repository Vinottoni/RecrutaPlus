﻿using FluentValidation;
using RecrutaPlus.Domain.Entities;

namespace RecrutaPlus.Domain.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator() 
        { 
        
        }
    }
}