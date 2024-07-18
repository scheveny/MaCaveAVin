namespace DomainModel.DTOs
{
    public class CellarCategoryDto
    {
        public int CellarCategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CreateCellarCategoryDto
    {
        public string CategoryName { get; set; }
    }

    public class UpdateCellarCategoryDto
    {
        public string CategoryName { get; set; }
    }
}
