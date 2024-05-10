public class BaseResult
{
    public BaseResult()
    {

    }

    public BaseResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public BaseResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccess = false;
    }

    public bool IsSuccess { get; set; }

    public string ErrorMessage { get; set; }
}
