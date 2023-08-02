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
        private readonly IDefaultCategoryRepository _defaultCategoryRepository;
        Mapper mapper = AutoMappers.InitializeAutoMapper();

        public CategoryService(ICategoryRepository categoryRepository,IDefaultCategoryRepository defaultCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _defaultCategoryRepository = defaultCategoryRepository;
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
                var categoriesResponse = await _categoryRepository.Get(UserId);
                var categories = mapper.Map<List<BLCategory>>(categoriesResponse.Item1);
                var defaultCategories = await _defaultCategoryRepository.Get();
                for(var i = 0; i < defaultCategories.Count; i++)
                {
                    var category = new BLCategory()
                    {
                        CategoryId = defaultCategories[i].CategoryId,
                        Name = defaultCategories[i].Name,
                        CreatedAt = defaultCategories[i].CreatedAt,
                        UpdatedAt = defaultCategories[i].UpdatedAt,
                        IsActive = defaultCategories[i].IsActive,
                    };
                    categories.Add(category);
                }
                return (categories, categoriesResponse.ErrorMsg);
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
