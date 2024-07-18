

namespace DomainModel.DTO.cellar
{
    public class UpdateCellarDto
    {
        public string CellarName { get; set; }
        public int NbRow { get; set; }
        public int NbStackRow { get; set; }
        public int CellarCategoryId { get; set; }
        public int CellarModelId { get; set; }
    }
}
