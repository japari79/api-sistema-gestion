namespace api_sistema_gestion.Models
{
    public class ProductoVenta
    {
        public long Id { get; set; }
        public long IdProducto { get; set; }
        public int Stock { get; set; }
        public long IdVenta { get; set; }

        public ProductoVenta(long id, long idProducto, int stock, long idVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;
        }
    }
}
