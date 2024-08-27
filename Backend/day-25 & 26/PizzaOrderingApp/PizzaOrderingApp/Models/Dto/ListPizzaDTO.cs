namespace PizzaOrderingApp.Models.Dto
{
    public class ListPizzaDTO: Pizza
    {

        public int PizzaId { get; set; }

        public string PizzaName { get; set; }

        public string PizzaFlavour { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public bool IsVegetarian { get; set; }

        public double Stock { get; set; }

    }
}
