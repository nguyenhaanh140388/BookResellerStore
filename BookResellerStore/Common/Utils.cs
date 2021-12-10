using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Serialization;
using static BookResellerStore.Common.Constants;

namespace BookResellerStore.Common
{
    public static class Utils
    {
        public static T DeserializeToObject<T>(string filepath) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyobject);
            }
        }

        public static bool IsInRight(this IPrincipal principal, string userRight)
        {
            var claimsPrincipal = (ClaimsPrincipal)principal;
            var userListRights = claimsPrincipal.Claims.FirstOrDefault(x => x.Type.Equals(PermissionType.Right))?.Value;
            if (string.IsNullOrWhiteSpace(userListRights))
            {
                return false;
            }

            var listRight = userListRights.Split(",")
            .Any(x => string.Compare(x, userRight) == 0);
            return listRight;
        }
    }
}
