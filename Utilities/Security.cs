namespace Rpg_Restapi.Utilities {
  public class Security {

    /// <summary>
    /// Create a hash password from Hmac of System.SecurityCryptography
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    public static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
      using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
      }
    }

    /// <summary>
    /// Verify passsword by hast and salt given
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    /// <returns></returns>
    public static bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
      using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
        var computerHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
        for (int i = 0; i < computerHash.Length; i++) {
          if (computerHash[i] != passwordHash[i]) {
            return false;
          }
        }
        return true;
      }
    }

  }
}