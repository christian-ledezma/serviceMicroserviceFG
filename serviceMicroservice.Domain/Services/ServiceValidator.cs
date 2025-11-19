using serviceMicroservice.Domain.Entities;

namespace serviceMicroservice.Domain.Services;

public class ServiceValidator : IValidator<Service>
{
    public Result Validate(Service entity)
    {
        var errors = new List<string>();

        var name = entity.Name?.Trim() ?? string.Empty;
        var type = entity.Type?.Trim() ?? string.Empty;
        var description = entity.Description?.Trim() ?? string.Empty;

        ValidateName(name, errors);
        ValidateType(type, errors);
        ValidatePrice(entity.Price, errors);
        ValidateDescription(description, errors);

        return errors.Count == 0
            ? Result.Success()
            : Result.Failure(errors);
    }


    private void ValidateName(string name, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("El nombre del servicio es requerido");
            return;
        }

        if (name.Length < 3)
        {
            errors.Add("El nombre del servicio debe tener al menos 3 caracteres");
        }

        if (name.Length > 100)
        {
            errors.Add("El nombre del servicio no puede superar los 100 caracteres");
        }

        if (!char.IsLetter(name[0]))
        {
            errors.Add("El nombre del servicio debe comenzar con una letra");
        }

        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|', '@', '$', '%', '&', '*', '=', '+' };
        if (name.Any(c => prohibitedCharacters.Contains(c)))
        {
            errors.Add("El nombre del servicio contiene caracteres no permitidos");
        }
    }
    
    private void ValidateType(string type, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            errors.Add("El tipo de servicio es requerido");
            return;
        }

        if (type.Length < 3)
        {
            errors.Add("El tipo de servicio debe tener al menos 3 caracteres");
        }

        if (type.Length > 50)
        {
            errors.Add("El tipo de servicio no puede superar los 50 caracteres");
        }

        if (!char.IsLetter(type[0]))
        {
            errors.Add("El tipo de servicio debe comenzar con una letra");
        }

        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|', '@', '#', '$', '%', '&', '*', '=', '+' };
        if (type.Any(c => prohibitedCharacters.Contains(c)))
        {
            errors.Add("El tipo de servicio contiene caracteres no permitidos");
        }
    }
    
    private void ValidatePrice(decimal? price, List<string> errors)
    {
        if (!price.HasValue)
        {
            errors.Add("El precio es requerido");
            return;
        }
        
        if (price.Value <= 0)
        {
            errors.Add("El precio debe ser mayor que cero");
            return;
        }

        if (decimal.Round(price.Value, 2) != price.Value)
        {
            errors.Add("El precio solo puede tener hasta dos decimales");
        }
    }


    private void ValidateDescription(string description, List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            errors.Add("La descripción es requerida");
            return;
        }

        if (description.Length < 5)
        {
            errors.Add("La descripción debe tener al menos 5 caracteres");
        }

        if (description.Length > 500)
        {
            errors.Add("La descripción no puede superar los 500 caracteres");
        }

        var prohibitedCharacters = new[] { '<', '>', '/', '\\', '|' };
        if (description.Any(c => prohibitedCharacters.Contains(c)))
        {
            errors.Add("La descripción contiene caracteres no permitidos: < > / \\ |");
        }
    }
}