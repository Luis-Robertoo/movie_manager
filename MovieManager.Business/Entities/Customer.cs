namespace MovieManager.BLL.Entities
{
    public class Customer : BaseEntity
    {

        public int Age { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual List<Rental> Rental { get; set; }
    }
}
