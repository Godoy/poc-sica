using Sica.Assets.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sica.Assets.Shared
{
    public static class BuilderErrorMessage
    {
        public static IEnumerable<ErrorMessage> Build(string message)
        {
            var structureMessage = message.Split('|').ToList();

            if (structureMessage.Count < 2)
                structureMessage.Insert(0, "000");

            var errorCode = structureMessage[0];
            var errorMessage = structureMessage[1];

            return new List<ErrorMessage> {
                new ErrorMessage
                {
                    Code = errorCode,
                    Message = errorMessage
                }
            };
        }
    }
}
