using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Security.Cryptography;
using System.Text;

namespace ZOO_Accounting.Model.Entities
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public User(){}
        public User(string login, string password)
        {
            Login = login;
            PasswordHash = GetMd5Hash(MD5.Create(), password);
        }

        private string GetMd5Hash(MD5 md5Hash, string input)     //Метод позволяет получить хеш-сумму из входящих данных
        {

            // Конвертируем поступившую строку в массив байтов и вычисляем хеш.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Создаем навый экземпляр Stringbuilder, чтобы аккумулировать байты
            // и собрать из них строку.
            StringBuilder sBuilder = new StringBuilder();

            // Генерируем шестнадцатеричное значение каждого байта 
            // и поочереди добавляем в строку.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Возвращаем хеш-сумму в виде строки.
            return sBuilder.ToString();
        }
    }
}
