using System;
using Microsoft.Extensions.Logging;

namespace AuthServiceIN6BM.Application.Extensions;


/*en otro archivo puedo segir trabajando en otra clase y solo se une despues por medio del patial*/
public static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 1001,
        /*metodo e nivel de solamente informacion*/
        Level = LogLevel.Information,
        Message = "User {Username} registered succesfuly"
    )]
    public static partial void LogUserRegistered(this ILogger logger, string username);

    [LoggerMessage(
        EventId = 1002,
        /*metodo e nivel de solamente informacion*/
        Level = LogLevel.Information,
        Message = "User login succeeded"
    )]
    public static partial void LogUserLoggedIn(this ILogger logger);

    [LoggerMessage(
        EventId = 1003,
        /*metodo e nivel de solamente informacion*/
        Level = LogLevel.Warning,
        Message = "Failed login attempt"
    )]
    public static partial void LogFailedLoginAttempt(this ILogger logger);

    [LoggerMessage(
           EventId = 1004,
           /*metodo e nivel de solamente informacion*/
           Level = LogLevel.Warning,
           Message = "Registration rejected: email alredy exists"
       )]
    public static partial void LogREgistrartionWhitExistingEmail(this ILogger logger);

    [LoggerMessage(
           EventId = 1005,
           /*metodo e nivel de solamente informacion*/
           Level = LogLevel.Warning,
           Message = "Registration rejected: username alredy exists"
       )]
    public static partial void LogREgistrartionWhitExistingUsername(this ILogger logger);
    [LoggerMessage(
    EventId = 1006,
    /*metodo e nivel de solamente informacion*/
    Level = LogLevel.Error,
    Message = "Error uploadinf profile image"
)]
    public static partial void LogImageUploadError(this ILogger logger);


}