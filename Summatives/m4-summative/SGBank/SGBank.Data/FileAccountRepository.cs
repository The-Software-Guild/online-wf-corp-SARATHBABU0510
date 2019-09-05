using SGBank.Models;
using SGBank.Models.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private static string path = "Accounts.txt";
        private static List<Account> _accountList = new List<Account>();
        private static string header = null;

        public Account LoadAccount(string AccountNumber)
        {
            FileAccountRepository.BuildAccountListFromFile();
            Account _account = new Account();
            _account = _accountList.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            return _account;
        }
        public void SaveAccount(Account account)
        {
            var index = _accountList.FindIndex(a => a.AccountNumber == account.AccountNumber);
            _accountList[index] = account;
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(header);
                string type = null;
                char comma = ',';
                foreach (var ta in _accountList)
                {
                    switch (ta.Type)
                    {
                        case AccountType.Free:
                             type = "F";
                            break;
                        case AccountType.Basic:
                            type = "B";
                            break;
                        case AccountType.Premium:
                            type = "P";
                            break;
                    }
                    writer.WriteLine(ta.AccountNumber + comma + ta.Name + comma + ta.Balance + comma + type);
                }
            }
        }
        public static void BuildAccountListFromFile()
        {
            _accountList.Clear();
            if (File.Exists(path))
            {
                List<string> rows = new List<string>();
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        rows.Add(line);
                    }
                }
                if (rows.Count > 0)
                {
                    header = rows[0];
                    for (int i = 1; i < rows.Count; i++)
                    {
                        string[] columns = rows[i].Split(',');
                        Account _account = new Account();
                        _account.AccountNumber = columns[0];
                        _account.Name = columns[1];
                        _account.Balance = Convert.ToDecimal(columns[2]);
                        string _tempaccounttype = columns[3];
                        switch (_tempaccounttype)
                        {
                            case "F":
                                _account.Type = AccountType.Free;
                                break;
                            case "B":
                                _account.Type = AccountType.Basic;
                                break;
                            case "P":
                                _account.Type = AccountType.Premium;
                                break;
                            default:
                                throw new Exception("Account type is not supported in the FileRepository! Contact IT!");
                        }
                        _accountList.Add(_account);
                    }
                }
            }
            else
            {
                Console.WriteLine("Could not find the file at {0}", path);
            }
        }        
    }
}
