using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PasswordCracker
{
    /// <summary>
    /// A list of md5 hashed passwords is contained within the passwords_hashed.txt file.  Your task
    /// is to crack each of the passwords.  Your input will be an array of strings obtained by reading
    /// in each line of the text file and your output will be validated by passing an array of the
    /// cracked passwords to the Validator.ValidateResults() method.  This method will compute a SHA256
    /// hash of each of your solved passwords and compare it against a list of known hashes for each
    /// password.  If they match, it means that you correctly cracked the password.  Be warned that the
    /// test is ALL or NOTHING.. so one wrong password means the test fails.
    /// </summary>
    class Program
    {
        public static string md5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        
        static void Main(string[] args)
        {
            string[] hashedPasswords = File.ReadAllLines("passwords_hashed.txt");

            Console.WriteLine("MD5 Password Cracker v1.0");
            
            foreach (var pass in hashedPasswords)
            {
                Console.WriteLine(pass);
            }
            Dictionary<int, String> letters = new Dictionary<int, String>();
            Dictionary<int, List<String>> password = new Dictionary<int, List<String>>();

            letters.Add(1, "a");
            letters.Add(2, "b");
            letters.Add(3, "c");
            letters.Add(4, "d");
            letters.Add(5, "e");
            letters.Add(6, "f");
            letters.Add(7, "g");
            letters.Add(8, "h");
            letters.Add(9, "i");
            letters.Add(10, "j");
            letters.Add(11, "k");
            letters.Add(12, "l");
            letters.Add(13, "m");
            letters.Add(14, "n");
            letters.Add(15, "o");
            letters.Add(16, "p");
            letters.Add(17, "q");
            letters.Add(18, "r");
            letters.Add(19, "s");
            letters.Add(20, "t");
            letters.Add(21, "u");
            letters.Add(22, "v");
            letters.Add(23, "w");
            letters.Add(24, "x");
            letters.Add(25, "y");
            letters.Add(26, "z");

            String begin;
            int counter = 0;

            List<String> nums = new List<String>();
            List<String> unhashed = new List<String>();

            for (int i = 1; i <= 26; i++)
            {
                for (int j = 1; j <= 26; j++)
                {

                    for (int k = 1; k <= 26; k++)
                    {

                        for (int l = 1; l <= 26; l++)
                        {

                            for (int p = 1; p <= 26; p++)
                            {

                                begin = letters[i] + letters[j] + letters[k] + letters[l] + letters[p];

                                nums.Add(md5(begin));
                                unhashed.Add(begin);

                                counter++;
                            }

                        }

                    }

                }

                
                Console.WriteLine(nums);
            }

            Console.Write(counter);

            for (int i = 0; i < hashedPasswords.Length; i++)
            {
                for (int j = 0; j < nums.Count; j++)
                {
                    if (hashedPasswords[i] == nums[j])
                    {
                        Console.WriteLine("hashed: " + hashedPasswords[i] + "generated: " + nums[j]);
                        hashedPasswords[i] = unhashed[j];
                        Console.WriteLine(i);
                        break;
                    }
                }
            }
           


            bool passwordsValidated = Validator.ValidateResults(hashedPasswords);
            
            Console.WriteLine($"\nPasswords successfully cracked: {passwordsValidated}");
        }
    }
}