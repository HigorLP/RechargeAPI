namespace Recharge.Application.Services;
public class ResultService {
    public bool isSucess { get; set; }
    public string Message { get; set; }
    public ICollection<ErrorValidation> Errors { get; set; }

    public static ResultService RequestError(string message, FluentValidation.Results.ValidationResult validationResult) {
        return new ResultService {
            isSucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList()
        };
    }

    public static ResultService<T> RequestError<T>(string message, FluentValidation.Results.ValidationResult validationResult) {
        return new ResultService<T> {
            isSucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation { Field = x.PropertyName, Message = x.ErrorMessage }).ToList()
        };
    }

    public static ResultService Fail(string message) => new ResultService { isSucess = false, Message = message };
    public static ResultService<T> Fail<T>(string message) => new ResultService<T> { isSucess = false, Message = message };

    public static ResultService Ok(string message) => new ResultService { isSucess = true, Message = message };
    public static ResultService<T> Ok<T>(T data) => new ResultService<T> { isSucess = true, Data = data };
}

public class ResultService<T> : ResultService {
    public T Data { get; set; }
}