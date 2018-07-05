using System.Linq;


namespace PasswordDll
{
    public static class Encrypter
    {
        public static string Encrypt(string str, int key = 1) => new string(str.Select(c => (char)(c + key)).ToArray());

        public static string Deencrypt(string str, int key = 1) => new string(str.Select(c => (char)(c - key)).ToArray());

    }
}
