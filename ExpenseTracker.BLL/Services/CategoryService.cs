using AutoMapper;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Contracts;
using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;

namespace ExpenseTrackerLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        Mapper mapper = AutoMappers.InitializeAutoMapper();

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<(BLCategory category,string ErrorMsg)> Add(BLCategory category)
        {
            try
            {
                var response = await _categoryRepository.Add(mapper.Map<Category>(category));
                return (mapper.Map<BLCategory>(response.category), response.ErrorMsg);
            }
            catch(Exception ex) 
            {
                return (null,ex.Message);
            }
        }

        public async Task<(BLCategory category, string ErrorMsg)> Delete(Guid categoryId)
        {
            try
            {
                var response = await _categoryRepository.Delete(categoryId);
                return (mapper.Map<BLCategory>(response.category), response.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(List<BLCategory>?, string ErrorMsg)> Get(Guid UserId)
        {
            try
            {
                var categories = await _categoryRepository.Get(UserId);
                return (mapper.Map<List<BLCategory>>(categories.Item1), categories.ErrorMsg);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(BLCategory? category, string ErrorMsg)> Update(BLCategory? category)
        {
            try
            {
                var _category = await _categoryRepository.Update(mapper.Map<Category>(category));
                return (mapper.Map<BLCategory>(_category.category), "");
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
