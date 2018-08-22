using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BI.Infrastructure
{
  public class JsonUtils
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string SerializeObject(object obj)
    {
      if (obj == null) return string.Empty;

      DefaultContractResolver contractResolver = new DefaultContractResolver
      {
        NamingStrategy = new CamelCaseNamingStrategy()
      };

      return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
      {
        ContractResolver = contractResolver,
        Formatting = Formatting.None
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stringify"></param>
    /// <returns></returns>
    public static T DeserializeObject<T>(string stringify) where T : class
    {
      var result = JsonConvert.DeserializeObject<T>(stringify);
      return result;
    }
  }
}
