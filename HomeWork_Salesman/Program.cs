using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Salesman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int counsSalesman = 10;
            Salesman salesman = new Salesman(counsSalesman,new List<Item>() 
            { 
                new Item("Меч",20),
                new Item("Лук",18)
            });
            int coinsBuyer = 30;
            Buyer buyer = new Buyer(coinsBuyer,new List<Item>());

            Trade trade = new Trade(salesman, buyer);
            trade.Trading();
        }
    }

    class Item
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Item(string name, int price)
        {
            Price = price;
            Name = name;
        }
    }


    abstract class User
    {
        protected List<Item> Items = new List<Item>();
        protected int Coins;

        public abstract void ShowItems();
    }

    class Salesman : User
    {
        public Salesman(int coin, List<Item> items) 
        {
            Items = items;
            Coins = coin;
        }
        public override void ShowItems()
        {
            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    Console.WriteLine($"{i + 1}.{item.Name} за {item.Price} монет");
                }
            }
            else 
            {
                Console.WriteLine("Инвентарь пуст");
            }
        }

        public Item GetItem(int idexItem)
        {
            return Items[idexItem];
        }

        public Item SellItem(int indexItem) 
        {

            Item item = Items[indexItem];
            Items.RemoveAt(indexItem);
            Coins += item.Price;
            return item;
        }
    }

    class Buyer : User
    {
        public Buyer(int coin, List<Item> items)
        {
            Items = items;
            Coins = coin;
        }

        public override void ShowItems()
        {
            if (Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    Console.WriteLine($"{item.Name} | ");
                }
            }
            else 
            {
                Console.WriteLine("Инвентарь пуст");
            }
        }

        public int GetCoins() 
        {
            return Coins;
        }

        public void BuyItem(Item item) 
        {
            Coins -= item.Price;
            Items.Add(item);
        }
    }

    class Trade 
    {
        private Salesman _salesman;
        private Buyer _buyer;

        public Trade(Salesman salesman, Buyer buyer)
        {
            _salesman = salesman;
            _buyer = buyer;
        }
        public void Trading() 
        {
            const string CommandShowItemSalesman = "1";
            const string CommandBuyItem = "2";
            const string CommandShowItemBuyer = "3";
            const string CommandExit = "0";

            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine("Нажмите:" +
                    $"\n{CommandShowItemSalesman} - Просмотреть товар у торговца" +
                    $"\n{CommandBuyItem} - Купить товар у торговца" +
                    $"\n{CommandShowItemBuyer} - Просмотреть своий инвентарь" +
                    $"\n{CommandExit} - Выйти");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowItemSalesman:
                        _salesman.ShowItems();
                        break;
                    case CommandBuyItem:
                        BuyItem();
                        break;

                    case CommandShowItemBuyer:
                        _buyer.ShowItems();
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Команда не распознана");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void BuyItem() 
        {
            Console.WriteLine("Какой товар вы хотите приобрести? ");
            int userInput = ReadNumber() - 1;

            Item item = _salesman.GetItem(userInput);

            if (_buyer.GetCoins() >= item.Price)
            {
                item = _salesman.SellItem(userInput);
                _buyer.BuyItem(item);
            }
            else 
            {
                Console.WriteLine("Недостаточно средств");
            }
        }

        private int  ReadNumber() 
        {
            int result;
            string number = Console.ReadLine();
            while (int.TryParse(number, out result) == false) 
            {
                Console.WriteLine("Ошибка ввода.\nВведите число повторно");
                number = Console.ReadLine();
            }

            return result;
        }
    }
}
