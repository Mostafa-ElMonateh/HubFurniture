using System.ComponentModel.DataAnnotations;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdminPanel.Validations
{
    public class CheckExistingNameAttribute<T> : ValidationAttribute where T : BaseEntityWithNames
    {

        private readonly Type _dbContextType;
        private readonly string _column;

        public CheckExistingNameAttribute(Type dbContextType, string column)
        {
            _dbContextType = dbContextType;
            _column = column;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var columnName = (string) value;

            StoreContext dbContext = (StoreContext)validationContext.GetService(_dbContextType);

            var dbSet = dbContext?.Set<T>();

            List<T?> entity = null;

            if (_column == "NameEnglish")
            {
                entity = dbSet?.Where(e => e.NameEnglish == columnName).AsNoTracking().ToList();
            }else if (_column == "NameArabic")
            {
                entity = dbSet?.Where(e => e.NameArabic == columnName).AsNoTracking().ToList();
            }

            if (entity == null || entity.Count() <= 1)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"This Name already exists.");
            }
            
        }
    }
}
