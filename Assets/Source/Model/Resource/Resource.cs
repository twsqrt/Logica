using Model.ResourceLogic.AmountLogic;

namespace Model.ResourceLogic
{
    public class Resource
    {
        private readonly IAmount _amount;

        public Resource(IAmount amount)
        {
            _amount = amount;
        }
    }
}