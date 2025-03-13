namespace Budgetify.Enums;

public enum EnumErrorCode
{
    Success = 0,

    // General Errors
    UnknownError = 1000,
    InvalidRequest = 1001,
    Unauthorized = 1002,
    Forbidden = 1003,
    NotFound = 1004,
    Conflict = 1005,
    InternalServerError = 1006,

    // Validation Errors
    ValidationFailed = 2000,
    MissingRequiredField = 2001,
    InvalidFormat = 2002,
    ExceedsMaxLength = 2003,

    // Authentication & Authorization
    TokenExpired = 3000,
    InvalidToken = 3001,
    UserNotFound = 3002,
    PasswordIncorrect = 3003,

    // Database Errors
    DatabaseConnectionFailed = 4000,
    RecordNotFound = 4001,
    DuplicateRecord = 4002,

    // Business Logic Errors
    InsufficientBalance = 5000,
    OperationNotAllowed = 5001,
    ExceededLimit = 5002
}