using Domain.Primitives;

namespace Domain.Entities.SellerInventory.SellerDeals
{
    public sealed class Deal : Entity
    {
        public Deal(string id, string name)
        : base(id)
        {
            Name = name;
        }

        private Deal()
        {
        }

        public string Name { get; private set; }

    }
}
