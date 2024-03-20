using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Common.Encryption;

public class EncryptionService : IEncryptionService
{
    private readonly EncryptionOptions _options;

    public EncryptionService(IOptions<EncryptionOptions> options)
    {
        _options = options.Value;
    }

    public byte[] Encrypt(string clearText)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

        using var encryptor = Aes.Create();
        encryptor.Key = _options.GetHahedKey();
        encryptor.IV = _options.GetHahedIV();

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
        }

        return ms.ToArray();
    }

    public string Decrypt(byte[] encryptedBytes)
    {
        using var encryptor = Aes.Create();
        encryptor.Key = _options.GetHahedKey();
        encryptor.IV = _options.GetHahedIV();

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(encryptedBytes, 0, encryptedBytes.Length);
            cs.Close();
        }

        return Encoding.Unicode.GetString(ms.ToArray());
    }
}
