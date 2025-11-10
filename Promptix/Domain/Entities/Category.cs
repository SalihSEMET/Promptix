namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = "";
    //Navigation Properties
    public ICollection<PromptCategory> PromptCategories { get; set; }
}