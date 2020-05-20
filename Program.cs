using System;

namespace Курсовая_режим_Ч_Ч
{
    class Ships
    {
        public static void Field(int[,] Player, char[,] player)
        {
            Console.WriteLine("Хочете розтавити кораблі автоматично?");
            Console.WriteLine("Відповідь формату так або ні");
            string answer = Console.ReadLine(); //Рядок для відповіді
            if (answer == "так")
            {
                ShipsRandom(Player);            //Автоматична розстановка
            }
            else
            {
                KeyboardShips(Player, player);  //Ручна розстановка
            }
            Console.WriteLine("Поле:");
            Output(Player, player);             //Вивід поля гравця
            for (int i = 0; i < 30; i++)        //Розділення полей
            {                                   //задля збереження таємниці 
                Console.WriteLine();            //розстановки кораблів противника
            }
        }
        public static void Output(int[,] Field, char[,] field)
        {
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
            Console.WriteLine("  _____________________");
            for (int i = 1; i < 11; i++)
            {
                switch (i)
                {
                    case 1:
                        Console.Write("А| ");
                        break;
                    case 2:
                        Console.Write("Б| ");
                        break;
                    case 3:
                        Console.Write("В| ");
                        break;
                    case 4:
                        Console.Write("Г| ");
                        break;
                    case 5:
                        Console.Write("Д| ");
                        break;
                    case 6:
                        Console.Write("Е| ");
                        break;
                    case 7:
                        Console.Write("Є| ");
                        break;
                    case 8:
                        Console.Write("Ж| ");
                        break;
                    case 9:
                        Console.Write("З| ");
                        break;
                    case 10:
                        Console.Write("И| ");
                        break;
                }
                for (int j = 1; j < 11; j++)
                {
                    if (Field[i, j] == 0 || Field[i, j] == 11)
                    {
                        field[i, j] = '.';
                    }
                    else if (Field[i, j] == 12)
                    {
                        field[i, j] = '0';
                    }
                    else if (Field[i, j] == 13)
                    {
                        field[i, j] = 'X';
                    }
                    else
                    {
                        field[i, j] = '#';
                    }
                    Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        static void ShipsRandom(int[,] Field)
        {
            Zero(Field);
            int count = 1;
            int row = 0;
            int column = 0;
            int direction, size;
            Random rand = new Random();
            for (int i = 4; i >= 1; i--)         //розмір коробля
            {
                for (int j = 1; j <= 5 - i; j++) //кількість кораблів 
                {
                mark:
                    row = rand.Next(1, 11);      //рядок початку корабля
                    column = rand.Next(1, 11);   //стовпець початку корабля
                    direction = rand.Next(0, 2); //напрямок(0-горизональний, 1-вертекальний)
                    size = i;                    //розмір корабля
                    bool flag = CheckTheAbilityToShip(row, column, direction, size, Field);
                    if (flag == true)            //Функція перевірки, чи можна поставити корабель
                    {                            //Якщо можна
                        PutShip(row, column, direction, size, Field, count);
                                                //Поставити корабель
                        count++;                //Змінна,якою помічаються кораблі
                    }
                    else                        //В іншому випадку поворити 
                    {
                        goto mark;
                    }
                }
            }
        }
        static void KeyboardShips(int[,] Field, char[,] field) 
        {
            Zero(Field);//функція, що завняє масив нулями
            int count = 1;// змінна, яккою помічаться кораблі в числовому масиві
            for (int i = 4; i >= 1; i--)//розмір коробля
            {
                for (int j = 1; j <= 5 - i; j++)//кількість кораблів 
                {
                    Console.WriteLine("Розташуйте корабель розміром {0}", i);
                mark:
                    int size = i; //Розмір корабля
                    Console.WriteLine("Введіть координати початку корабля");
                    Console.WriteLine("В форматі А 10");
                    string coordinates = Console.ReadLine();//ввід координат
                    int row = RetRow(coordinates);//рядок початку корабля
                    int column = Convert.ToInt32(coordinates.Split(' ')[1]);//стовпець початку коробля
                    Console.WriteLine("Виберіть напрям 1- вертикально, 0-горизонтально");
                    int direction = int.Parse(Console.ReadLine());//напрям корабля
                    bool flag = CheckTheAbilityToShip(row, column, direction, size, Field);//функція перевірки 
                    if (flag == true)            //якщо можно поставити корабль
                    {
                        Console.WriteLine("Корабель можна поставити");
                        PutShip(row, column, direction, size, Field, count);//функція 
                        Output(Field, field);//вивід поля
                        count++;

                    }
                    else
                    {
                        Console.WriteLine("Корабель не можна поставити, повторіть заново");
                        goto mark;
                    }
                }
            }
        }
        public static int RetRow(string coordinates)
        {
            int row = 0;
            switch (coordinates.Split(' ')[0])
            {
                case "А":
                    row = 1;
                    break;
                case "Б":
                    row = 2;
                    break;
                case "В":
                    row = 3;
                    break;
                case "Г":
                    row = 4;
                    break;
                case "Д":
                    row = 5;
                    break;
                case "Е":
                    row = 6;
                    break;
                case "Є":
                    row = 7;
                    break;
                case "Ж":
                    row = 8;
                    break;
                case "З":
                    row = 9;
                    break;
                case "И":
                    row = 10;
                    break;
            }
            return row;
        }
        static bool CheckTheAbilityToShip(int row, int column, int direction, int size, int[,] Field)//проверка, можно ли поставить корабыль
        {
            bool flag = true;
            //влазит ли корабыль в поле
            if (direction == 0)
            {
                if (column + size > 11)
                {
                    flag = false;
                }
            }
            if (direction == 1)
            {
                if (row + size > 11)
                {
                    flag = false;
                }
            }
            if (flag == true)
            {
                //проверка пустые ли клетки коробля
                for (int i = 0; i < size; i++)
                {
                    if (direction == 0)
                    {
                        if (Field[row, column] != 0)
                        {
                            flag = false;
                            break;
                        }
                        column++;
                    }
                    else
                    {
                        if (Field[row, column] != 0)
                        {
                            flag = false;
                            break;
                        }
                        row++;
                    }
                }
            }
            return flag;
        }
        static void PutShip(int row, int column, int direction, int size, int[,] Field, int count)//поставить корабль
        {
            if (direction == 0)//клетки вокруг коробля
                               //горизонтальний
            {
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = column - 1; j <= column + size; j++)
                    {
                        Field[i, j] = 11;
                    }
                }
            }
            if (direction == 1)//вертикальний
            {
                for (int i = row - 1; i <= row + size; i++)
                {
                    for (int j = column - 1; j <= column + 1; j++)
                    {
                        Field[i, j] = 11;
                    }
                }
            }
            for (int i = 0; i < size; i++)//клетки коробля
            {
                Field[row, column] = count;
                if (direction == 0)
                {
                    column++;
                }
                if (direction == 1)
                {
                    row++;
                }
            }
        }
        public static void Zero(int[,] Field)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Field[i, j] = 0;
                }
            }
        }
    }
    class Opponent1
    {
        public static void Brain(int[,] Player1, char[,] player1, int[,] Player2, char[,] player2, int[] mas1, int[] mas2, int[] mas, int[,] Pl1, int[,] Pl2)
        {
        mark:
        mark2:
            Console.WriteLine("Хід 1 гравця");
            Console.WriteLine("Введіть координати в форматі А 10");
            string coordinates = Console.ReadLine();
            int row = Ships.RetRow(coordinates);
            int column = Convert.ToInt32(coordinates.Split(' ')[1]);
            bool cheak = Program.Check(Player2, row, column);//Перевірка, чи закриця кліипнка
            if (cheak == false)
            {
                Console.WriteLine("Ця клітинка вже відкрита, повторіть спробу");
                goto mark;
            }
            else
            {
                if (Player2[row, column] == 0 || Player2[row, column] == 11)//якщо клітика пуста
                {
                    Player2[row, column] = 12;      //позначається як відкритта пуста
                    Pl2[row, column] = 12;
                    Program.OutPut(Pl1, Pl2, player1, player2); //вивід поля
                    Console.WriteLine("Промах!");
                    Opponent2.Brain(Player1, player1, Player2, player2, mas1, mas2, mas,Pl1, Pl2);
                }//хід другого гравця 
                else
                {
                    int index = Player2[row, column];
                    mas2[index]--;
                    Player2[row, column] = 13;
                    Pl2[row, column] = 13;
                    bool check_killed = Program.CheckShipKilled(Player2, mas2, index);
                    if (check_killed == true)//перевірка  чи потоплений корабель
                    {                        //якщо потопленно
                        Program.SinkingShip(Player2, row, column, mas, index, Pl2);//функція, що позначить клітинки навколо
                        Program.OutPut(Pl1, Pl2, player1, player2);
                        Console.WriteLine("Потоплено!");
                    }
                    else
                    {
                        Program.OutPut(Pl1, Pl2, player1, player2);
                        Console.WriteLine("Поранено!");
                    }
                    bool Win = Program.CheckWinner(Player2);
                    if (Win == true)
                    {
                        goto mark3;
                    }
                    else goto mark2;
                    mark3:
                    Console.WriteLine("Перемога гравця 1!!!");
                    Console.WriteLine("Дякую за гру!!!");
                }
            }
        }
    }
    class Opponent2
    {
        public static void Brain(int[,] Player1, char[,] player1, int[,] Player2, char[,] player2, int[] mas1, int[] mas2, int[] mas,int [,] Pl1, int [,] Pl2)
        {
        mark:
        mark2:
            Console.WriteLine("Хід 2 гравця");
            Console.WriteLine("Введіть координати в форматі А 10");
            string coordinates = Console.ReadLine();
            int row = Ships.RetRow(coordinates);
            int column = Convert.ToInt32(coordinates.Split(' ')[1]);
            bool cheak = Program.Check(Player1, row, column);
            if (cheak == false)
            {
                Console.WriteLine("Ця клітинка вже відкрита, повторіть спробу");
                goto mark;
            }
            else
            {
                if (Player1[row, column] == 0 || Player1[row, column] == 11)
                {
                    Console.WriteLine("Промах!");
                    Player1[row, column] = 12;
                    Pl1[row, column] = 12;
                    Program.OutPut(Pl1, Pl2, player1, player2);
                    Opponent1.Brain(Player1, player1, Player2, player2, mas1, mas2, mas, Pl1, Pl2);
                }
                else
                {
                    int index = Player1[row, column];
                    mas1[index]--;
                    Player1[row, column] = 13;
                    Pl1[row, column] = 13;
                    bool check_killed = Program.CheckShipKilled(Player1, mas1, index);
                    if (check_killed == true)
                    {
                        Program.SinkingShip(Player1, row, column, mas, index, Pl1);
                        Program.OutPut(Pl1, Pl2, player1, player2);
                        Console.WriteLine("Потоплено!");
                    }
                    else
                    {
                        Program.OutPut(Pl1, Pl2, player1, player2);
                        Console.WriteLine("Поpанено!");
                    }
                    bool Win = Program.CheckWinner(Player1);
                    if (Win == true)
                    {
                        goto mark3;
                    }
                    else goto mark2;
                    mark3:
                    Console.WriteLine("Перемога гравця 2!!!");
                    Console.WriteLine("Дякую за гру!!!");
                }
            }
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] Player1 = new int[12, 12];
            int[,] Player2 = new int[12, 12];

            int[,] Pl1 = new int[12, 12];
            int[,] Pl2 = new int[12, 12];

            char[,] player1 = new char[12, 12];
            char[,] player2 = new char[12, 12];

            int[] mas1 = { 0, 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };  //player1
            int[] mas2 = { 0, 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };//player2
            int[] mas = { 0, 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };//size
            Ships.Zero(Pl1);
            Ships.Zero(Pl2);
            Console.WriteLine(" Вітаю у грі!!! Розставте свої кораблі та прочинаймо гру!!!");
            Console.WriteLine("   **  Гравець №1 **");

            Ships.Field(Player1, player1);
            Console.WriteLine("   **  Гравець №2 **");
            Ships.Field(Player2, player2);
            Opponent1.Brain(Player1, player1, Player2, player2, mas1, mas2, mas,Pl1,Pl2);
        }
        public static void OutPut(int [,]  Pl1,int [,] Pl2, char[,] player1, char[,] player2)
        {
            Console.WriteLine("   ** Поле гравця №1 **");
            Ships.Output(Pl1, player1);
            Console.WriteLine("   ** Поле гравця №2 **");
            Ships.Output(Pl2, player2);
        }
        public static bool CheckWinner (int[,] Player)
        {
            bool check = false;
            int count = 0;
            for(int i=1;i<11;i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if(Player[i,j]==13)
                    {//якщо клітинка є відкритим кораблем(або його частиною)
                        count++;//рахунок збільшується
                        if(count==20)
                        {//якщо ккількість таких  кліинок рівна 20
                            check = true;//повертається значення true
                            break;//фуункція зупиняється
                        }//гра закінченна
                    }
                }
            }
            return check;
        }
        public static bool Check(int[,] Field, int row, int column)
        {
            bool checker = true;
            if (Field[row, column] == 12 || Field[row, column] == 13)
            {
                checker = false;
            }
            return checker;
        }
        public static bool CheckShipKilled(int[,] Field, int[] mas, int index)//проверка, потоплен ли корабыль
        {
            bool cheack_killed = false;
            if (mas[index] == 0)
            {
                cheack_killed = true;
            }
            return cheack_killed;
        }
        public static void SinkingShip(int[,] Field, int row, int column, int[] mas, int index, int [,] Pl)
        {
            int direction;//напрям(1-вертикальний, 0-горизонатльний)
            if (Field[row - 1, column] == 13 || Field[row + 1, column] == 13)
            {//якщо кліинка зверху, або  знизу-корабель верикальний
                direction = 1;
            }//інакше, горизонтальний
            else direction = 0;
            if (direction == 1)
            {//функція пошуку початку корабля(при різних напрямах)
                row = FindBeginningShipDirection1(Field, row, column);//вертикальний
            }
            else column = FindBeginningShipDirection0(Field, row, column);//горизонтальний
            int size = mas[index];//розмір корабля
            MarkingCellsArroundShip(Field, direction, row, column, size, Pl);
        }           //функція, що відкриває клітинки навколо            
        public static int FindBeginningShipDirection1(int[,] Field, int row, int column)
        {
            for (int i = 1; i < 5; i++)
            {
                if (Field[row - i, column] != 13)
                {
                    row = row - i + 1;
                    break;
                }
            }
            return row;
        }
        public static int FindBeginningShipDirection0(int[,] Field, int row, int column)
        {
            for (int i = 1; i < 5; i++)
            {
                if (Field[row, column - i] != 13)
                {
                    column = column - i + 1;
                    break;
                }
            }
            return column;
        }
        public static void MarkingCellsArroundShip(int[,] Field, int direction, int row, int column, int size,int [,] Pl)
        {
            if (direction == 0)//клетки вокруг коробля
                               //горизонтальний
            {
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = column - 1; j <= column + size; j++)
                    {
                        Field[i, j] = 12;
                        Pl[i, j] = 12;
                    }
                }
            }
            if (direction == 1)//вертикальний
            {
                for (int i = row - 1; i <= row + size; i++)
                {
                    for (int j = column - 1; j <= column + 1; j++)
                    {
                        Field[i, j] = 12;
                        Pl[i, j] = 12;
                    }
                }
            }
            for (int i = 0; i < size; i++)//клетки коробля
            {
                Field[row, column] = 13;
                Pl[row, column] = 13;
                if (direction == 0)
                {
                    column++;
                }
                if (direction == 1)
                {
                    row++;
                }
            }
        }
    }
}