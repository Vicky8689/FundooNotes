using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusineesLayer.Services
{
    public class HashPasswordBL
    {

        public static string HashPsaaword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //combin salt and hash
            byte[] hashBytes = new byte[16 + 20];
            //Array.Cpoy(sourceArray,sourceIndex,DestinationArray,DestinationIndexBegin,lengthOfElementToStore);
            Array.Copy(salt, 0, hashBytes, 0, 16 );
            Array.Copy(hash, 0, hashBytes,16, 20);

            return Convert.ToBase64String( hashBytes );


        }
        public static bool VerifyHash(string enterPassword ,string storedPassword)
        {
            byte[] hashByts = Convert.FromBase64String( storedPassword );

            byte[] salt = new byte[16];
            Array.Copy(hashByts,0,salt,0,16);

            var pbkdf2 = new Rfc2898DeriveBytes(enterPassword,salt,10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] newGenretedHash = new byte[16 + 20];
            Array.Copy(salt,0,newGenretedHash,0,16);
            Array.Copy(hash,0,newGenretedHash,16,20);

            string enterPasswordHash = Convert.ToBase64String(newGenretedHash);
            if(enterPasswordHash.Equals( storedPassword))
            {

                return true;
            }
            return false;



        }
    }
}
