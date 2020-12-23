using System.Collections.Generic;
using System.Linq;

namespace Framework.Core
{
    public abstract class SystemStringCodeBase
    {
        #region Fields

        protected List<SystemCodeModel<string>> CodeList;

        #endregion

        #region Public Methods

        public string GetDescription(string code)
        {
            return this.CodeList.Single(item => item.Value == code).Description;
        }

        public Dictionary<string, string> ToDictionary()
        {
            return this.CodeList.ToDictionary(item => item.Value, item => item.Description);
        }

        public List<SystemCodeModel<string>> GetCodeList()
        {
            return this.CodeList;
        }

        public SystemCodeModel<string> GetItem(string code)
        {
            return this.CodeList.Single(item => item.Value == code);
        }

        #endregion
    }
}
