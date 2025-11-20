namespace serviceMicroservice.DTOs;

public class CreateServiceDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
public class ValidationErrorResponse
{
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}

public class SuccessResponse
{
    public string Message { get; set; }
    public int Id { get; set; }
}