using System;

namespace PoolPatternExample
{
    interface IObject
    {
        bool IsBusy
        {
            get;
            set;
        }
    }
    interface IPool
    {
        IObject GetObject();
    }

    class Deck : IPool
    {
        Card[] cards;

        public int Count
        {
            get; private set;
        }

        public Deck(int amount)
        {
            Count = amount;
            cards = new Card[amount];

            for (int i = 0; i < cards.Length; i++)
                cards[i] = new Card(i + 1);
        }

        public void Shuffle()
        {
            Random rand = new Random();

            int randNumber;
            Card tempCard;

            for (int i = 0; i < Count; i++)
            {
                randNumber = rand.Next(Count);

                tempCard = cards[i];
                cards[i] = cards[randNumber];
                cards[randNumber] = tempCard;
            }
        }

        public IObject GetObject()
        {
            for (int i = 0; i < cards.Length; i++)
                if (!cards[i].IsBusy)
                {
                    cards[i].IsBusy = true;
                    return cards[i];
                }

            throw new Exception("Нету свободных объектов!");
        }
    }
    class Card : IObject
    {
        public int Number
        {
            get;
            set;
        }
        public bool IsBusy
        {
            get;
            set;
        }

        public Card(int number)
        {
            Number = number;
        }
    }

    class Program
    {
        static void StartGame(int deckSize, int playersAmountCards, int amountGames)
        {
            int scorePlayer1 = 0;
            int scorePlayer2 = 0;

            Deck deck = new Deck(deckSize);

            Card[] player1 = new Card[playersAmountCards];
            Card[] player2 = new Card[playersAmountCards];

            for (int i1 = 0; i1 < amountGames; i1++)
            {
                Console.Clear();

                Console.WriteLine("---< Пример паттерна Pool >---\n");

                deck.Shuffle();

                for (int i2 = 0; i2 < playersAmountCards; i2++)
                {
                    player1[i2] = deck.GetObject() as Card;
                    player2[i2] = deck.GetObject() as Card;

                    scorePlayer1 += player1[i2].Number;
                    scorePlayer2 += player2[i2].Number;

                    Console.WriteLine("Карта игрока 1: {0}", player1[i2].Number);
                    Console.WriteLine("Карта игрока 2: {0}", player2[i2].Number);
                }

                for (int i3 = 0; i3 < playersAmountCards; i3++)
                {
                    player1[i3].IsBusy = false;
                    player2[i3].IsBusy = false;
                }

                if (scorePlayer1 > scorePlayer2)
                    Console.WriteLine("\nВыиграл игрок 1");
                else
                    Console.WriteLine("\nВыиграл игрок 2");

                Console.WriteLine("\nEnter - Следующая игра");
                Console.WriteLine("Esc - Выход");

                ConsoleKey inputKey = Console.ReadKey().Key;

                if (inputKey == ConsoleKey.Enter)
                    continue;
                else if (inputKey == ConsoleKey.Escape)
                    break;
            }
        }

        static void Main(string[] args)
        {
            int playersAmountCards = 2;
            int amountGames = 2;
            int deckSize = 16;

            StartGame(deckSize, playersAmountCards, amountGames);
        }
    }
}
