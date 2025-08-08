namespace SuperWarehouseBackend.WebApi.General;

public record ResultOrError<TResult, TError>(TResult Result, TError Error, bool IsSuccess)
{
    public static ResultOrError<TResult, TError> FromResult(TResult result)
    {
        return new ResultOrError<TResult, TError>(result, default!, true);
    }
    
    public static ResultOrError<TResult, TError> FromError(TError error)
    {
        return new ResultOrError<TResult, TError>(default!, error, false);
    }
}