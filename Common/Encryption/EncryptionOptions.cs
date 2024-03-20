using System.Security.Cryptography;
using System.Text;

namespace Common.Encryption;

public class EncryptionOptions
{
    public string Key { get; set; }
    public string IV { get; set; }

    public byte[] GetHahedIV()
    {
        using MD5 md5 = MD5.Create();
        return md5.ComputeHash(Encoding.UTF8.GetBytes(IV));
    }

    public byte[] GetHahedKey()
    {
        using SHA256 sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(Key));
    }
}
