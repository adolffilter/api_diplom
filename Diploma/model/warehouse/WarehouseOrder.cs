using Diploma.model.order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.model.warehouse
{
    [Table(name: "WarehouseOrders")]
    public class WarehouseOrder : Order
    {
        public WarehouseState State { get; set; }
        public override bool Warehouse => true;
    }

    public enum WarehouseState
    {
        ON_ENTERPRISE,
        ON_WAREHOUSE
    }
}
