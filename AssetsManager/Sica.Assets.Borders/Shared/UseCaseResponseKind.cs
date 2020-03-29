namespace Sica.Assets.Borders.Shared
{
    public enum UseCaseResponseKind
    {
        Created = 201,
        OK = 200,
        InternalServerError = 500,
        BadGateway = 502,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401,
        Processing = 102,
        Forbidden = 403,
        Unavailable = 503
    }
}
