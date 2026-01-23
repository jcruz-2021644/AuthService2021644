namespace AuthServiceIN6BM.Domain.Constants;

public static class RoleConstants
{
    /*cuandop hacemos la verificacion solo mandamos a llamr a la constante*/
    public const string ADMIN_ROLE = "ADMIN_ROLE";
    public const string USER_ROLE = "USER_ROLE";

/*lista para mostrar los tipos de roles que hay en el sistema*/
    public static readonly string[] AllowedRole = [ADMIN_ROLE, USER_ROLE];
    }