namespace Domain.Entities;

public class Prompt : BaseEntity
{
    public string Title { get; set; }
    public string  Description { get; set; }
    public string  Content { get; set; }
    public decimal Price { get; set; }
}