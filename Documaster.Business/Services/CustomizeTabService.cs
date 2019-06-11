using System.Collections.Generic;
using Documaster.Model.Entities;
using System.Linq;
using NLog;

namespace Documaster.Business.Services
{
    public class CustomizeTabService: ICustomizeTabService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<CustomizeTab> _customizeTabRepository;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CustomizeTabService(IUnitOfWork unitOfWork,
                            IGenericRepository<CustomizeTab> customizeTabRepository)

        {
            _unitOfWork = unitOfWork;
            _customizeTabRepository = customizeTabRepository;
        }

        public List<CustomizeTab> GetCustomizeTabs()
        {
            return _customizeTabRepository.GetAll().ToList();
        }

        public CustomizeTab Create(CustomizeTab customizeTab)
        {

            var createdCustomizeTab = _customizeTabRepository.Create(customizeTab);

            _unitOfWork.SaveChanges();

            return createdCustomizeTab;
        }

        public CustomizeTab GetCustomizeTabById(int id)
        {
            return _customizeTabRepository.Get(id);
        }

        public bool Edit(CustomizeTab customizeTab)
        {
            var editedCustomizeTab = _customizeTabRepository.Update(customizeTab, new List<string> { "Name", "Type" });
            _unitOfWork.SaveChanges();
            return editedCustomizeTab;
        }

        public bool Delete(CustomizeTab customizeTab)
        {
            var deletedCustomizeTab = _customizeTabRepository.Delete(customizeTab.Id);
            _unitOfWork.SaveChanges();
            return deletedCustomizeTab;
        }

        public bool DoesNumberExist(CustomizeTab customizeTab)
        {
            var customizeTabs = _customizeTabRepository.Get(x => x.Number == customizeTab.Number && x.Id != customizeTab.Id);
            return customizeTabs.Any();
        }

        public void SaveOrder(IList<int> sortedList, string entityName)
        {
            foreach(var id in sortedList)
            {
                var customizeTab = _customizeTabRepository.Get(id);
                _logger.Info($"Customize tab: {customizeTab.Name}");
                customizeTab.Number = sortedList.IndexOf(id)+1;
                _customizeTabRepository.Update(customizeTab, new List<string> { "Number" });
               

            }
            _unitOfWork.SaveChanges();
        }
    }
}
