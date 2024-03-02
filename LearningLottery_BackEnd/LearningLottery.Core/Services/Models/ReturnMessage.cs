namespace LearningLottery.Core.Services.Models; 

public class ReturnMessage<T> {
    public ReturnMessage(int statusCode, string? message = null) {
        StatusCode = statusCode;
        Message = message;
    }

    public int StatusCode { get; }
    public string? Message { get; }
    public T? Data { get; init; } = default(T);

    public static ReturnMessage<T> GenerateReturnMessageSuccess<T>(T? data = default) {
        return new ReturnMessage<T>(
            statusCode: 200,
            message: "success"
        ) {
            Data = data
        };
    }
    
    public static ReturnMessage<T> GenerateReturnMessageSuccess<T>(
        int statusCode = 200, string message = "success", T? data = default
        ) {
        return new ReturnMessage<T>(
            statusCode: statusCode,
            message: message
        ) {
            Data = data
        };
    }
    
    public static ReturnMessage<T> GenerateReturnMessageFailed<T>(
        int statusCode = 400, string message = "error", T? data = default) {
        return new ReturnMessage<T>(
            statusCode: statusCode,
            message: message
        ) {
            Data = data
        };
    }
}