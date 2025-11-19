namespace serviceMicroservice.DTOs;

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