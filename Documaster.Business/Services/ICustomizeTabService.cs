using System;
using System.Collections.Generic;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface ICustomizeTabService
    {
        List<CustomizeTab> GetCustomizeTabs();
        CustomizeTab Create(CustomizeTab customizeTab);
        CustomizeTab GetCustomizeTabById(int id);


        bool Edit(CustomizeTab customizeTab);
        bool Delete(CustomizeTab customizeTab);
        bool DoesNumberExist(CustomizeTab customizeTab);

        void SaveOrder(IList<int> sortedList, string entityName);
    }
}
