namespace Common.Encryption;

public interface IEncryptionService
{
    public byte[] Encrypt(string rawText);
    public string Decrypt(byte[] rawText);
}
