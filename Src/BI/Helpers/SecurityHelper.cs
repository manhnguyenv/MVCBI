using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VASJ.BI.Helpers
{
  public static class SecurityHelper
  {
    #region http://tekeye.uk/visual_studio/encrypt-decrypt-c-sharp-string

    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "pemgail9uzpgzl88";

    // This constant is used to determine the keysize of the encryption algorithm
    private const int keysize = 256;

    private const string Key = "NguyenVietManh";

    public static string Encrypt(string Text)
    {
      if (string.IsNullOrWhiteSpace(Text)) return string.Empty;
      byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
      PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
      cryptoStream.FlushFinalBlock();
      byte[] Encrypted = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();
      return Convert.ToBase64String(Encrypted);
    }

    public static string Decrypt(string EncryptedText)
    {
      if (string.IsNullOrWhiteSpace(EncryptedText)) return string.Empty;
      byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
      byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
      PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
      CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] plainTextBytes = new byte[DeEncryptedText.Length];
      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }

    #endregion http://tekeye.uk/visual_studio/encrypt-decrypt-c-sharp-string

    /// <summary>
    /// Computes a SHA256 hash and return it as a byte array
    /// </summary>
    /// <param name="plainPassword">A plain text</param>
    /// <returns>A byte array containing a computed SHA256 hash</returns>
    public static byte[] ComputeHash(string plainText)
    {
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
      HashAlgorithm hash = new SHA256Managed();
      return hash.ComputeHash(plainTextBytes);
    }

    /// <summary>
    /// Generates a random salt
    /// </summary>
    /// <returns>A byte array containing a random salt</returns>
    public static byte[] GetRandomSalt()
    {
      int minSaltSize = 16;
      int maxSaltSize = 32;

      Random random = new Random();
      int saltSize = random.Next(minSaltSize, maxSaltSize);
      byte[] saltBytes = new byte[saltSize];
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      rng.GetNonZeroBytes(saltBytes);
      return saltBytes;
    }

    /// <summary>
    /// Encrypts a plain password and auto generates a random salt
    /// </summary>
    /// <param name="plainPassword">The plain password</param>
    /// <param name="generatedRandomSalt">When this method returns, contains the auto generated random salt</param>
    /// <returns>The encrypted password</returns>
    public static string EncryptPassword(string plainPassword, out string generatedRandomSalt)
    {
      byte[] passwordHash = ComputeHash(plainPassword);
      byte[] salt = GetRandomSalt();
      generatedRandomSalt = Convert.ToBase64String(salt);
      byte[] passwordHashAndSalt = new byte[passwordHash.Length + salt.Length];
      for (int i = 0; i < passwordHash.Length; i++)
        passwordHashAndSalt[i] = passwordHash[i];
      for (int i = 0; i < salt.Length; i++)
        passwordHashAndSalt[passwordHash.Length + i] = salt[i];

      return Convert.ToBase64String(passwordHashAndSalt);
    }

    /// <summary>
    /// Encrypts a plain password with an existing salt
    /// </summary>
    /// <param name="plainPassword">The plain password</param>
    /// <param name="existingSalt">The existing salt</param>
    /// <returns>The encrypted password</returns>
    public static string EncryptPassword(string plainPassword, string existingSalt)
    {
      if (existingSalt.IsNull())
        existingSalt = string.Empty;
      byte[] passwordHash = ComputeHash(plainPassword);
      byte[] salt = Convert.FromBase64String(existingSalt);
      byte[] passwordHashAndSalt = new byte[passwordHash.Length + salt.Length];
      for (int i = 0; i < passwordHash.Length; i++)
        passwordHashAndSalt[i] = passwordHash[i];
      for (int i = 0; i < salt.Length; i++)
        passwordHashAndSalt[passwordHash.Length + i] = salt[i];

      return Convert.ToBase64String(passwordHashAndSalt);
    }

    public static string EncodeBase64(string str)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    public static string DecodeBase64(string encodedStr)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));
    }
  }
}
