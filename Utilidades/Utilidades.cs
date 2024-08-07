using System.Security.Cryptography;
using System.Text;

namespace BibliotecaNexus.Utilidades
{
    public class Utilidades
    {
        public static string GetMD5(string str)
        {

            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sd = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sd.AppendFormat("{0:x2}", stream[i]);
            return sd.ToString();
        }
    }
}
