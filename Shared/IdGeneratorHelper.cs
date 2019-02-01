using MongoDB.Bson;

namespace Shared
{
    public static class IdGeneratorHelper
    {
        public static string IdGenerator()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
