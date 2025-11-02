using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SmartRigWeb
{
    public abstract class Repository 
    {
        protected OleDbConext dbContext;
        protected ModelsFactory modelsFactory;

        public Repository(OleDbConext dbContext, ModelsFactory modelsFactory)
        {
            this.dbContext = dbContext;
            this.modelsFactory = modelsFactory;
        }
    }
}
