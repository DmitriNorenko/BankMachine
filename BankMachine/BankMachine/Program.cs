using System;

namespace BankMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("Дмитрий", 9786, 3479);
            Client Maksim = new Client("Максим", 37, 0);
            Client Igor = new Client("Игорь", 2056, 0);
            Client Ivan = new Client("Иван", 642, 0);
            bool works = true;
            while (works)
            {
                Console.WriteLine("Приложите/вставте карту и введите пароль.\n");
                if (client.PerformingForLogin())
                {
                    Console.WriteLine($"\nАвторизация прошла успешно.\n Здравствуйте,{client.Name}\n");
                    bool menu = true;
                    while (menu)
                    {
                        Console.WriteLine("Меню:\n 1 - Баланс. \n 2 - Пополнить/Снять. \n 3 - Перевод. \n 4 - Выход. \n");
                        Console.WriteLine("Выберите нужную вам услугу.Нажмите (1-4).");
                        int UserInput = Convert.ToInt32(Console.ReadLine());
                        switch (UserInput)
                        {
                            case 1:
                                Console.WriteLine($"Ваш баланс : {client.GetBalance()} руб.");
                                break;
                            case 2:
                                Console.WriteLine("1 - Пополнить 2 - Снять. Нажмите (1,2).");
                                UserInput = Convert.ToInt32(Console.ReadLine());
                                if (UserInput == 1)
                                {
                                    Console.WriteLine("Введите сумму: ");
                                    float payment = Convert.ToInt32(Console.ReadLine());
                                    client.Replenishment(payment);
                                    Console.WriteLine($"Ваш баланс: {client.GetBalance()} руб.");
                                }
                                else if (UserInput == 2)
                                {
                                    Console.WriteLine("Введите сумму: ");
                                    float payment = Convert.ToInt32(Console.ReadLine());
                                    if (client.TakeOff(payment) > 0)
                                    {
                                        Console.WriteLine($"Не забудьте забрать дельги:{payment} руб.");
                                    }
                                    Console.WriteLine($"Остаток: {client.GetBalance()} руб.");
                                }
                                else
                                {
                                    Console.WriteLine("Услуги под таким номером нет.");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Выберите пользователя которому хотите совершить перевод:\n" +
                                    " 1 - Максим\n 2 - Игорь\n 3 - Иван\n");
                                int operation = Convert.ToInt32(Console.ReadLine());
                                if (operation == 1)
                                {
                                    client.TransferMoney(Maksim);
                                }
                                else if (operation == 2)
                                {
                                    client.TransferMoney(Igor);
                                }
                                else if (operation == 3)
                                {
                                    client.TransferMoney(Ivan);
                                }
                                else
                                {
                                    Console.WriteLine("Такого пользователя нет.");
                                }
                                break;
                            case 4:
                                menu = false;
                                works = false;
                                Console.WriteLine("Досвидания.");
                                break;
                            default:
                                Console.WriteLine("Такой услуги нет.");
                                break;
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    works = false;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    class Client
    {
        public string Name { get; private set; }
        private float _money;
        private float _balance;
        public int Password { get; private set; }
        public Client(string name, float money, int password)
        {
            Name = name;
            _money = money;
            Password = password;
        }
        public float Replenishment(float value)
        {
            _balance = _money;
            _balance += value;
            _money = _balance;
            return _balance;
        }
        public float TakeOff(float value)
        {
            _balance = _money;
            if (_balance >= value)
            {
                _balance -= value;
                _money = _balance;
                return _balance;
            }
            else
            {
                Console.WriteLine("У вас недостаточно средств.");
                _balance = 0;
                return _balance;
            }
        }

        public void TransferMoney(Client recipient)
        {
            Console.WriteLine("Введите сумму: ");
            float payment = Convert.ToInt32(Console.ReadLine());

            if (TakeOff(payment) > 0)
            {
                recipient.Replenishment(payment);
                Console.WriteLine($"Вы перевели {payment} руб. Ваш остаток:{GetBalance()} руб.");
                Console.WriteLine($"Баланс Пользователя: {recipient.GetBalance()} руб");
            }
            else
            {
                Console.WriteLine("Операция прервана.\n");
            }
        }
        public float GetBalance()
        {
            _balance = 0;
            _balance = _money;
            return _balance;
        }
        public bool PerformingForLogin()
        {
            int attempts = 3;
            while (true)
            {
                if (attempts > 0)
                {
                    int UserInput = Convert.ToInt32(Console.ReadLine());
                    if (Password == UserInput)
                        return true;
                    else
                    {
                        Console.WriteLine($"Вы ввели неверный пароль. Попыток: {attempts - 1}\n");
                        attempts--;
                    }
                }
                else
                {
                    Console.WriteLine("Карта заблокирована.");
                    return false;
                }
            }
        }
    }
}
