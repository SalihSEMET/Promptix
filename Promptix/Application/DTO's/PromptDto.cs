namespace Application.DTO_s;

public class PromptDto
{
    //DTO(Data Transfer Object): We may not want to retrieve all the data in the database and show it to the user, or sometimes we may combine a table with another table and only want to show the data we want from both tables. In such cases, we need filtered and cleaned classes, we call these DTOs.
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
}