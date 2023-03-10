﻿using WebApplication1.Models.entities;
using WebApplication1.Models.ModelPattern;

namespace WebApplication1.Models.ModelPattern
{
    public sealed class FacadeMaker
    {
        private static FacadeMaker _instance = null;
        private CategoryMgr _categoryMgr;

        private FacadeMaker()
        {
            _categoryMgr = new CategoryMgr();
        }

        public static FacadeMaker Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FacadeMaker();
                }
                return _instance;
            }
        }

        //Category
        public Category CreateCategory(Category category)
        {
            _categoryMgr.Create(category);
            return category;
        }
        public Category UpdateCategory(int id, Category category)
        {
            _categoryMgr.Update(id, category);
            return category;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryMgr.GetAll();
        }
        public Category GetCategoryById(int id)
        {
            return _categoryMgr.GetById(id);
        }
    }
}
