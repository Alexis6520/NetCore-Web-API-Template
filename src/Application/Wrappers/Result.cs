using System.Net;
using System.Text.Json.Serialization;

namespace Application.Wrappers
{
    public class Result
    {
        [JsonIgnore]
        public bool Succeeded { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Errors { get; set; }

        public static Result Success(HttpStatusCode statusCode = HttpStatusCode.NoContent)
        {
            if (statusCode >= HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("Código inválido", nameof(statusCode));
            }

            return new()
            {
                Succeeded = true,
                StatusCode = statusCode
            };
        }

        public static Result Fail(HttpStatusCode statusCode, params string[] errors)
        {
            if (statusCode < HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("Código inválido", nameof(statusCode));
            }

            return new()
            {
                Succeeded = false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }

    public class Result<TValue> : Result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TValue Value { get; set; }

        public static Result<TValue> Success(TValue value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (statusCode >= HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("Código inválido", nameof(statusCode));
            }

            return new()
            {
                Succeeded = true,
                StatusCode = statusCode,
                Value = value
            };
        }

        public static new Result<TValue> Fail(HttpStatusCode statusCode, params string[] errors)
        {
            if (statusCode < HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("Código inválido", nameof(statusCode));
            }

            return new()
            {
                Succeeded = false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
