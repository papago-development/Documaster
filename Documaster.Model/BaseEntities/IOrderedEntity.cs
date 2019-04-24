using System;
namespace Documaster.Model.BaseEntities
{
    public interface IOrderedEntity
    {
        int GetOrder();
        void SetOrder(int value);
    }
}
